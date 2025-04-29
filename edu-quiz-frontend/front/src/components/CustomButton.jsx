function CustomButton({btnText, onClick}) {
    return ( 
            <button 
                className='btn btn-dark' 
                style={{width: "12em"}} 
                onClick={onClick}
            >
                {btnText}
            </button> 
        
        )
}

export default CustomButton