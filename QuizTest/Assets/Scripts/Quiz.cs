using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace praveen.one
{

    public class Quiz : MonoBehaviour
    {
        [SerializeField] AudioSource m_AudioSource;
        [SerializeField] Transform m_ButtonParent;
        [SerializeField] GameObject m_ChoiseButton;

        bool m_IsAnswered;
        quiz m_SelectedQuiz;
        int m_QuestionIndex;
        Texture m_AlbumTexture;

        AudioClip m_AudioClip;

        void Awake()
        {
            m_QuestionIndex = 0;
            m_SelectedQuiz = GameController.Instance.GetSelectedQuiz();
            ShowQuestions(m_SelectedQuiz.questions[m_QuestionIndex]);
        }

        void ShowQuestions(questions question)
        {
            HUD.Instance.SetQuestionInfo("Question " + (m_QuestionIndex + 1) + " of " + m_SelectedQuiz.questions.Length);

            HUD.Instance.HideUIResult();
            HUD.Instance.SetLoadingText("");

            StartCoroutine((new[] {
                SetAlbumCover(question.song.picture),
                GetAudioClip(question.song.sample),
                PopulateChoices(question.choices)
            }).GetEnumerator());

        }

        IEnumerator PopulateChoices(choices[] choices)
        {
            foreach (Transform child in m_ButtonParent)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < choices.Length; i++)
            {
                GameObject go = Instantiate(m_ChoiseButton, m_ButtonParent);
                int j = i;
                go.GetComponent<ActionButton>().Init(
                    choices[i].artist + " / " + choices[i].title
                    , () => { OnQuestionAnswered(j); });
            }

            yield return null;
            HUD.Instance.SetAlbumArt(m_AlbumTexture);
            m_IsAnswered = false;

            m_AudioSource.clip = m_AudioClip;
            m_AudioSource.PlayOneShot(m_AudioClip);
        }

        void OnQuestionAnswered(int answer)
        {
            if (m_IsAnswered)
                return;

            m_IsAnswered = true;

            m_AudioSource.Stop();
            questions question = m_SelectedQuiz.questions[m_QuestionIndex];
            GameController.Instance.AddUserChoice(new userChoice(m_QuestionIndex, answer));
            if (answer == question.answerIndex)
            {
                HUD.Instance.ShowUIResult(true);
            }
            else
            {
                HUD.Instance.ShowUIResult(false);
            }

            m_QuestionIndex += 1;

            if (m_SelectedQuiz.questions.Length > m_QuestionIndex)
            {
                StartCoroutine(LoadNextQuestion());
            }
            else
            {
                SceneManager.LoadScene(GameScenes.result.ToString());
            }
        }

        IEnumerator LoadNextQuestion()
        {
            int counter = 3;
            while (counter > 0)
            {
                HUD.Instance.SetLoadingText("Load Next In " + counter);
                yield return new WaitForSeconds(1);
                counter--;
            }

            ShowQuestions(m_SelectedQuiz.questions[m_QuestionIndex]);
        }


        IEnumerator SetAlbumCover(string MediaUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.LogError(request.error);
            else
                m_AlbumTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }

        IEnumerator GetAudioClip(string mediaUrl)
        {
            using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(mediaUrl, AudioType.WAV))
            {
                request.SendWebRequest();
                while (!request.isDone)
                {
                    yield return null;
                }

                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    m_AudioClip = DownloadHandlerAudioClip.GetContent(request);
                }
            }
            yield break;
        }
        
    }
}
