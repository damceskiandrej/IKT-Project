
const CustomCircleButton = ({ btnText ,onClick, disabled }) => {

    const style = {
        width: "150px",
        height: "150px",
        borderRadius: "50%",
        padding: "0",
        fontSize: "1.3rem",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        textAlign: "center",
        whiteSpace: "normal", 
        lineHeight: "1.1",
        overflow: "hidden",   
        wordBreak: "break-word", 
        backgroundColor: "#C3EFCF",
        fontWeight: "bold",
        color: "#094E3F",
        border: "none", 
      };

    return (
        <button
            className={`btn`}
            disabled={disabled}
            style={style}
            onClick={onClick}
        >
            {btnText}
        
        </button>
    );
};

export default CustomCircleButton;
