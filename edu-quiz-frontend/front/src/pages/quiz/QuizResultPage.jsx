import { useLocation, useNavigate } from "react-router-dom";
import CustomReviewCard from "../../components/CustomReviewCard";
import CustomButton from "../../components/CustomButton";
import { useTranslation } from 'react-i18next';

function QuizResultPage() {
    const location = useLocation();
    const navigate = useNavigate();
    const { submission } = location.state || {};
    const { t } = useTranslation();
    
    if (!submission) return <div>No submission found.</div>;
    
    const { quiz, results } = submission;
    const result = results[results.length - 1]; 
    const { score, userAnswers } = result;
    console.log("submission",submission)
    
    
    const handleGoBack = () => navigate("/myQuizesPage");
    
 
    const selectedAnswersMapped = quiz.questions.map((question) => {
        const userAnswer = userAnswers.find(ua => ua.questionId === question.questionId);
        const selectedAnswerId = userAnswer?.selectedAnswerIds?.[0]; 
        const selectedAnswerObj = question.answers.find(ans => ans.answerId === selectedAnswerId);
        return {
            selectedAnswerText: selectedAnswerObj?.answerText || "—"
        };
    });
    
    return (
        <div>
    <div className="contain">
        <div className="row justify-content-center p-5">
            <div className="col-md-8">
                <h2 className="fw-bold text-success text-center mb-4">ПРЕГЛЕД НА ОДГОВОРИ</h2>
                
                <h4 className="text-success text-center mb-2">
                    За квизот: {quiz.title}
                </h4>

                <h5 className="text-center mb-4">
                    <span className="badge bg-success fs-5 px-3 py-2">
                        Резултат: {result.score}%
                    </span>
                </h5>

                <div className="container pt-4 bg-light rounded-3">
                    {quiz.questions.map((question, index) => (
                        <CustomReviewCard
                            key={index}
                            question={question}
                            selectedAnswer={selectedAnswersMapped[index].selectedAnswerText}
                            correctAnswer={question.answers.find(answer => answer.isCorrect)}
                            showCorrectness={true}
                        />
                    ))}
                </div>

                <div className="container d-flex justify-content-center">
                    <button
                        
                        className="btn btn-dark border-0 mt-4 px-4 py-2"
                        onClick={handleGoBack}
                    >
                        НАЗАД
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>

    );
}

export default QuizResultPage;
