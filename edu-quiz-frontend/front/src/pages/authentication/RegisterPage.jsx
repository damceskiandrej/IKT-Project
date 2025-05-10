import {useState} from 'react';
import {useNavigate} from "react-router-dom";
import {registerUser} from '@/api/authApi.js';
import AnimationWave from "@/components/AnimationWave.jsx";

function RegisterPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!email || !password || !firstName || !lastName) {
            setError('Сите полиња се задолжителни');
            return;
        }

        try {
            const result = await registerUser({
                email,
                password,
                firstName,
                lastName,
                username: email, // you can adjust this
            });
            console.log('Registration successful:', result);
            navigate('/login');
        } catch (err) {
            setError('Регистрацијата не беше успешна. Обидете се повторно.');
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
                <div style={{ flex: 1, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                   <AnimationWave/>
                </div>

                <div style={{ flex: 1, paddingLeft: '40px', display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
                    <div style={{textAlign: 'center'}}>
                        <h2 style={{ color: '#1f8b6c', marginBottom: '10px' }}>EDUQUIZ</h2>
                        <p style={{ marginBottom: '25px', fontWeight: '600' }}>
                            Креирајте го вашиот профил за да продолжите кон квизовите
                        </p>
                    </div>
                    <form onSubmit={handleSubmit} style={{ background: '#F0F0F0', padding: '1.5rem' }}>
                        <input
                            type="text"
                            placeholder="Име"
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                            required
                            style={inputStyle}
                        />
                        <input
                            type="text"
                            placeholder="Презиме"
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                            required
                            style={inputStyle}
                        />
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
                            Регистрирај се!
                        </button>
                    </form>
                    <a href="/login" style={{ fontSize: '14px', color: '#555', textDecoration: 'underline', marginTop: '1.5rem' }}>
                        Веќе имате корисничка сметка? Логирајте се!
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

export default RegisterPage;