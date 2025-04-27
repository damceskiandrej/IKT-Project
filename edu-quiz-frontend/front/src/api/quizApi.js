
const URL = "http://localhost:5190/api/quiz"

export const getAllQuizzes = async () => {
    const response = await fetch(`${URL}/GetAllQuizzes`);
    if (!response.ok) {
        throw new Error('Failed to fetch quizzes',response);
    }
    return response.json();
};

export const getQuizById = async (id) => {
    try {
        const response = await fetch(`${URL}/GetQuiz?id=${id}`);  
        if (!response.ok) {
            throw new Error(`Failed to fetch quiz: ${response.statusText}`);
        }
        const data = await response.json();  
        return data;  
    } catch (error) {
        console.error("Error fetching quiz:", error); 
        throw error;  
    }
}



