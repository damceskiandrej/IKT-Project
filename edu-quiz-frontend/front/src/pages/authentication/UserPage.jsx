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
            <div className="container">
            <div className="row mt-5">
                <div className="col-6">
                    <div className="row mb-5">
                        <div style={{ textAlign: 'center', fontWeight: 'bold', fontSize: '2rem', paddingTop: '2rem', color: '#368278' }}>
                            {user?.firstName?.toUpperCase()} {user?.lastName?.toUpperCase()}
                        </div>
                    </div>
                    <div className="row mb-4">
                        <div className="col-4 d-flex justify-content-center">
                            <CustomCircleButton btnText={"ТВОИ КВИЗОВИ"} onClick={goToMyQuizesPage} />
                        </div>
                        <div className="col-4 d-flex justify-content-center">
                            <CustomCircleButton btnText={"СИТЕ КВИЗОВИ"} onClick={navigateToAllQuizzes} />
                        </div>
                        <div className="col-4 d-flex justify-content-center">
                            <CustomCircleButton btnText={"ДОБИЕНИ СОВЕТИ"} onClick={navigateToQAPage} />
                        </div>
                        {isProfessor && (
                            <div className="d-flex justify-content-center gap-5 mt-3">
                                <UploadDocument btnText={"ДОДАДИ СТУДЕНТИ"} apiEndpoint="http://localhost:5190/api/Import/ImportStudents" />
                                <UploadDocument btnText={"ДОДАДИ КВИЗ"} apiEndpoint="http://localhost:5190/api/Import/ImportQuiz" />
                            </div>
                        )}
                    </div>
                </div>

                <div className="col-6 d-flex justify-content-center align-items-center">
                    <img src="/img/authentication.png" alt="Illustration" style={{ maxWidth: '50%', height: 'auto' }} />
                </div>
            </div>
            </div>
        </>

    );
}

export default UserPage;

