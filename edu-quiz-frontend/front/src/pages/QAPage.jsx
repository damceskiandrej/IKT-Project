import { useLocation, useNavigate } from 'react-router-dom';
import CustomAIMessage from "../components/CustomAIMessage";
import { useTranslation } from 'react-i18next';

function QAPage() {
    const { t } = useTranslation();
    const location = useLocation();
    const { summary = [], quizTitle = "" } = location.state || {};



    console.log("Summary", summary);

    return (
        <div className="container text-center py-5">
            <div className="row justify-content-center p-5">
                <div className="col-md-8">

                    <h2 className="text-success mb-4">{quizTitle}</h2> 
                    <div className="container p-4">
                        {summary.map((item, index) => (
                            item.isProcessed ? (
                                <CustomAIMessage
                                    key={index}
                                    question={item.question}
                                    explanation={item.explanation}
                                />
                            ) : (
                                <div key={index} className="alert alert-info my-3 p-4 rounded shadow-sm">
                                    <h5 className="mb-2">{item.question}</h5>
                                    <p className="mb-0">
                                        🧠 {t('aiPreparingSummary') || "AI is still preparing the summary. Please check back soon."}
                                    </p>
                                </div>
                            )
                        ))}
                    </div>
                    <button style={{ color: "black", backgroundColor: "rgba(181, 212, 205, 1)" }} className="btn btn-success border-0 mt-4 px-4 py-2">
                        КОН КВИЗОВИ
                    </button>
                </div>
            </div>
        </div>
    );
}

export default QAPage;
