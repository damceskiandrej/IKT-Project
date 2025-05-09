import { useEffect, useState } from 'react';
import CustomCardsList from '../../components/CustomCardsList';
import { getQuizzesByUser } from '../../api/quizApi'; // Assuming you have this function in quizApi.js
import useUser from '../../hooks/useUser';

function MyQuizesPage() {
    const [quizzes, setQuizzes] = useState([]); // State to store quizzes
    const [loading, setLoading] = useState(true); // State to manage loading state
    const [error, setError] = useState(null); // State to manage error messages
    const user = useUser()
    
    const userId = user ? user.userId : "";
    

    useEffect(() => {

        if (!userId) return;

        const fetchQuizzes = async () => {
            try {
                const data = await getQuizzesByUser(userId);
                setQuizzes(data); // Set the fetched quizzes to state
                setLoading(false); // Set loading to false after fetching
            } catch (error) {
                setError(error.message); // Set the error message if something goes wrong
                setLoading(false); // Set loading to false even on error
            }
        };

        fetchQuizzes(); // Fetch quizzes when component mounts
    }, [userId]); // Run the effect when the userId changes

    return (
        <div className="container my-4">
            <div className="row mb-4 m-2">
                <div className="col d-flex justify-content-center">
                    <h3 style={{ color: "rgba(60, 141, 123, 1)" }}>ТВОИ КВИЗОВИ</h3>
                </div>
            </div>

            {loading && <div>Loading...</div>} 
            {error && <div className="alert alert-danger">{error}</div>} {/* Show error if any */}
            
            {/* Pass quizzes data to CustomCardsList2 */}
            {!loading && !error && quizzes && quizzes.length > 0 ? (
                <CustomCardsList className="row g-4" quizzes={quizzes} hideStartButton/>
            ) : (
                <div>No quizzes found.</div> // Show message if no quizzes found
            )}
        </div>
    );
}

export default MyQuizesPage;
