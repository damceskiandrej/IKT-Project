import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import LanguageDetector from 'i18next-browser-languagedetector';

const resources = {
    en: {
        translation: {
            header_option_1: 'HOME',
            header_option_2: 'ABOUT US',
            header_option_3: 'QUIZZES',
            header_option_4: 'QUIZ OF THE DAY',
            header_option_5: 'PROFILE',
            header_option_6: 'LOGIN',
            header_option_7: 'REGISTER',
            start_quiz: "START QUIZ",
            learn_more: "LEARN MORE",
            home_title: "Education is not the learning of facts, but the training of the mind to think. – Albert Einstein"
        },
    },
    mk: {
        translation: {
          header_option_1: 'ПОЧЕТНА',
          header_option_2: 'ЗА НАС',
          header_option_3: 'КВИЗОВИ',
          header_option_4: 'КВИЗ НА ДЕНОТ',
          header_option_5: 'ПРОФИЛ',
          header_option_6: 'ЛОГИРАЈ СЕ',
          header_option_7: 'РЕГИСТРИРАЈ СЕ',
          start_quiz: "ПОЧНИ КВИЗ",
          learn_more: "ДОЗНАЈ ПОВЕЌЕ",
          home_title: "ОБРАЗОВАНИЕТО НЕ Е УЧЕЊЕ НА ФАКТИ, ТУКУ ТРЕНИРАЊЕ НА УМОТ ДА РАЗМИСЛУВА. – АЛБЕРТ АЈНШТАЈН"
        },
    }
}

i18n
  .use(LanguageDetector)
  .use(initReactI18next) 
  .init({
    resources,
    fallbackLng: 'mk',
    interpolation: {
      escapeValue: false, 
    },
  });

export default i18n