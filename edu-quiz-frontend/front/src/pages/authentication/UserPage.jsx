import { useNavigate } from 'react-router-dom'; // Import useNavigate from react-router-dom
import CustomButton from "../../components/CustomButton";

function UserPage() {
    const navigate = useNavigate(); // Initialize the navigate hook

    // Function to handle the "Твои квизови" button click
    const goToMyQuizesPage = () => {
        navigate('/myQuizesPage'); // Navigate to the MyQuizesPage route
    };

    return (
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
            <div className="form-group mb-3">
                <CustomButton btnText={"Твои квизови"} onClick={goToMyQuizesPage} /> {/* Add onClick to navigate */}
            </div>
            <div className="form-group mb-3">
                <CustomButton btnText={"Сите квизови"} />
            </div>
            <div className="form-group mb-3">
                <CustomButton btnText={"Добиени совети"} />
            </div>
        </div>
    );
}

export default UserPage;
