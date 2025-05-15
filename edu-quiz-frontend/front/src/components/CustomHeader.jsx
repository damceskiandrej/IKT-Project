import { useState, useRef, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useUser from '../hooks/useUser';
import { useTranslation } from 'react-i18next';

function CustomHeader() {
    const user = useUser();
    const navigate = useNavigate();
    const { t } = useTranslation();

    const [dropdownOpen, setDropdownOpen] = useState(false);
    const dropdownRef = useRef(null);

    const handleLogout = () => {
        localStorage.removeItem('user');
        navigate('/login');
    };

    const goToProfilePage = () => {
        navigate("/profile")
    }

    const toggleDropdown = () => {
        setDropdownOpen(prev => !prev);
    };


    useEffect(() => {
        function handleClickOutside(event) {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setDropdownOpen(false);
            }
        }
        document.addEventListener("mousedown", handleClickOutside);
        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, []);


    return (
        <header className="bg-light py-2">
            <div className="container d-flex justify-content-between align-items-center">
                <a href="/home" className="d-flex align-items-center text-decoration-none">
                    <img src="img\logo.png" style={{ width: "85px" }} alt="Logo" />
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
                        <>
                            <div className="position-relative" ref={dropdownRef}>
                               
                                <button
                                    onClick={toggleDropdown}
                                    className="btn p-0 border-0 rounded-circle overflow-hidden"
                                    style={{ width: "40px", height: "40px", cursor: "pointer" }}
                                    aria-haspopup="true"
                                    aria-expanded={dropdownOpen}
                                >
                                    <img
                                        src="img\student.png" 
                                        alt="Student"
                                        style={{ width: "100%", height: "100%", objectFit: "cover" }}
                                    />
                                </button>

                            
                                {dropdownOpen && (
                                    <div
                                        className="dropdown-menu show shadow"
                                        style={{ right: 0, left: "auto", minWidth: "150px" }}
                                    >
                                        <button
                                            className="dropdown-item"
                                            onClick={() => {
                                                navigate('/goToProfilePage');
                                                setDropdownOpen(false);
                                            }}
                                        >
                                            {t('profile')}
                                        </button>

                                        <button
                                            className="dropdown-item"
                                            onClick={() => {
                                                navigate('/myQuizesPage');
                                                setDropdownOpen(false);
                                            }}
                                        >
                                            {t('my_quizzes')}
                                        </button>
                                        
                                        <button
                                            className="dropdown-item"
                                            onClick={() => {
                                                navigate('/quizes');
                                                setDropdownOpen(false);
                                            }}
                                        >
                                            {t('all_quizzes')}
                                        </button>
                                        <button
                                            className="dropdown-item text-danger"
                                            onClick={() => {
                                                handleLogout();
                                                setDropdownOpen(false);
                                            }}
                                        >
                                            Logout
                                        </button>
                                    </div>
                                )}
                            </div>
                        </>
                    )}
                </nav>
            </div>
        </header>
    );
}

export default CustomHeader;
