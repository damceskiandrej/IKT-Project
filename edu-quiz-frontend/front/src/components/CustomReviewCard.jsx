function CustomReviewCard() {
    return (
      <div className="container p-3">
        <div className="border rounded p-3">
          <div className="d-flex align-items-start mb-3">
            <span className="badge bg-dark me-2">1</span>
            <h6 className="mb-0">
              КОЕ ОД СЛЕДНИВЕ Е ОСНОВНА КАРАКТЕРИСТИКА НА АГЕНТИТЕ?
            </h6>
          </div>
  
          <div className="d-flex flex-column">
            <div className="mb-2">A СТАТИЧНОСТ</div>
  
            <div
              className="mb-2 p-2 rounded bg-success bg-opacity-25 d-flex justify-content-between align-items-center"
              style={{ maxWidth: "300px" }}>
              <span>B АВТОНОМНОСТ</span>
              <span className="text-success">✔️</span>
            </div>
  
            <div className="mb-2 text-decoration-line-through text-danger">
              C ДИРЕКТНА КОНТРОЛА ОД КОРИСНИК
            </div>
  
            <div className="mb-2">D НЕПРОМЕНЛИВОСТ</div>
          </div>
        </div>
      </div>
    )
  }
  
  export default CustomReviewCard