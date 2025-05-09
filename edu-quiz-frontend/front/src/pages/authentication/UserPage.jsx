import { useNavigate } from 'react-router-dom';
import CustomButton from "../../components/CustomButton";
import CustomCircleButton from '../../components/CustomCircleButton';
import UploadDocument from "@/components/UploadDocument.jsx";
import useUser from "@/hooks/useUser.js";
import { Role } from "@/enum/role.js";

function UserPage() {
    const navigate = useNavigate();
    const user = useUser();
    const isProfessor = user && user.role === Role.PROFESSOR;

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
            <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
                {isProfessor && (
                    <>
                        <UploadDocument btnText={"Додади студенти"} apiEndpoint="http://localhost:5190/api/Import/ImportStudents" />
                        <UploadDocument btnText={"Додади квиз"} apiEndpoint="http://localhost:5190/api/Import/ImportQuiz" />
                    </>
                )}
                <div className="form-group mb-3" style={{ padding: '2rem' }}>
                    <CustomCircleButton btnText={"Твои квизови"} onClick={goToMyQuizesPage} />
                </div>
                <div className="form-group mb-3" style={{ padding: '2rem' }}>
                    <CustomCircleButton btnText={"Сите квизови"} onClick={navigateToAllQuizzes} />
                </div>
                <div className="form-group mb-3" style={{ padding: '2rem' }}>
                    <CustomCircleButton btnText={"Добиени совети"} onClick={navigateToQAPage} />
                </div>
            </div>
        </>

    );
}

export default UserPage;

