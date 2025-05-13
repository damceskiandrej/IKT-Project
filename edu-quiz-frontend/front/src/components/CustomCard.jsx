import { useNavigate } from 'react-router-dom'
import CustomButton from './CustomButton'

function CustomCard({ id, title, category, questionCount, hideStartButton, onReviewClick}) {

    const navigate = useNavigate()

    const handleStartQuiz = () => {
        navigate(`/quiz/${id}`)
    }

    return (
        <div className="card border-0" style={{width: "25em"}}>
            <div className="card-body p-4">
                <h4 className="card-title">{title}</h4>
                <div className="card-text">
                    <div className="mb-2">
                        <strong>Category:</strong> {category || 'Uncategorized'}
                    </div>
                    <div>
                        <strong>Questions:</strong> {questionCount}
                    </div>
                </div>
                {!hideStartButton && (
                    <div className="d-flex justify-content-center mt-3">
                        <CustomButton onClick={handleStartQuiz} btnText={"ПОЧНИ КВИЗ"} />
                    </div>
                )}
                {typeof onReviewClick === 'function' && hideStartButton && (
                    <button
                        className="btn btn-outline-success"
                        onClick={onReviewClick}
                    >
                        Review Quiz
                    </button>
                )}

            </div>
        </div>
    )
}

export default CustomCard