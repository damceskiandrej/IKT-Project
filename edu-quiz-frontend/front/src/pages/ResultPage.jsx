import CustomButton from '../components/CustomButton'

function ResultPage({score}) {
  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <div
        style={{
          width: '350px',
          height: '350px',
          borderRadius: '50%',
          backgroundColor: '#538c7a',
          position: 'relative',
          textAlign: 'center',
          color: 'white',
          padding: '40px 20px',
          boxShadow: '0 4px 8px rgba(0, 0, 0, 0.2)',
        }}>
        <div className="container mt-5 p-3">
            <h2 className="fw-bold">БРАВО!</h2>
            <h4 className="fw-bold">Твојот резултат e {score}%.</h4>
            <CustomButton btnText={"НАЗАД"}/>
        </div>
      </div>
    </div>
  );
};

export default ResultPage