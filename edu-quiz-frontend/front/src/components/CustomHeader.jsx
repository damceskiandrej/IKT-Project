import React from 'react';
import { useNavigate } from 'react-router-dom';
import useUser from '../hooks/useUser';

function CustomHeader() {
    const user = useUser();
    const navigate = useNavigate();

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
                        <li className="ms-4"><a href="/home" className="text-dark text-decoration-none">ПОЧЕТНА</a></li>
                        <li className="ms-4"><a href="/about" className="text-dark text-decoration-none">ЗА НАС</a></li>
                        <li className="ms-4"><a href="/quizes" className="text-dark text-decoration-none">КВИЗОВИ</a></li>
                        <li className="ms-4">
                            {user ? (
                                <a href="/profile" className="text-dark fw-bold">
                                    {user.firstName} {user.lastName}
                                </a>
                            ) : (
                                <a href="/login" className="text-dark text-decoration-none">ПРОФИЛ</a>
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