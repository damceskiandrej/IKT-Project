import CustomCard from './CustomCard'

function CustomCardsList({ quizzes }) {
    if (quizzes.length === 0) {
        return (
            <div className="text-center my-5">
                No quizzes available at the moment
            </div>
        );
    }
    return (
        <div className="container my-4">
            <div className="row g-4">
                {quizzes.map((quiz) => (
                    <div className="col-md-4" key={quiz.id}>
                        <CustomCard 
                            title={quiz.title}
                            category={quiz.category}
                            questionCount={quiz.questionCount}
                        />
                    </div>
                ))}
            </div>
        </div>
    )
}

export default CustomCardsList 