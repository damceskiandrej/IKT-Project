// CustomSearch.jsx
function CustomSearch({ searchTerm, onSearchChange }) {
    return (
        <div className="input-group p-1" style={{ width: "320px" }}>
            <input
                type="text"
                className="form-control border-0"
                placeholder="Пребарај квиз по категорија"
                value={searchTerm}
                onChange={(e) => onSearchChange(e.target.value)}
            />
            <button className="input-group-append border-0 rounded-1" style={{ backgroundColor: '#ffffff' }}>
                <img src="../../img/search.png" alt="Search" />
            </button>
        </div>
    );
}

export default CustomSearch;
