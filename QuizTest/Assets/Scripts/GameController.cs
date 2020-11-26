using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace praveen.one
{

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
    }
}
