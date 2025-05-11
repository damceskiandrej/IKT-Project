import {useLocation, useNavigate} from "react-router-dom";
import CustomButton from "../../components/CustomButton";
import { useTranslation } from 'react-i18next';
import {useEffect} from "react";
import {toast, ToastContainer} from "react-toastify";


function HomePage() {
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        if (location.state?.showToast) {
            toast.success('Добредојдовте!');
        }
    }, [location.state]);

    const onClickToQuizzes = () => {
        navigate("/quizes")
    }

    const onClickToAboutUs = () => {
        navigate("/about")
    }

    const { t, i18n } = useTranslation();

    return (
        <div className="d-flex justify-content-center align-items-center min-vh-100 bg-gray-100">
            <div className="container py-5">
                <div className="row align-items-center">
                    {/* Text Section */}
                    <div className="col-md-6 mb-4 mb-md-0 text-center">
                        <h1 className="display-4 text-success fw-bold mb-5">EDUQUIZ</h1>
                        <p className="fs-4 fst-italic text-success">{t('home_title')}</p>
                        <div className="container d-flex justify-content-around mt-5">
                            <div className="mt-4 d-flex justify-content-center">
                                <CustomButton btnText={t('start_quiz')} onClick={onClickToQuizzes}/>
                            </div>
                            <div className="mt-4 d-flex justify-content-center">
                                <CustomButton btnText={t('learn_more')} onClick={onClickToAboutUs} />
                            </div>
                        </div>

                    </div>
                    <div className="col-md-6 text-center">
                        <img
                            src="/img/authentication.png"
                            alt="Student studying"
                            className="img-fluid"
                            style={{ maxWidth: "400px" }}
                        />
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
        </div>
    );
}


export default HomePage;