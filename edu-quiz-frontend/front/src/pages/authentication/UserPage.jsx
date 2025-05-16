import {useNavigate} from 'react-router-dom';
import CustomCircleButton from '../../components/CustomCircleButton';
import UploadDocument from "@/components/UploadDocument.jsx";
import useUser from "@/hooks/useUser.js";
import {Role} from "@/enum/role.js";
import AnimationComputer from "@/components/AnimationComputer.jsx";
import {toast, ToastContainer} from "react-toastify";
import { useTranslation } from 'react-i18next';


function UserPage() {
    const navigate = useNavigate();
    const user = useUser();

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

    const showToast = (message) => {
        toast.success(message);
    };

    return (
        <div
            className="container-fluid"
            style={{ height: '100vh', display: 'flex', alignItems: 'center' }}
        >
            <div className="row w-100 h-100">
                <div className="col-6 d-flex flex-column justify-content-center gap-3">
                    <div className="row mb-5">
                        <div
                            style={{
                                textAlign: 'center',
                                fontWeight: 'bold',
                                fontSize: '3rem',
                                color: '#368278',
                            }}
                        >
                            {user?.firstName?.toUpperCase()} {user?.lastName?.toUpperCase()}
                        </div>
                    </div>

                    <div className="row mb-4">
                        <div className="col-12 d-flex justify-content-center gap-5">
                            <CustomCircleButton btnText={t('my_quizzes')} onClick={goToMyQuizesPage} />
                            <CustomCircleButton btnText={t('all_quizzes')} onClick={navigateToAllQuizzes} />
                        </div>
                    </div>
                    {user?.role === 'professor' && (
                        <div className="d-flex justify-content-center gap-5 mt-3">
                            <UploadDocument
                                btnText={t('add_student')}
                                apiEndpoint="http://localhost:5190/api/Import/ImportStudents"
                                successText="Успешно додадени студенти!"
                                showToast={showToast}
                            />
                            <UploadDocument
                                btnText={t('add_quiz')}
                                apiEndpoint="http://localhost:5190/api/Import/ImportQuiz"
                                successText="Успешно додадени квизови!"
                                showToast={showToast}
                            />
                        </div>
                    )}
                </div>

                <div className="col-6 d-flex justify-content-center align-items-center">
                    <AnimationComputer />
                </div>
            </div>

            <ToastContainer
                position="bottom-right"
                autoClose={3000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="colored"
            />
        </div>
    );
}

export default UserPage;