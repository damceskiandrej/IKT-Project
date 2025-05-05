function CustomReviewCard({ question, selectedAnswer, correctAnswer, showCorrectness = true }) {
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

            const getClassNames = () => {
              if (showCorrectness) {
                if (isCorrect) return 'bg-success bg-opacity-25';
                if (isIncorrect) return 'text-danger';
              }
              if (isSelected) return 'bg-info bg-opacity-25'; // Highlight selected answer
              return '';
            };

            return (
              <div
                key={index}
                className={`mb-2 p-2 rounded d-flex justify-content-between align-items-center ${getClassNames()}`}
                style={{ maxWidth: "300px" }}
              >
                <span>{String.fromCharCode(65 + index)} {answer.answerText}</span>
                {showCorrectness && (
                  isCorrect && isSelected ? (
                    <span className="text-success">✔️</span>
                  ) : isIncorrect ? (
                    <span className="text-danger">❌</span>
                  ) : null
                )}
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}

export default CustomReviewCard;
