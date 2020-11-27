using UnityEngine;

namespace praveen.one
{
    public class QuizSelect : MonoBehaviour
    {
        [SerializeField] GameObject m_QuizListButton;
        [SerializeField] Transform m_ButtonParent;
        void Awake()
        {
            PopulateQuizList();
        }

        /// <summary>
        /// Display Quiz List
        /// </summary>
        void PopulateQuizList()
        {
            quiz[] quizArray = GameController.Instance.GetQuizzes();

            for (int i = 0; i < quizArray.Length; i++)
            {
                quiz quiz = quizArray[i];
                GameObject go = Instantiate(m_QuizListButton, m_ButtonParent);
                int j = i;
                go.GetComponent<ActionButton>().Init(quiz.playlist, () => { OnSelectQuiz(quiz.id); });
            }
        }

        /// <summary>
        /// On User select a quiz
        /// </summary>
        /// <param name="quizId"></param>
        void OnSelectQuiz(string quizId)
        {
            GameController.Instance.OnSlectQuiz(quizId);
        }
    }
}
