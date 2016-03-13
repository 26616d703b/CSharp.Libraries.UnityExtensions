using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityExtensions.UI.FX
{
    [RequireComponent(typeof(Text))]
    public class Typewriter : MonoBehaviour
    {
        #region Attributes

        public bool autoStart = true;

        private string m_text;
        private int m_currentIndex;

        private Text m_label;

        [SerializeField]
        [Range(0.01f, 0.1f)]
        private readonly float m_frequency = 0.01f;

        [SerializeField]
        [Range(0.01f, 0.2f)]
        private readonly float m_delayOnNewLine = 0.1f;

        [SerializeField]
        [Range(0.01f, 0.2f)]
        private readonly float m_delayOnSpace = 0.05f;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private void Start()
        {
            m_label = GetComponent<Text>();

            m_text = m_label.text;

            if (autoStart)
            {
                StartCoroutine(Start_Coroutine());
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public delegate void TypewriterEvent();

        //public event TypewriterEvent OnFinish;

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        private IEnumerator Start_Coroutine()
        {
            m_label.text = string.Empty;

            while (m_currentIndex != m_text.Length + 1)
            {
                if (m_currentIndex != m_text.Length)
                {
                    if (m_text[m_currentIndex] == ' ')
                    {
                        yield return new WaitForSeconds(m_delayOnSpace);
                    }
                    else if (m_text[m_currentIndex] == '\n')
                    {
                        yield return new WaitForSeconds(m_delayOnNewLine);
                    }
                    else
                    {
                        yield return new WaitForSeconds(m_frequency);
                    }
                }

                // Substring is a part of Type_Text String that we declared at the start
                m_label.text = m_text.Substring(0, m_currentIndex) + "_";
                m_currentIndex++; // Doing a post fix
            }

            //OnFinish();

            m_currentIndex = 0;
        }

        #endregion Other Methods

        #endregion Methods
    }
}