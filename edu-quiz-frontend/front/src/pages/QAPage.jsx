import { useLocation, useNavigate } from 'react-router-dom';
import CustomAIMessage from "../components/CustomAIMessage";
import { useTranslation } from 'react-i18next';

function QAPage() {
    const { t } = useTranslation();
    const location = useLocation();
    const { summary = [], quizTitle = "" } = location.state || {};

    return (
        <div className="container text-center py-5">
            <div className="row justify-content-center p-5">
                <div className="col-md-8">

                    <h2 className="text-success mb-4">{quizTitle}</h2> 
                    <div className="container pt-4 bg-light">
                        {summary.map((item, index) => (
                            <CustomAIMessage
                                key={index}
                                question={item.question}
                                explanation={item.explanation}
                            />
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
