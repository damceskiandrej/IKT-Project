function CustomReviewCard({ question, selectedAnswer, correctAnswer}) {

  // console.log("selectedAnswer",selectedAnswer)
  return (
    <div className="container p-3">
      <div className="border rounded p-3">
        <div className="d-flex align-items-start mb-3">
 
          <h6 className="mb-0">{question.questionText}</h6>
        </div>

        <div className="d-flex flex-column">
          {question.answers.map((answer, index) => {
            const isSelected = selectedAnswer === answer.answerText
            const isCorrect = answer.isCorrect;
            const isIncorrect = isSelected && !isCorrect;

            return (
              <div
                key={index}
                className={`mb-2 p-2 rounded d-flex justify-content-between align-items-center ${isCorrect ? 'bg-success bg-opacity-25' : ''} ${isIncorrect ? 'text-danger' : ''}`}
                style={{ maxWidth: "300px" }}
              >
                <span>{String.fromCharCode(65 + index)} {answer.answerText}</span>
                {isCorrect && isSelected ? (
                  <span className="text-success">✔️</span> 
                ) : isIncorrect ? (
                  <span className="text-danger">❌</span> 
                ) : null}
              </div>
            );
          })}
        </div>
      </div>
    </div>
  )
}

export default CustomReviewCard;
