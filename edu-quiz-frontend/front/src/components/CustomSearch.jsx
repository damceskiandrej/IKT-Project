function CustomSearch() {
    return (
        <div className="input-group p-1" style={{width: "320px"}}>
            <input type="text" className="form-control border-0" placeholder="Пребарај квиз по категорија"/>
            <button className="input-group-append border-0 rounded-1" style={{ backgroundColor: '#ffffff' }}>
                <img src="../../img/search.png"/>
            </button>
        </div>
    )
}

export default CustomSearch