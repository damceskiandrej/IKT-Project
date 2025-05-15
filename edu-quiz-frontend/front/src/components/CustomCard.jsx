import { useNavigate } from 'react-router-dom';
import CustomButton from './CustomButton';
import { useTranslation } from 'react-i18next';

function CustomCard({ id, title, category, questionCount, hideStartButton, onReviewClick, showAISummaryButton, onAISummaryClick }) {
    const navigate = useNavigate();
    const {t} = useTranslation()
    const handleStartQuiz = () => {
        navigate(`/quiz/${id}`);
    };

    return (
        <div className="card border-0" style={{ width: "25em" }}>
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

                {/* Button group horizontally aligned */}
                <div className="d-flex justify-content-center gap-2 mt-4 flex-wrap">
                    {!hideStartButton && (
                        <CustomButton onClick={handleStartQuiz} btnText={t('start_quiz')} />
                    )}

                    {typeof onReviewClick === 'function' && hideStartButton && (
                        <button className="btn btn-dark" onClick={onReviewClick}>
                            {t('review_quiz_btn')}
                        </button>
                    )}

                    {showAISummaryButton && (
                        <button className="btn btn-info" onClick={() => onAISummaryClick(id,title)}>
                            {t('ai_summary_btn')}
                        </button>
                    )}
                </div>
            </div>
        </div>
    );
}

export default CustomCard;
