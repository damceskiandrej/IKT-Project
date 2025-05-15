import { useTranslation } from "react-i18next";


function CustomSearch({ searchTerm, onSearchChange }) {

    const {t} = useTranslation()



    return (
        <div className="input-group p-1" style={{ width: "320px" }}>
            <input
                type="text"
                className="form-control border-0"
                placeholder={t('search_placeholder')}
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
