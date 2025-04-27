import CustomQuestion from "../components/CustomQuestion";
import CustomAnswer from "../components/CustomAnswer";
import CustomButton from "../components/CustomButton";
import { getQuizById } from "../api/quizApi";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

function QuizQuestionsPage() {
    const { id } = useParams();
    const [quiz, setQuiz] = useState(null);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0); // Track current question
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedAnswer, setSelectedAnswers] = useState([]);


    // Fetch quiz data
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

    // Get the current question and answers
    const currentQuestion = quiz.questions[currentQuestionIndex];

    const handleAnswerClick = (selectedAnswer) => {
        const updatedAnswers = [...selectedAnswers];
        updatedAnswers[currentQuestionIndex] = selectedAnswer;
        setSelectedAnswers(updatedAnswers);
    };

    const handleNextQuestion = () => {
        if (currentQuestionIndex < quiz.questions.length - 1) {
            setCurrentQuestionIndex(currentQuestionIndex + 1);
        } else {
            alert("Quiz Completed!");
        }
    }

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
                            
                        />
                    </div>
                ))}
            </div>
            <div className="mt-4 d-flex justify-content-center">
                <CustomButton 
                    btnText={"СЛЕДНО ПРАШАЊЕ"} 
                    onClick={handleNextQuestion} 
                />
            </div>
        </div>
    );
}

export default QuizQuestionsPage;
