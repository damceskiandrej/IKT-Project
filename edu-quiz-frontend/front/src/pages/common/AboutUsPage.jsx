import { useNavigate } from "react-router-dom"
import { useTranslation } from 'react-i18next';

function AboutUsPage() {
     
    const navigate = useNavigate()
    const handleOnClick = () => {
        navigate("/quizes")
    }

    const { t, i18n } = useTranslation();

    return (
        <div className="container text-center py-5">
            <div className="row justify-content-center p-5">
                <div className="col-md-8">
                    <h2 className="fw-bold text-success mb-4">EDUQUIZ</h2>
                    <p className="lead">
                        {t('about_us')}
                    </p>
                    <button 
                            className="btn btn-dark border-0 mt-4 px-4 py-2"
                            onClick={handleOnClick}
                    >{t('go_to_quizzes')}</button>
                </div>
            </div>
        </div>
    )
}

export default AboutUsPage