import CustomHeader from './components/CustomHeader'
import { useTranslation } from "react-i18next";
import React, { useState } from "react";
import { Outlet } from 'react-router-dom'

function Layout() {
  const { t, i18n } = useTranslation();
  const languages = ["en", "mk"];
  const [currentLangIndex, setCurrentLangIndex] = useState(
    languages.indexOf(i18n.language) !== -1 ? languages.indexOf(i18n.language) : 0
  );

  const changeLanguage = () => {
    const nextIndex = (currentLangIndex + 1) % languages.length; // Cycle through languages
    const nextLang = languages[nextIndex];
    i18n.changeLanguage(nextLang);
    setCurrentLangIndex(nextIndex);
  };

  return (
    <>
      <CustomHeader />
      <Outlet />
      <div className="d-flex justify-content-end">
        <button className="btn btn-secondary m-4" onClick={changeLanguage}>
          {t("Language")} ({languages[currentLangIndex].toUpperCase()})
        </button>
      </div>  
    </>
  )
}

export default Layout