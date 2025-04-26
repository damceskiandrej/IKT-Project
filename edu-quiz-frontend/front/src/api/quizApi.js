
const URL = "http://localhost:5190/api/quiz"

export const getAllQuizzes = async () => {
    const response = await fetch(`${URL}/GetAllQuizzes`);
    if (!response.ok) {
        throw new Error('Failed to fetch quizzes',response);
    }
    return response.json();
};





