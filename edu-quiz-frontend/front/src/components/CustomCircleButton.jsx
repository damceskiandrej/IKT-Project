const CustomCircleButton = ({ btnText, onClick, disabled }) => {
    return (
        <button
            className="circle-btn"
            disabled={disabled}
            onClick={onClick}
        >
            {btnText}
        </button>
    );
};

export default CustomCircleButton;
