import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { loginUser } from "../../api/authApi";
import CustomButton from "../../components/CustomButton";

function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const [user, setUser] = useState(null);  
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const loginData = { email, password };
            const result = await loginUser(loginData);
    
            // Save user data in localStorage
            
            localStorage.setItem('user', JSON.stringify(result));
            console.log(result)
    
            // Redirect
            navigate('/quizes');
        } catch (err) {
            console.error(err);
            setError(err.message || 'Login failed');
        }
    };
    

    return (
        <form onSubmit={handleLogin} className="d-flex flex-column align-items-center">
            <div className="form-group col-6 mb-3">
                <label>Email address</label>
                <input 
                    type="email" 
                    className="form-control" 
                    placeholder="Enter email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
            </div>
            <div className="form-group col-6 mb-3">
                <label>Password</label>
                <input 
                    type="password" 
                    className="form-control" 
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
            </div>
            {error && <div className="text-danger mb-3">{error}</div>}
            <CustomButton btnText={"Login"} />

            
            {user && (
                <div className="mt-3">
                    <p>Welcome, {user.firstName} {user.lastName}!</p>
                    <p>Email: {user.email}</p>
                </div>
            )}
        </form>
    );
}

export default LoginPage;
