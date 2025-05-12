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
            home_title: "EDUCATION IS NOT THE LEARNING OF FACTS, BUT THE TRAINING OF THE MIND TO THINK. – ALBERT EINSTEIN",
            about_us: "EDUQUIZ IS A MODERN ONLINE PLATFORM THAT USES ARTIFICIAL INTELLIGENCE TO AUTOMATICALLY GENERATE QUESTIONS AND ANSWERS, TAILORED TO THE USER'S KNOWLEDGE AND INTERESTS. PERFECT FOR STUDENTS, PROFESSIONALS, AND LEARNING ENTHUSIASTS, EDUQUIZ OFFERS INTERACTIVE QUIZZES, PERSONALIZED RECOMMENDATIONS, AND A DETAILED OVERVIEW OF THE ACHIEVED RESULTS. WITH A DYNAMIC APPROACH TO EDUCATION, THIS PLATFORM ENABLES FUN AND EFFECTIVE ACQUISITION OF NEW KNOWLEDGE.",
            go_to_quizzes: "GO TO QUIZZES",
            next_question: "NEXT QUESTION",
            logout: "LOGOUT",
            logout: "ОДЈАВИ СЕ",
            name: "Name",
            surname: "Surname",
            email: "Email",
            password: "Password",
            my_quizzes: "My Quizzes",
            all_quizzes: "All Quizzes",
            hints: "Hints",
            congrats: "CONGRATS",
            congrats_description: "YOUR SCORE IS",
            previous_question: "PREVIOUS QUESTION",
            final_question: "FINAL QUESTION",
            new_acc: "You don't have profile? Register now!",
            have_acc: "Already have an account? Login!",
            add_quiz: "Add Quiz",
            add_student: "Add Student",
            back: "BACK",
            welcome: 'Welcome!',
            view_answers: "View The Answers",
            for_the_quiz: 'To The Quiz On'
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
          home_title: "ОБРАЗОВАНИЕТО НЕ Е УЧЕЊЕ НА ФАКТИ, ТУКУ ТРЕНИРАЊЕ НА УМОТ ДА РАЗМИСЛУВА. – АЛБЕРТ АЈНШТАЈН",
          about_us: "EDUQUIZ Е МОДЕРНА ОНЛАЈН ПЛАТФОРМА КОЈА КОРИСТИ ВЕШТАЧКА ИНТЕЛИГЕНЦИЈА ЗА АВТОМАТСКО ГЕНЕРИРАЊЕ НА ПРАШАЊА И ОДГОВОРИ, ПРИЛАГОДЕНИ НА ЗНАЕЊЕТО И ИНТЕРЕСИТЕ НА КОРИСНИКОТ. СОВРШЕНА ЗА СТУДЕНТИ, ПРОФЕСИОНАЛЦИ И ЉУБИТЕЛИ НА УЧЕЊЕ, EDUQUIZ НУДИ ИНТЕРАКТИВНИ КВИЗОВИ, ПЕРСОНАЛИЗИРАНИ ПРЕПОРАКИ И ДЕТАЛЕН ПРЕГЛЕД НА ПОСТИГНАТИТЕ РЕЗУЛТАТИ. СО ДИНАМИЧЕН ПРИСТАП КОН ОБРАЗОВАНИЕТО, ОВАА ПЛАТФОРМА ОВОЗМОЖУВА ЗАБАВНО И ЕФЕКТИВНО УСВОЈУВАЊЕ НА НОВИ ЗНАЕЊА",
          go_to_quizzes: "КОН КВИЗОВИ",
          next_question: "СЛЕДНО ПРАШАЊЕ",
          logout: "ОДЈАВИ СЕ",
          name: "Име",
          surname: "Презиме",
          email: "Мејл адреса",
          password: "Лозинка",
          my_quizzes: "Мои Квизови",
          all_quizzes: "Сите Квизови",
          hints: "Совети",
          congrats: "БРАВО",
          congrats_description: "ТВОЈОТ РЕЗУЛТАТ Е",
          previous_question: "ПРЕДХОДНО ПРАШАЊЕ",
          final_question: "ФИНАЛНО ПРАШАЊЕ",
          new_acc: "Немате профил? Регистрирајте се!",
          have_acc: "Веќе имате корисничка сметка? Логирајте се!",
          add_quiz: "Додади Квиз",
          add_student: "Додади Студент",
          back: 'НАЗАД',
          welcome: 'Добредојдовте!',
          view_answers: 'Преглед на Одговори',
          for_the_quiz: 'За квизот'
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