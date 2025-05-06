function Hint() {
    return (
      <div className="container my-4">
        <button className="border-0 btn btn-outline">
            <div className="d-flex align-items-center mb-2">
            <img src="../../img/hint.png" style={{width: "40px"}}/>
            <h5 className="mb-0 fw-bold">HINT</h5>
            </div>
        </button>
        <p className="text-secondary fs-4 mb-0">[Ова е твојот hint]</p>
      </div>
    )
  }
  
  export default Hint