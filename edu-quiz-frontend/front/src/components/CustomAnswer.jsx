function CustomAnswer({letter, answer}) {
    return (
        <button className="btn btn-outline container mt-3">
            <div className="border rounded p-3 d-flex align-items-center">
                <div className="fw-bold me-3">{letter}</div>
                <div className="fw-bold">{answer}</div>
            </div>
        </button>
    )
}

export default CustomAnswer