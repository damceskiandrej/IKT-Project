
const URL = "http://localhost:5190/api/Recommendation"


export async function getQuizExplanationResponses(quizId, userId) {
    const response = await fetch(`${URL}/GetQuizExplanationResponses/${quizId}/${userId}`);

    if (!response.ok) {
        throw new Error(`Failed to fetch quiz result: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    console.log("recommends",data)
    return data;
}