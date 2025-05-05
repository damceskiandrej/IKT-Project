
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
        console.log("getQuiz", data)
        return data;  
    } catch (error) {
        console.error("Error fetching quiz:", error); 
        throw error;  
    }
}

export const postQuizResult = async (submission) => {
    try {
        const response = await fetch(`${URL}/SubmitQuizResult`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(submission),
        });

        if (!response.ok) {
            throw new Error(`Failed to submit quiz: ${response.statusText}`);
        }

        const data = await response.json();
        console.log("Submission response:", data); // You can log or process the response as needed
        return data;
    } catch (error) {
        console.error("Error submitting quiz:", error);
        throw error;
    }
};






