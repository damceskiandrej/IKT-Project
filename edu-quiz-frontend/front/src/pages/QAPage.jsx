import CustomAIMessage from "../components/CustomAIMessage"
import { useTranslation } from 'react-i18next';

function QAPage() {
    const { t, i18n } = useTranslation(); 
    
    return (
        <div className="container text-center py-5">
            <div className="row justify-content-center p-5">
                <div className="col-md-8">
                    <h2 className="fw-bold text-success mb-4">{t('ai_hints')}</h2>
                    <h4 className="text-success mb-4">{t('ai_description')}</h4>
                    <div className="container pt-4">
                        <CustomAIMessage/>
                        <CustomAIMessage/>
                        <CustomAIMessage/>
                        <CustomAIMessage/>
                        <CustomAIMessage/>
                    </div>
                    <button style={{color: "black", backgroundColor: "rgba(181, 212, 205, 1)"}} className="btn btn-success border-0 mt-4 px-4 py-2">КОН КВИЗОВИ</button>
                </div>
            </div>
        </div>
    )
}

export default QAPage