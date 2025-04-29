
const URL = "http://localhost:5190/api/Authentication"

export const loginUser = async (loginData) => {
    try {
        const response = await fetch(`${URL}/Login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Login failed');
        }

        const data = await response.json();
        return data;
    } catch (error) {
        throw new Error(error.message || 'Login failed');
    }
};


export const registerUser = async (userData) => {
    try {
        const response = await fetch(`${URL}/Register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });

        if (!response.ok) {
            const errorData = await response.json()
            throw new Error(errorData.message || 'Failed to register');
        }
        
        const data = await response.json();
        return data; 
    } catch (error) {
        console.error('Error during registration:', error);
        throw error; 
    }
};


