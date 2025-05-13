import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom'; // ✅ Needed for navigation
import CustomCardsList from '../../components/CustomCardsList';
import { getQuizzesByUser, getQuizResultByUserAndQuizId } from '../../api/quizApi'; // ✅ Make sure this exists
import useUser from '../../hooks/useUser';

function MyQuizesPage() {
    const [quizzes, setQuizzes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const user = useUser();
    const navigate = useNavigate();
    const userId = user ? user.userId : "";

    useEffect(() => {
        if (!userId) return;

        const fetchQuizzes = async () => {
            try {
                const data = await getQuizzesByUser(userId);
                setQuizzes(data);
                setLoading(false);
            } catch (error) {
                setError(error.message);
                setLoading(false);
            }
        };

        fetchQuizzes();
    }, [userId]);

    const handleReviewClick = async (quizId) => {
        try {
            const quizResult = await getQuizResultByUserAndQuizId(userId, quizId);

            navigate("/quiz-review", {
                state: {
                    submission: quizResult,
                }
            });
        } catch (error) {
            console.error("Error fetching quiz result:", error);
            setError("Failed to fetch quiz results.");
        }
    };

    return (
        <div className="container my-4">
            <div className="row mb-4 m-2">
                <div className="col d-flex justify-content-center">
                    <h3 style={{ color: "rgba(60, 141, 123, 1)" }}>ТВОИ КВИЗОВИ</h3>
                </div>
            </div>

            {loading && <div>Loading...</div>}
            {error && <div className="alert alert-danger">{error}</div>}

            {!loading && !error && quizzes.length > 0 ? (
                <CustomCardsList
                    quizzes={quizzes}
                    hideStartButton
                    onReviewClick={handleReviewClick}
                />
            ) : (
                !loading && <div>No quizzes found.</div>
            )}
        </div>
    );
}


export default MyQuizesPage;
