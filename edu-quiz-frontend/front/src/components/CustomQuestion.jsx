function CustomQuestion({title, question}) {
    return (
        <div className="container mt-5">
            <div className="border border-2 rounded p-5 text-center">
                <h6 className="text-muted">{title}</h6>
                <h4 className="fw-bold my-4">{question}</h4>
                <p className="text-muted">Прашање 1 / 10</p>
            </div>
        </div>
    )
}

export default CustomQuestion