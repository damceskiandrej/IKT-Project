function CustomAnswer({ letter, answer, onClick, isSelected }) {
    return (
        <button
            className={`btn container mt-3 ${isSelected ? 'btn-outline-secondary' : ''}`}
            onClick={onClick}
            style={{
                backgroundColor: isSelected ? 'rgba(181, 212, 205, 1)' : '',
                borderColor: isSelected ? 'rgba(181, 212, 205, 1)' : '',
            }}
        >
            <div className="border rounded p-3 d-flex align-items-center">
                <div className="fw-bold me-3">{letter}</div>
                <div className="fw-bold">{answer}</div>
            </div>
        </button>
    );
}

export default CustomAnswer;