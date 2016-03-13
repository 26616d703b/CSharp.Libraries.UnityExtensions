using UnityEngine;
using UnityExtensions.Localization;

namespace UnityExtensions.Runtime.Managers
{
    public abstract class AMenuManager : MonoBehaviour
    {
        #region Attributes

        [SerializeField]
        private RectTransform m_menuPanel;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        protected void Awake()
        {
            LocaleManager.instance.OnLocaleChange += OnLocaleChanged;
        }

        private void OnDestroy()
        {
            LocaleManager.instance.OnLocaleChange -= OnLocaleChanged;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        protected abstract void OnLocaleChanged();

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void DisplayMenuPanel()
        {
            m_menuPanel.gameObject.SetActive(true);

            if (m_menuPanel.GetComponent<Animator>() != null)
            {
                m_menuPanel.GetComponent<Animator>().SetBool("Visible", true);
            }
        }

        public void HideMenuPanel()
        {
            /*if (m_menuPanel.GetComponent<Animator>() != null)
            {
                m_menuPanel.GetComponent<Animator>().SetBool("Visible", false);
            }*/

            m_menuPanel.gameObject.SetActive(false);
        }

        #endregion Other Methods

        #endregion Methods
    }
}