import { useLocation, useNavigate } from "react-router-dom";
import CustomReviewCard from "../../components/CustomReviewCard";
import CustomButton from "../../components/CustomButton";

function ResultPage() {
    const location = useLocation();
    const navigate = useNavigate()
    const { score, totalQuestions, title, questions, selectedAnswers} = location.state || {};
    const percentage = ((score / totalQuestions) * 100).toFixed(2);

    const handleOnClick = () => {
        navigate("/home")
    }

    return (
        <div>
            <div className="d-flex justify-content-center align-items-center vh-100">
                <div
                    style={{
                        width: '350px',
                        height: '350px',
                        borderRadius: '50%',
                        backgroundColor: '#538c7a',
                        position: 'relative',
                        textAlign: 'center',
                        color: 'white',
                        padding: '40px 20px',
                        boxShadow: '0 4px 8px rgba(0, 0, 0, 0.2)',
                    }}>
                    <div className="container mt-5 p-3">
                        <h2 className="fw-bold">БРАВО!</h2>
                        <h4 className="fw-bold">Твојот резултат е {percentage}%.</h4>
                        <CustomButton btnText={"НАЗАД"} />
                    </div>
                </div>
            </div>

            <div className="contain">
                <div className="row justify-content-center p-5">
                    <div className="col-md-8">
                        <h2 className="fw-bold text-success text-center mb-4">ПРЕГЛЕД НА ОДГОВОРИ</h2>
                        <h4 className="text-success text-center mb-4">За квизот {title}</h4>
                        
                        <div className="container pt-4">
                            {questions.map((question, index) => (
                                <CustomReviewCard 
                                    key={index} 
                                    question={question} 
                                    selectedAnswer={selectedAnswers[index]}
                                    correctAnswer={question.answers.find(answer => answer.isCorrect)}
                                />
                            ))}
                        </div>

                        <div className="container d-flex justify-content-center">
                            <button
                                style={{ color: 'black', backgroundColor: 'rgba(181, 212, 205, 1)' }}
                                className="btn btn-success border-0 mt-4 px-4 py-2"
                                onClick={handleOnClick}
                            >
                                КОН КВИЗОВИ
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ResultPage;
