import { useNavigate } from 'react-router-dom';
import CustomButton from "../../components/CustomButton";
import CustomCircleButton from '../../components/CustomCircleButton';
import UploadDocument from "@/components/UploadDocument.jsx";
import useUser from "@/hooks/useUser.js";
import { Role } from "@/enum/role.js";
import { useTranslation } from 'react-i18next';

function UserPage() {
    const navigate = useNavigate();
    const user = useUser();
    const isProfessor = user && user.role === Role.PROFESSOR;

    const { t, i18n } = useTranslation(); 

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
                        <UploadDocument btnText={t('add_student')} apiEndpoint="http://localhost:5190/api/Import/ImportStudents" />
                        <UploadDocument btnText={t('add_quiz')} apiEndpoint="http://localhost:5190/api/Import/ImportQuiz" />
                    </>
                )}
                <div className="form-group mb-3" style={{ padding: '2rem' }}>
                    <CustomCircleButton btnText={t('my_quizzes')} onClick={goToMyQuizesPage} />
                </div>
                <div className="form-group mb-3" style={{ padding: '2rem' }}>
                    <CustomCircleButton btnText={t('all_quizzes')} onClick={navigateToAllQuizzes} />
                </div>
                <div className="form-group mb-3" style={{ padding: '2rem' }}>
                    <CustomCircleButton btnText={t('hints')} onClick={navigateToQAPage} />
                </div>
            </div>
        </>

    );
}

export default UserPage;

