import CustomQuestion from "../../components/CustomQuestion";
import CustomAnswer from "../../components/CustomAnswer";
import CustomButton from "../../components/CustomButton";
import { getQuizById } from "../../api/quizApi";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import useUser from "../../hooks/useUser";

function QuizQuestionsPage() {
    const { id } = useParams();
    const [quiz, setQuiz] = useState(null);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedAnswers, setSelectedAnswers] = useState([]);
    const [timer, setTimer] = useState(30); // Initial timer value (30 seconds per question)
    const [timerDuration] = useState(30); // You can adjust this value for the timer duration
    const navigate = useNavigate();
    const user = useUser();
    const userId = user ? user.userId : "";

    useEffect(() => {
        const fetchQuiz = async () => {
            try {
                setIsLoading(true);
                const data = await getQuizById(id);
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
            }, 1000); // Decrease the timer every second
        } else if (timer === 0) {
            handleNextQuestion(); // Automatically move to the next question when timer hits 0
        }

        return () => clearInterval(timerInterval); // Clean up the interval on unmount or when timer changes
    }, [timer, quiz]);

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
            // Calculate score after the last question
            const score = selectedAnswers.reduce((correctAnswersCount, selection, index) => {
                // Ensure selection exists and has a valid selectedAnswerId
                if (selection && selection.selectedAnswerId !== undefined) {
                    const question = quiz.questions[index];
                    const correctAnswer = question.answers.find(answer => answer.isCorrect);
                    return selection.selectedAnswerId === correctAnswer.id ? correctAnswersCount + 1 : correctAnswersCount;
                }
                return correctAnswersCount;
            }, 0);
    
            // Build the submission object for result page
            const submission = {
                userId: userId,
                quizId: quiz.id,
                questionResults: selectedAnswers.map((selection) => ({
                    questionId: selection.questionId,
                    selectedAnswerIds: [selection.selectedAnswerId]
                }))
            };
    
            // Navigate to the result page with the score and selected answers
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
    
            alert(`Quiz Completed! Your score is ${score}/${quiz.questions.length}`);
        }
    };
    

    const handlePreviousQuestion = () => {
        if (currentQuestionIndex > 0) {
            setCurrentQuestionIndex(currentQuestionIndex - 1);
            setTimer(timerDuration); // Reset the timer for the previous question
        }
    };

    // Check if the current question has a selected answer
    const isAnswerSelected = selectedAnswers[currentQuestionIndex]?.selectedAnswerId !== undefined;

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!quiz || quiz.questions.length === 0) return <div>No questions found</div>;

    const currentQuestion = quiz.questions[currentQuestionIndex];

    return (
        <div className="container mt-5">
            <CustomQuestion
                title={quiz.title}
                question={currentQuestion.questionText}
                currentQuestionNumber={currentQuestionIndex + 1}
                totalQuestions={quiz.questions.length}
            />
            <div className="row mt-5 g-3">
                {currentQuestion.answers.map((answer, index) => (
                    <div className="col-md-6" key={index}>
                        <CustomAnswer
                            letter={String.fromCharCode(65 + index)}
                            answer={answer.answerText}
                            onClick={() => handleAnswerClick(answer.id)}
                            isSelected={selectedAnswers[currentQuestionIndex]?.selectedAnswerId === answer.id}
                        />
                    </div>
                ))}
            </div>
            <div className="d-flex justify-content-center mt-4">
                <p>{timer} seconds remaining</p> {/* Display the timer */}
            </div>
            <div className="container d-flex justify-content-around">
                <div className="mt-4 d-flex justify-content-center">
                    <CustomButton
                        btnText={"ПРЕТХОДНО"}
                        onClick={handlePreviousQuestion}
                        disabled={currentQuestionIndex === 0}
                    />
                </div>
                <div className="mt-4 d-flex justify-content-center">
                    <CustomButton
                        btnText={currentQuestionIndex === quiz.questions.length - 1 ? "ФИНАЛНО ПРАШАЊЕ" : "СЛЕДНО ПРАШАЊЕ"}
                        onClick={handleNextQuestion}
                        disabled={!isAnswerSelected} // Disable the button if no answer is selected
                    />
                </div>
            </div>
        </div>
    );
}

export default QuizQuestionsPage;
