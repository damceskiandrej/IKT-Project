import {useState} from "react";
import {useNavigate} from "react-router-dom";
import {loginUser} from "@/api/authApi.js";
import AnimationWave from "@/components/AnimationWave.jsx";

function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const result = await loginUser({ email, password });
            localStorage.setItem('user', JSON.stringify(result));
            navigate('/quizes');
        } catch (err) {
            setError(err.message || 'Најавувањето не беше успешно.');
        }
    };

    return (
        <div style={{
            backgroundColor: '#f3f3f3',
            minHeight: '100vh',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            padding: '40px 16px'
        }}>
            <div style={{
                backgroundColor: '#fff',
                maxWidth: 1000,
                width: '100%',
                minHeight: '50vh',
                alignItems: 'center',
                display: 'flex',
                flexWrap: 'wrap',
                padding: '40px',
                borderRadius: '10px',
                boxShadow: '0 0 20px rgba(0,0,0,0.05)'
            }}>
                {/* Illustration */}
                <div style={{
                    flex: '1 1 300px',
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    padding: '10px'
                }}>
                    {/*<img*/}
                    {/*    src="/img/authentication.png"*/}
                    {/*    alt="Login illustration"*/}
                    {/*    style={{ width: '80%', maxWidth: 400 }}*/}
                    {/*/>*/}
                    <AnimationWave/>
                </div>

                {/* Login Form */}
                <div style={{
                    flex: '1 1 300px',
                    padding: '10px 40px',
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'center'
                }}>
                    <div style={{ textAlign: 'center' }}>
                        <h2 style={{ color: '#1f8b6c', marginBottom: '10px' }}>EDUQUIZ</h2>
                        <p style={{ marginBottom: '25px', fontWeight: '600' }}>
                            Најавете се за да продолжите кон квизовите
                        </p>
                    </div>

                    <form onSubmit={handleLogin} style={{ background: '#F0F0F0', padding: '1.5rem', borderRadius: '10px' }}>
                        <input
                            type="email"
                            placeholder="Емаил адреса"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            style={inputStyle}
                        />
                        <input
                            type="password"
                            placeholder="Лозинка"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            style={inputStyle}
                        />
                        {error && <div style={{ color: 'red', marginBottom: '10px' }}>{error}</div>}

                        <button type="submit" style={buttonStyle}>
                            Најави се!
                        </button>
                    </form>

                    <a href="/register" style={{ fontSize: '14px', color: '#555', textDecoration: 'underline', marginTop: '1.5rem' }}>
                        Немате профил? Регистрирајте се!
                    </a>
                </div>
            </div>
        </div>
    );
}

const inputStyle = {
    width: '100%',
    padding: '12px',
    marginBottom: '25px',
    borderRadius: '5px',
    border: '1px solid #ccc',
    fontSize: '14px',
    backgroundColor: '#D9D9D9'
};

const buttonStyle = {
    width: '100%',
    padding: '12px',
    backgroundColor: '#222',
    color: '#fff',
    fontWeight: 'bold',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer'
};

export default LoginPage;