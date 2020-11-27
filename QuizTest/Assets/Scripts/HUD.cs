using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace praveen.one
{
    public class HUD : MonoBehaviour
    {
        #region singleton stuff
        private static HUD m_Instance;

        public static HUD Instance
        {
            get { return m_Instance; }
        }
        #endregion

        [SerializeField] RawImage m_AlbumArt;
        [SerializeField] Text m_QuestionInfo;
        [SerializeField] GameObject m_CorrectIcon;
        [SerializeField] GameObject m_WrongIcon;
        [SerializeField] Text m_LoadingCountdown;

        private void Awake()
        {
            m_Instance = this;

        }

        /// <summary>
        /// Display results 
        /// </summary>
        /// <param name="isCrorrect"></param>
        public void ShowUIResult(bool isCrorrect)
        {
            m_CorrectIcon.SetActive(isCrorrect);
            m_WrongIcon.SetActive(!isCrorrect);
        }

        /// <summary>
        /// Hide results
        /// </summary>
        public void HideUIResult()
        {
            m_CorrectIcon.SetActive(false);
            m_WrongIcon.SetActive(false);
        }

        /// <summary>
        /// Display loading info
        /// </summary>
        /// <param name="text"></param>
        public void SetLoadingText(string text)
        {
            m_LoadingCountdown.text = text;
        }

        /// <summary>
        /// Display question info
        /// </summary>
        /// <param name="text"></param>
        public void SetQuestionInfo(string text)
        {
            m_QuestionInfo.text = text;
        }

        /// <summary>
        /// Set Album art
        /// </summary>
        /// <param name="texture"></param>
        public void SetAlbumArt(Texture texture)
        {
            m_AlbumArt.texture = texture;
        }

    }
}
