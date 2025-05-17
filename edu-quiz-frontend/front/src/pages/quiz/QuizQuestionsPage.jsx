import CustomQuestion from "../../components/CustomQuestion";
import CustomAnswer from "../../components/CustomAnswer";
import CustomButton from "../../components/CustomButton";
import { getQuizById } from "@/api/quizApi.js";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import useUser from "../../hooks/useUser";
import CustomHint from "../../components/CustomHint";
import CustomTimer from "../../components/CustomTimer"
import { getHint } from "@/api/aiApi.js";
import {toast, ToastContainer} from "react-toastify";
import { useTranslation } from 'react-i18next';


function QuizQuestionsPage() {
    const { id } = useParams();
    const navigate = useNavigate()
    const [quiz, setQuiz] = useState(null);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedAnswers, setSelectedAnswers] = useState([]);
    const [timer, setTimer] = useState(300);
    const [timerDuration] = useState(300);
    const user = useUser();
    const userId = user ? user.userId : "";
    const [hints, setHints] = useState({});

    const { t, i18n } = useTranslation();

    useEffect(() => {
        const fetchQuiz = async () => {
            try {
                setIsLoading(true);
                const data = await getQuizById(id);
                data.questions.forEach(question => {
                    question.answers = question.answers.filter(answer => answer.answerText.toLowerCase() !== "n/a");
                });
                setQuiz(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setIsLoading(false);
            }
        };
        fetchQuiz();
    }, [id]);

    useEffect(() => {
        let timerInterval;

        if (timer > 0 && quiz) {
            timerInterval = setInterval(() => {
                setTimer(prevTimer => prevTimer - 1);
            }, 1000);
        } else if (timer === 0) {
            handleNextQuestion();
        }

        return () => clearInterval(timerInterval);
    }, [timer, quiz]);

    useEffect(() => {
        const currentQuestionId = quiz?.questions[currentQuestionIndex]?.id;
        if (currentQuestionId && !(currentQuestionId in hints)) {
            setHints(prev => ({ ...prev, [currentQuestionId]: null }));
        }
    }, [currentQuestionIndex, quiz]);



    const handleAnswerClick = (answerId) => {
        const selectedAnswer = quiz.questions[currentQuestionIndex].answers.find(ans => ans.id === answerId);
        const updatedAnswers = [...selectedAnswers];
        updatedAnswers[currentQuestionIndex] = {
            questionId: quiz.questions[currentQuestionIndex].id,
            selectedAnswerId: answerId,
            selectedAnswerText: selectedAnswer.answerText,
            correctAnswerText: selectedAnswer.isCorrect ? selectedAnswer.answerText : ''
        };
        setSelectedAnswers(updatedAnswers);
    };

    const calculateScore = () => {
        let correctAnswersCount = 0;
        selectedAnswers.forEach((selectedAnswerText, index) => {
            const question = quiz.questions[index];
            const correctAnswer = question.answers.find((answer) => answer.isCorrect);
            if (correctAnswer && selectedAnswerText === correctAnswer.answerText) {
                correctAnswersCount++;
            }
        });
        return correctAnswersCount;
    };

    const handleNextQuestion = () => {
        if (currentQuestionIndex < quiz.questions.length - 1) {
            setCurrentQuestionIndex(currentQuestionIndex + 1);
            setTimer(timerDuration); // Reset the timer for the next question
        } else {

            const score = selectedAnswers.reduce((correctAnswersCount, selection, index) => {
                // Ensure selection exists and has a valid selectedAnswerId
                if (selection && selection.selectedAnswerId !== undefined) {
                    const question = quiz.questions[index];
                    const correctAnswer = question.answers.find(answer => answer.isCorrect);
                    return selection.selectedAnswerId === correctAnswer.id ? correctAnswersCount + 1 : correctAnswersCount;
                }
                return correctAnswersCount;
            }, 0);


            const submission = {
                userId: userId,
                quizId: quiz.id,
                questionResults: selectedAnswers.map((selection) => ({
                    questionId: selection.questionId,
                    selectedAnswerIds: [selection.selectedAnswerId]
                }))
            };

            toast.info(`${t('quiz_done')} ${score}/${quiz.questions.length}. ${t('click_to_continue')}`, {
                autoClose: false,
                closeButton: false,
                draggable: false,
                closeOnClick: true,
                onClose: () => {
                    navigate('/reviewPage', {
                        state: {
                            submission,
                            score,
                            totalQuestions: quiz.questions.length,
                            questions: quiz.questions,
                            selectedAnswers,
                            title: quiz.title
                        }
                    });
                }
            });
        }
    };


    const handlePreviousQuestion = () => {
        if (currentQuestionIndex > 0) {
            setCurrentQuestionIndex(currentQuestionIndex - 1);
            setTimer(timerDuration); // Reset the timer for the previous question
        }
    };

    const fetchHintForQuestion = async (questionId) => {
        try {
            const hintText = await getHint(quiz.id, questionId);
            console.log("Hint:", hintText);

            setHints(prev => ({
                ...prev,
                [questionId]: hintText || "No hint available."
            }));
        } catch (err) {
            console.error(err);
            setHints(prev => ({ ...prev, [questionId]: "Error loading hint." }));
        }
    };




    // Check if the current question has a selected answer
    const isAnswerSelected = selectedAnswers[currentQuestionIndex]?.selectedAnswerId !== undefined;

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!quiz || quiz.questions.length === 0) return <div>No questions found</div>;



    const currentQuestion = quiz.questions[currentQuestionIndex];

    return (
        <div className="container mt-5 p-5 rounded-5 shadow-lg bg-gradient bg-light">
            {/* Top section with title and timer */}
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold">{quiz.title}</h2>
                <div className="position-relative">
                    <CustomTimer timer={timer} />
                    {/* Animated pulse when timer is low */}
                    {timer <= 10 && <div className="pulse-timer" />}
                </div>
            </div>

            {/* Progress Bar */}
            <div className="progress mb-4 rounded-pill" style={{ height: '1.5rem' }}>
                <div
                    className="progress-bar progress-bar-striped progress-bar-animated bg-success"
                    role="progressbar"
                    style={{
                        width: `${((currentQuestionIndex + 1) / quiz.questions.length) * 100}%`,
                    }}
                >
                    {`${currentQuestionIndex + 1} / ${quiz.questions.length}`}
                </div>
            </div>

            {/* Question Section with light animation */}
            <div className="mb-4 animate-slide-in">
                <CustomQuestion
                    title={quiz.title}
                    question={currentQuestion.questionText}
                    currentQuestionNumber={currentQuestionIndex + 1}
                    totalQuestions={quiz.questions.length}
                />
            </div>

            {/* Answer Options Grid */}
            <div className="row g-4">
                {currentQuestion.answers.map((answer, index) => {
                    const isSelected =
                        selectedAnswers[currentQuestionIndex]?.selectedAnswerId === answer.id;

                    return (
                        <div className="col-md-6" key={index}>
                            <CustomAnswer
                                letter={String.fromCharCode(65 + index)}
                                answer={answer.answerText}
                                onClick={() => handleAnswerClick(answer.id)}
                                isSelected={isSelected}
                                additionalClass={isSelected ? 'selected-answer-card' : ''}
                            />
                        </div>
                    );
                })}
            </div>

            {/* Hint Box */}
            <div className="mt-4">
                <CustomHint
                    hint={hints[currentQuestion.id]}
                    fetchHint={() => fetchHintForQuestion(currentQuestion.id)}
                    style={{ border: '2px dashed #6c757d', borderRadius: '1rem', padding: '1rem' }}
                />
            </div>

            {/* Navigation Buttons */}
            <div className="d-flex justify-content-between align-items-center mt-5">
                <CustomButton
                    btnText={t('previous_question')}
                    onClick={handlePreviousQuestion}
                    disabled={currentQuestionIndex === 0}
                    variant="outline-secondary"
                />
                <CustomButton
                    btnText={
                        currentQuestionIndex === quiz.questions.length - 1
                            ? t('final_question')
                            : t('next_question')
                    }
                    onClick={handleNextQuestion}
                    disabled={!isAnswerSelected}
                    variant="success"
                />
            </div>

            {/* Toast Notification */}
            <ToastContainer
                position="top-center"
                autoClose={3000}
                hideProgressBar={false}
                theme="colored"
            />
        </div>
    );
}

export default QuizQuestionsPage;
