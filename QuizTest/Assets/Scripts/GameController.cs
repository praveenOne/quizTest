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

        void PrepareGameData()
        {
            m_QuizArray = JsonMapper.ToObject<quiz[]>(m_MetaDataJson.ToString());
        }

        public quiz[] GetQuizzes()
        {
            return m_QuizArray;
        }

        public void OnSlectQuiz(string quizId)
        {
            m_SelectedQuiz = GetQuiz(quizId);
            SceneManager.LoadScene(GameScenes.quiz.ToString());
        }

        public quiz GetSelectedQuiz()
        {
            return m_SelectedQuiz;
        }

        private quiz GetQuiz(string id)
        {
            return m_QuizArray.Where(x => x.id == id).Single();
        }
    }
}
