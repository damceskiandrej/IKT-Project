function CustomQuestion({ title, question, currentQuestionNumber, totalQuestions }) {
    return (
        <div className="border border-2 rounded p-4 mb-4 text-center">
            <h6 className="text-muted mb-2">{title}</h6>
            <h4 className="fw-bold mb-3">{question}</h4>
            <p className="text-muted mb-0">Прашање {currentQuestionNumber} / {totalQuestions}</p>
        </div>
    );
}


export default CustomQuestion;