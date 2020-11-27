using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace praveen.one
{
    public enum GameScenes
    {
        main,
        quiz,
        result,
    }

    public class GameController : MonoBehaviour
    {
        #region singleton stuff
        private static GameController m_Instance;

        public static GameController Instance
        {
            get { return m_Instance; }
        }
        #endregion

        [SerializeField] string m_MetaDataJson;
        quiz[] m_QuizArray;
        quiz m_SelectedQuiz;
        List<userChoice> m_UserChoice = new List<userChoice>();

        private void Awake()
        {
            if (m_Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                m_Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            PrepareGameData();

        }

        /// <summary>
        /// Fetch game data from Json
        /// </summary>
        void PrepareGameData()
        {
            m_QuizArray = JsonMapper.ToObject<quiz[]>(m_MetaDataJson.ToString());
        }

        /// <summary>
        /// Get All Quizzes
        /// </summary>
        /// <returns></returns>
        public quiz[] GetQuizzes()
        {
            return m_QuizArray;
        }

        /// <summary>
        /// On User Select a quiz
        /// </summary>
        /// <param name="quizId"></param>
        public void OnSlectQuiz(string quizId)
        {
            m_UserChoice.Clear();
            m_SelectedQuiz = GetQuiz(quizId);
            SceneManager.LoadScene(GameScenes.quiz.ToString());
        }

        /// <summary>
        /// Return Selected Quiz
        /// </summary>
        /// <returns></returns>
        public quiz GetSelectedQuiz()
        {
            return m_SelectedQuiz;
        }

        /// <summary>
        /// Get Quiz By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private quiz GetQuiz(string id)
        {
            return m_QuizArray.Where(x => x.id == id).Single();
        }

        /// <summary>
        /// Get Quiz Question by ID
        /// </summary>
        /// <param name="questionIndex"></param>
        /// <returns></returns>
        public questions GetQuestionInCurrentQuiz(int questionIndex)
        {
            return m_SelectedQuiz.questions[questionIndex];
        }

        /// <summary>
        /// Record user choice
        /// </summary>
        /// <param name="userChoice"></param>
        public void AddUserChoice(userChoice userChoice)
        {
            m_UserChoice.Add(userChoice);
        }

        /// <summary>
        /// Get Final Results
        /// </summary>
        /// <returns></returns>
        public List<userChoice> GetResults()
        {
            return m_UserChoice;
        }
    }
}
