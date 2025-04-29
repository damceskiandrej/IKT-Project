import CustomQuestion from "../../components/CustomQuestion";
import CustomAnswer from "../../components/CustomAnswer";
import CustomButton from "../../components/CustomButton";
import { getQuizById } from "../../api/quizApi";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";

function QuizQuestionsPage() {
    const { id } = useParams();
    const [quiz, setQuiz] = useState(null);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0); 
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedAnswers, setSelectedAnswers] = useState([]);
    const navigate = useNavigate()


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

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    if (!quiz || quiz.questions.length === 0) return <div>No questions found</div>;

   
    const currentQuestion = quiz.questions[currentQuestionIndex];


    const handleAnswerClick = (answerText) => {
        const updatedAnswers = [...selectedAnswers];
        updatedAnswers[currentQuestionIndex] = answerText;
        setSelectedAnswers(updatedAnswers);
        console.log(updatedAnswers); 
        console.log(answerText)
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

        if (!selectedAnswers[currentQuestionIndex]) {
            alert("Please select an answer before proceeding.");
            return;
        }

        if (currentQuestionIndex < quiz.questions.length - 1) {
            setCurrentQuestionIndex(currentQuestionIndex + 1);
        } else {
            const score = calculateScore();
            navigate('/result' , 
                    { state: 
                        {
                            score,
                            totalQuestions: quiz.questions.length,
                            questions: quiz.questions, 
                            selectedAnswers,
                            title: quiz.title
                        }
                    }
            )
            alert(`Quiz Completed!!! Your score ${score}/${quiz.questions.length}`);
        }
    }

    const handlePreviousQuestion = () => {
        if (currentQuestionIndex > 0) {
            setCurrentQuestionIndex(currentQuestionIndex - 1);
        }
    };

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
                            onClick={() => handleAnswerClick(answer.answerText)}
                            isSelected={selectedAnswers[currentQuestionIndex] === answer.answerText}
                        />
                    </div>
                ))}
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
                    />
                </div>
            </div>
            
        </div>
    );
}



export default QuizQuestionsPage;
