import { useState } from 'react';
import CustomButton from "../../components/CustomButton";
import { registerUser } from '../../api/authApi'; // Import the registerUser function

function RegisterPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [username, setUsername] = useState('');
    const [error, setError] = useState('');

    // Handle form submission
    const handleSubmit = async (e) => {
        e.preventDefault();

        // Basic validation
        if (!email || !password || !firstName || !lastName || !username) {
            setError('All fields are required');
            return;
        }

        // Prepare user data
        const userData = {
            email,
            password,
            firstName,
            lastName,
            username,
        };

        try {
            // Call the registerUser API function
            const result = await registerUser(userData);

            // Handle successful registration (e.g., redirect or show success message)
            console.log('Registration successful:', result);
            // Redirect user or show success message
        } catch (err) {
            setError('Failed to register. Please try again.');
        }
    };

    return (
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '40vh' }}>
            <form onSubmit={handleSubmit} style={{ width: '100%', maxWidth: '400px' }}>
                <h2 className="text-center mb-4">Register</h2>
                {error && <div className="alert alert-danger mb-3">{error}</div>}
                <div className="form-group mb-3">
                    <label htmlFor="email">Email address</label>
                    <input
                        type="email"
                        className="form-control"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="Enter email"
                    />
                </div>
                <div className="form-group mb-3">
                    <label htmlFor="password">Password</label>
                    <input
                        type="password"
                        className="form-control"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Password"
                    />
                </div>
                <div className="form-group mb-3">
                    <label htmlFor="firstName">First Name</label>
                    <input
                        type="text"
                        className="form-control"
                        id="firstName"
                        value={firstName}
                        onChange={(e) => setFirstName(e.target.value)}
                        placeholder="First Name"
                    />
                </div>
                <div className="form-group mb-3">
                    <label htmlFor="lastName">Last Name</label>
                    <input
                        type="text"
                        className="form-control"
                        id="lastName"
                        value={lastName}
                        onChange={(e) => setLastName(e.target.value)}
                        placeholder="Last Name"
                    />
                </div>
                <div className="form-group mb-3">
                    <label htmlFor="username">Username</label>
                    <input
                        type="text"
                        className="form-control"
                        id="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        placeholder="Username"
                    />
                </div>
                <CustomButton btnText={"Регистрирај се"} />
            </form>
        </div>
    );
}

export default RegisterPage;
