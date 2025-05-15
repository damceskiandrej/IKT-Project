const URL = "http://localhost:5190/api/Export"


export async function generatePdf(quizId, userId) {
  const response = await fetch(`${URL}/GeneratePdf`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ quizId, userId }),
  });

  if (!response.ok) {
    throw new Error('Failed to generate PDF');
  }

  return await response.blob(); 
}
