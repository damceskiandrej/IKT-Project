import {ChatDots} from "react-bootstrap-icons";

function CustomAIMessage({ question, explanation }) {
    return (
        <div className="p-4 m-3 rounded-4 shadow-sm bg-white border border-2 border-success-subtle position-relative">
            <div className="d-flex align-items-start gap-3">
                <div className="bg-success-subtle text-success p-2 rounded-circle d-flex align-items-center justify-content-center" style={{ width: 40, height: 40 }}>
                    <ChatDots className="me-1 ms-1" />
                </div>
                <div>
                    <h6 className="fw-bold mb-2">{question}</h6>
                    <p className="text-muted mb-0" style={{ whiteSpace: 'pre-line' }}>
                        {explanation}
                    </p>
                </div>
            </div>
        </div>
    );
}

export default CustomAIMessage;
