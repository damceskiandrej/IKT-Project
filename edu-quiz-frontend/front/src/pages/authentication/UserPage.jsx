import { useNavigate } from 'react-router-dom'; 
import CustomButton from "../../components/CustomButton";
import CustomCircleButton from '../../components/CustomCircleButton';

function UserPage() {
    const navigate = useNavigate(); 

   
    const goToMyQuizesPage = () => {
        navigate('/myQuizesPage'); 
    };

    const navigateToAllQuizzes = () => {
        navigate("/quizes")
    }

    const navigateToQAPage = () => {
        navigate("/qaPage")
    }

    return (
    <>
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '40vh'  }}>
            <div className="form-group mb-3" style={{padding: '2rem'}}>
                <CustomCircleButton btnText={"Твои квизови"} onClick={goToMyQuizesPage}/>  
            </div>
            <div className="form-group mb-3" style={{padding: '2rem'}}>
                <CustomCircleButton btnText={"Сите квизови"} onClick={navigateToAllQuizzes}/>  
            </div>
            <div className="form-group mb-3" style={{padding: '2rem'}}>
                <CustomCircleButton btnText={"Добиени совети"} onClick={navigateToQAPage}/>  
            </div>
        </div>
    </>
        
    );
}

export default UserPage;

