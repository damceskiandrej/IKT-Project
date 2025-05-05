function CustomButton({btnText, onClick, disabled}) {
    return ( 
            <button 
                className='btn btn-dark' 
                style={{width: "12em"}} 
                onClick={onClick}
                disabled={disabled}
            >
                {btnText}
            </button> 
        
        )
}

export default CustomButton