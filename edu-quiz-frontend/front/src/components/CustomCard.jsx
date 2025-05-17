import { useNavigate } from 'react-router-dom';
import CustomButton from './CustomButton';
import { useTranslation } from 'react-i18next';

function CustomCard({
                        id,
                        title,
                        category,
                        questionCount,
                        hideStartButton,
                        onReviewClick,
                        showAISummaryButton,
                        onAISummaryClick
                    }) {
    const navigate = useNavigate();
    const { t } = useTranslation();

    const handleStartQuiz = () => {
        navigate(`/quiz/${id}`);
    };

    return (
        <div className="card shadow-sm border-0 rounded-4 card-hover" style={{ width: "100%", maxWidth: "25em" }}>
        <div className="card-body p-4 d-flex flex-column justify-content-between h-100">
                <div>
                    <h5 className="card-title fw-bold mb-3">{title}</h5>
                    <p className="text-muted mb-1">
                        <i className="bi bi-tags-fill me-1"></i>
                        <strong>{t('Category')}:</strong> {category || t('uncategorized')}
                    </p>
                    <p className="text-muted">
                        <i className="bi bi-question-circle-fill me-1"></i>
                        <strong>{t('Questions')}:</strong> {questionCount}
                    </p>
                </div>

                <div className="d-flex justify-content-center gap-2 flex-wrap mt-3">
                    {!hideStartButton && (
                        <CustomButton onClick={handleStartQuiz} btnText={t('start_quiz')} />
                    )}

                    {typeof onReviewClick === 'function' && hideStartButton && (
                        <button className="btn btn-outline-secondary rounded-pill px-3" onClick={onReviewClick}>
                            {t('review_quiz_btn')}
                        </button>
                    )}

                    {showAISummaryButton && (
                        <button className="btn btn-outline-info rounded-pill px-3" onClick={() => onAISummaryClick(id, title)}>
                            {t('ai_summary_btn')}
                        </button>
                    )}
                </div>
            </div>
        </div>
    );
}

export default CustomCard;
