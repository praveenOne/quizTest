using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace praveen.one
{

    public class Quiz : MonoBehaviour
    {
        [SerializeField] AudioSource m_AudioSource;
        [SerializeField] RawImage m_AlbumArt;
        [SerializeField] Transform m_ButtonParent;
        [SerializeField] GameObject m_ChoiseButton;
        [SerializeField] Text m_QuestionInfo;


        quiz m_SelectedQuiz;
        int m_QuestionIndex;

        void Awake()
        {
            m_QuestionIndex = 0;
            m_SelectedQuiz = GameController.Instance.GetSelectedQuiz();
            ShowQuestions(m_SelectedQuiz.questions[m_QuestionIndex]);
        }

        void ShowQuestions(questions question)
        {
            m_QuestionInfo.text = "Question "+ (m_QuestionIndex+1) +" of " + m_SelectedQuiz.questions.Length;

            StartCoroutine(SetAlbumCover(question.song.picture));
            StartCoroutine(GetAudioClip(question.song.sample));
            PopulateChoices(question.choices);
        }

        void PopulateChoices(choices[] choices)
        {
            //m_ScrollRect.
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
                    , () => { OnAnswerQuestion(j); });
            }
        }

        void OnAnswerQuestion(int answer)
        {
            m_AudioSource.Stop();
            questions question = m_SelectedQuiz.questions[m_QuestionIndex];
            if (answer == question.answerIndex)
            {
                Debug.Log("Answer Is correnct");
            }
            else
            {
                Debug.Log("Wrong");
            }

            m_QuestionIndex += 1;

            if (m_SelectedQuiz.questions.Length > m_QuestionIndex)
            {
                ShowQuestions(m_SelectedQuiz.questions[m_QuestionIndex]);
            }
        }


            IEnumerator SetAlbumCover(string MediaUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
                m_AlbumArt.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }

        IEnumerator GetAudioClip(string mediaUrl)
        {
            AudioClip myClip = null;
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(mediaUrl, AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    myClip = DownloadHandlerAudioClip.GetContent(www);
                }
            }

            m_AudioSource.clip = myClip;
            m_AudioSource.PlayOneShot(myClip);
        }
    }
}
