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

    const showToast = (message) => {
        toast.success(message);
    };

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
                            <CustomCircleButton btnText={t('my_quizzes')} onClick={goToMyQuizesPage} />
                        </div>
                        <div className="col-4 d-flex justify-content-center">
                            <CustomCircleButton btnText={t('all_quizzes')} onClick={navigateToAllQuizzes} />
                        </div>
                        <div className="col-4 d-flex justify-content-center">
                            <CustomCircleButton btnText={t('hints')} onClick={navigateToQAPage} />
                        </div>
                        {isProfessor && (
                            <div className="d-flex justify-content-center gap-5 mt-3">
                                <UploadDocument btnText={t('add_student')}
                                                apiEndpoint="http://localhost:5190/api/Import/ImportStudents"
                                                successText="Усшено додадени студенти!"
                                                showToast={showToast}/>
                                <UploadDocument btnText={t('add_quiz')}
                                                apiEndpoint="http://localhost:5190/api/Import/ImportQuiz"
                                                successText="Усшено додадени квизови!"
                                                showToast={showToast}/>
                            </div>
                        )}
                    </div>
                </div>

                <div className="col-6 d-flex justify-content-center align-items-center">
                    {/*<img src="/img/authentication.png" alt="Illustration" style={{ maxWidth: '50%', height: 'auto' }} />*/}
                    <AnimationComputer/>
                </div>
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
        </>

    );
}

export default UserPage;

