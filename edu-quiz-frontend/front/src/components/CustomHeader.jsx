
import { useNavigate } from 'react-router-dom';
import useUser from '../hooks/useUser';

import { useTranslation } from 'react-i18next';
import { fetchExternalQuiz } from '../api/quizApi';


function CustomHeader() {
    const user = useUser();
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();

    const handleLogout = () => {
        localStorage.removeItem('user');
        navigate('/login');
    };

    
    return (
        <header className="bg-light py-2">
            <div className="container d-flex justify-content-between align-items-center">
                <a href="/home" className="d-flex align-items-center text-decoration-none">
                    <img src="../../img/logo.png" style={{ width: "85px" }} alt="Logo" />
                </a>

                <nav className="d-flex align-items-center">
                    <ul className="d-flex list-unstyled mb-0 me-4">
                        <li className="ms-4"><a href="/home" className="nav-link-custom">{t('header_option_1')}</a></li>
                        <li className="ms-4"><a href="/about" className="nav-link-custom">{t('header_option_2')}</a></li>
                        <li className="ms-4"><a href="/quizes" className="nav-link-custom">{t('header_option_3')}</a></li>
                        <li className="ms-4">
                            {user ? (
                                <a href="/profile" className="nav-link-custom">
                                    {user.firstName.toUpperCase()} {user.lastName.toUpperCase()}
                                </a>
                            ) : (
                                <a href="/login" className="text-dark text-decoration-none">{t('header_option_5')}</a>
                            )}
                        </li>
                    </ul>

                    {user && (
                        <button onClick={handleLogout} className="btn btn-danger">
                            Logout
                        </button>
                    )}
                </nav>
            </div>
        </header>
    );
}

export default CustomHeader;