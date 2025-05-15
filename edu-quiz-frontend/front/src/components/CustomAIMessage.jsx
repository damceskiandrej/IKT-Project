function CustomAIMessage({ question, explanation }) {
    return (
        <div className="mb-3 text-start">
            <p className="text-dark p-0 m-0"><strong>{question}</strong></p>
            <p className="text-secondary">{explanation}</p>
        </div>
    );
}

export default CustomAIMessage;
