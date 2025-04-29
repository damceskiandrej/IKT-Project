import { useNavigate } from "react-router-dom";
import CustomButton from "../../components/CustomButton";

function HomePage() {
    const navigate = useNavigate(); 

    return (
        <div className="container d-flex flex-column justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
            <div className="mb-3">
                <CustomButton 
                    btnText={"Login"} 
                    onClick={() => navigate("/login")}
                />
            </div>
            <div>
                <CustomButton 
                    btnText={"Register"} 
                    onClick={() => navigate("/register")} 
                />
            </div>
        </div>
    )
}

export default HomePage;
