function CustomTimer({ timer }) {
  const formatTime = (seconds) => {
      const minutes = Math.floor(seconds / 60);
      const remainingSeconds = seconds % 60;
      return `${minutes.toString().padStart(2, '0')}:${remainingSeconds.toString().padStart(2, '0')}`;
  };

  return (
      <div className="d-flex align-items-center">
          <img src="../../img/timer.png" alt="Timer Icon" />
          <h4 className="timer-text mb-0 ms-2" style={{ color: "rgba(0, 0, 0, 1)" }}>
              Time left: <strong>{formatTime(timer)}</strong>
          </h4>
      </div>
  );
}

export default CustomTimer;
