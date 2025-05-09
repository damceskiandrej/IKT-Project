
const URL = "http://localhost:5190/api/AI"

export const getHint = async (quizId, questionId) => {
    try {
        const response = await fetch(`${URL}/hint/${quizId}/${questionId}`);
        
        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(errorText || "Failed to fetch hint.");
        }

        const data = await response.json();
        return data.hint
    } catch (error) {
        console.error("Error fetching hint:", error);
        throw error;
    }
};
