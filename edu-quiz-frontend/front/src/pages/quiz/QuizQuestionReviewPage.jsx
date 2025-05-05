import { useLocation, useNavigate } from "react-router-dom";
import CustomReviewCard from "../../components/CustomReviewCard";
import CustomButton from "../../components/CustomButton";
import useUser from '../../hooks/useUser';
import { postQuizResult } from "../../api/quizApi"; 

function QuizQuestionReviewPage() {
    // const location = useLocation();
    // const navigate = useNavigate()
    // const { submission, score, totalQuestions, title, questions, selectedAnswers} = location.state || {};
    // const percentage = ((score / totalQuestions) * 100).toFixed(2);

    const location = useLocation();
    const navigate = useNavigate();
    const { submission, questions, selectedAnswers, title } = location.state || {};

    const quizId = submission ? submission.quizId : ""

    const calculateScore = () => {
        let score = 0;

        questions.forEach((question, index) => {
            const correctAnswer = question.answers.find(answer => answer.isCorrect); // Find the correct answer
            const selectedAnswer = selectedAnswers[index]?.selectedAnswerText; // Get the selected answer

            if (selectedAnswer === correctAnswer?.answerText) {
                score += 1; // Increment score if the selected answer is correct
            }
        });

        return score;
    };

    const handleOnClick = async () => {
        try {
            const score = calculateScore(); // Calculate the score
            const response = await postQuizResult(submission);
            console.log("Quiz submission response:", response);

            // Navigate to ResultPage with the quiz details
            navigate("/resultPage", {
                state: {
                    submission,
                    score,
                    totalQuestions: questions.length,
                    title,
                    questions,
                    selectedAnswers,
                    showCorrectness: true,
                },
            });
        } catch (error) {
            console.error("Error while submitting quiz:", error);
            alert("There was an error submitting your quiz. Please try again later.");
        }
    };

    const handleOnClickBack = () => {
        if (quizId) {
            navigate(`/quiz/${quizId}`); // This will navigate back to the specific quiz page
            
        } else {
            navigate("/quizes"); // Fallback to the quiz list page if quizId is missing
        }
    };

    return (
        <div>
            
            <div className="contain">
                <div className="row justify-content-center p-5">
                    <div className="col-md-8">
                        <h2 className="fw-bold text-success text-center mb-4">ПРЕГЛЕД НА ОДГОВОРИ</h2>
                        <h4 className="text-success text-center mb-4">За квизот</h4>
                        
                        <div className="container pt-4">
                            {questions.map((question, index) => (
                                <CustomReviewCard 
                                    key={index} 
                                    question={question} 
                                    selectedAnswer={selectedAnswers[index]?.selectedAnswerText}
                                    showCorrectness={false}
                                />
                            ))}
                        </div>

                        <div className="container d-flex justify-content-center">
                            <button
                                style={{ color: 'black', backgroundColor: 'rgba(181, 212, 205, 1)' }}
                                className="btn btn-success border-0 mt-4 px-4 py-2"
                                onClick={handleOnClickBack}
                            >
                                НАЗАД
                            </button>
                            <button
                                style={{ color: 'black', backgroundColor: 'rgba(181, 212, 205, 1)' }}
                                className="btn btn-success border-0 mt-4 px-4 py-2"
                                onClick={handleOnClick}
                            >
                                Submit Quiz
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default QuizQuestionReviewPage;
