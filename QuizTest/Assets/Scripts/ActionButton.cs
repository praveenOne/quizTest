using UnityEngine;
using UnityEngine.UI;

namespace praveen.one
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField] Text m_ButtonText;

        /// <summary>
        /// Initialize action button
        /// </summary>
        /// <param name="btnText"></param>
        /// <param name="callback"></param>
        public void Init(string btnText, System.Action callback)
        {
            m_ButtonText.text = btnText;
            GetComponent<Button>().onClick.AddListener(delegate {
                callback.Invoke();
            });
        }
    }
}
