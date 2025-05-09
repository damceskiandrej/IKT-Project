
const CustomCircleButton = ({ btnText ,onClick }) => {

    const style = {
        width: "150px",
        height: "150px",
        borderRadius: "50%",
        padding: "0",
        fontSize: "2rem",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        textAlign: "center",
        whiteSpace: "normal", 
        lineHeight: "1.1",
        overflow: "hidden",   
        wordBreak: "break-word", 
        backgroundColor: "rgba(60, 141, 123, 1)", 
        color: "#fff", 
        border: "none", 
      };

    return (
        <button
            className={`btn`}

            style={style}
            onClick={onClick}
        >
            {btnText}
        
        </button>
    );
};

export default CustomCircleButton;
