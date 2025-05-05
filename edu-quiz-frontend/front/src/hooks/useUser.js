import { useState, useEffect } from 'react';

const useUser = () => {
    const [user, setUser] = useState(null);

    useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            setUser(JSON.parse(storedUser));
            
        }
    }, []);
    // console.log(user)
    return user;
};

export default useUser;