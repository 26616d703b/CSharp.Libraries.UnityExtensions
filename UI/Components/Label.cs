using UnityEngine;
using UnityEngine.UI;

namespace UnityExtensions.UI.Components
{
    public class Label : MonoBehaviour
    {
        #region Attributes

        [SerializeField]
        private Image m_image;

        [SerializeField]
        private Text m_text;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        public Sprite image
        {
            get { return m_image.sprite; }
            set { m_image.sprite = value; }
        }

        public string text
        {
            get { return m_text.text; }
            set { m_text.text = value; }
        }

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private void Start()
        {
            if (m_image == null)
                m_image = GetComponentInChildren<Image>();

            if (m_text == null)
                m_text = GetComponentInChildren<Text>();
        }

        // Update is called once per frame
        //void Update() { }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}