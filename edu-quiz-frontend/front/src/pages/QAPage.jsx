import { useLocation, useNavigate } from 'react-router-dom';
import CustomAIMessage from "../components/CustomAIMessage";
import { useTranslation } from 'react-i18next';
import { generatePdf } from '../api/exportApi';
import {toast, ToastContainer} from "react-toastify";

function QAPage() {
    const { t } = useTranslation();
    const location = useLocation();
    const { summary = [], quizTitle = "", quizId = "", userId = ""} = location.state || {};
    const navigate = useNavigate()
    
    const handleExportQuiz = async () => {
        try {
            toast.success("A PDF Document will be generated shortly");
            const blob = await generatePdf(quizId, userId);
            const url = window.URL.createObjectURL(blob);

            
            const link = document.createElement('a');
            link.href = url;
            link.download = `${quizTitle || "quiz"}.pdf`;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            window.URL.revokeObjectURL(url);
        } catch (error) {
            console.error("Failed to export quiz PDF:", error);
            alert("Export failed. Please try again.");
        }
    };

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
                    <div style={{display: 'flex', justifyContent: 'space-around'}}>
                        <button className="btn btn-dark border-0 mt-4 px-4 py-2" onClick={() => navigate("/quizes")}>
                            КОН КВИЗОВИ
                        </button >
                        <button className="btn btn-dark border-0 mt-4 px-4 py-2" onClick={handleExportQuiz}>
                            ЕКСПОРТ КВИЗ
                        </button>
                    </div>
                    
                </div>
            </div>
            <ToastContainer
                position="bottom-right"
                autoClose={3000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="colored"
            />
        </div>
    );
}

export default QAPage;