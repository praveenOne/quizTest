using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace praveen.one
{
    public class Results : MonoBehaviour
    {
        List<userChoice> m_FinalResults = new List<userChoice>();
        [SerializeField] Text m_ResultUIText;
        public void Start()
        {
            DisplayResults();
        }

        /// <summary>
        /// Display Final Results
        /// </summary>
        void DisplayResults()
        {
            m_FinalResults = GameController.Instance.GetResults();

            string resultsText = "";
            string resultSummary = "";
            int correct = 0;
            int incorrect = 0;

            for (int i = 0; i < m_FinalResults.Count; i++)
            {
                userChoice choice = m_FinalResults[i];
                questions q = GameController.Instance.GetQuestionInCurrentQuiz(choice.questionIndex);
                if (choice.userFeedback == q.answerIndex)
                {
                    correct += 1;
                    resultsText += "Question "+ (i+1) + " Answer :" + q.choices[choice.userFeedback].artist + " : " + q.choices[choice.userFeedback].title + "\n\n";
                }
                else
                {
                    incorrect += 1;
                    resultsText += "Question " + (i + 1) + " Answer :" + q.choices[choice.userFeedback].artist + " : " + q.choices[choice.userFeedback].title + "\n";
                    resultsText += "<color=green>Correct Answer : " + q.song.artist + " : " + q.song.title + "</color>\n\n";
                }
            }

            resultSummary += "<b><color=green>" + correct + " Correct Answers </color></b>\n";
            resultSummary += "<b><color=red>" + incorrect + " In Correct Answers</color></b>\n\n\n\n";

            m_ResultUIText.text = resultSummary + resultsText;

        }

        /// <summary>
        /// Go back to main menu
        /// </summary>
        public void OnClieckMainMenuBtn()
        {
            SceneManager.LoadScene(GameScenes.main.ToString());
        }
    }
}
