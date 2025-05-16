function CustomAnswer({ letter, answer, onClick, isSelected }) {
    return (
        <button
            className={`btn w-100 ${isSelected ? 'btn-outline-secondary' : 'btn-light'} border rounded d-flex align-items-center p-3`}
            onClick={onClick}
            style={{
                backgroundColor: isSelected ? 'rgba(181, 212, 205, 1)' : '',
                borderColor: isSelected ? 'rgba(181, 212, 205, 1)' : '',
                transition: 'all 0.2s ease-in-out'
            }}
        >
            <div className="fw-bold me-3">{letter}.</div>
            <div className="fw-semibold text-start">{answer}</div>
        </button>
    );
}


export default CustomAnswer;