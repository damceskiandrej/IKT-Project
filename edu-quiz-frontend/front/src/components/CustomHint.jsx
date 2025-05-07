import { useState } from "react";

function CustomHint({ hint, fetchHint }) {
    const [loading, setLoading] = useState(false);

    const handleClick = async () => {
        setLoading(true);
        await fetchHint();
        setLoading(false);
    };

    return (
        <div className="container my-4">
            <button className="border-0 btn btn-outline" onClick={handleClick}>
                <div className="d-flex align-items-center mb-2">
                    <img src="../../img/hint.png" alt="Hint Icon" style={{ width: "40px" }} />
                    <h5 className="mb-0 fw-bold">HINT</h5>
                </div>
            </button>
            {loading && <p className="text-secondary">Loading hint...</p>}
            {hint !== null && !loading && (<p className="text-secondary fs-4 mb-0">{hint}</p>)}
        </div>
    );
}

export default CustomHint;
