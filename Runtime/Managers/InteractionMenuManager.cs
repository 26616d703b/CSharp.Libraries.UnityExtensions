using UnityEngine;

namespace UnityExtensions.Runtime.Managers
{
    public class InteractionMenuManager : AMenuManager
    {
        #region Attributes

        public static InteractionMenuManager instance { get; set; }

        [Header("Actions")]
        [SerializeField]
        private RectTransform firstInteractionMenu;

        [SerializeField]
        private RectTransform coopMenu;

        [SerializeField]
        private RectTransform inquireMenu;

        [SerializeField]
        private RectTransform kneelMenu;

        private RectTransform m_previousInteractionMenuPanel;
        private RectTransform m_currentInteractionMenuPanel;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private new void Awake()
        {
            base.Awake();

            instance = this;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        protected override void OnLocaleChanged()
        {
            //throw new System.NotImplementedException();
        }

        // UI

        public void OnInquireClicked()
        {
            m_previousInteractionMenuPanel = m_currentInteractionMenuPanel;

            // -

            m_previousInteractionMenuPanel.gameObject.SetActive(false);
            m_currentInteractionMenuPanel.gameObject.SetActive(true);
        }

        public void OnCooperateClicked()
        {
        }

        public void OnTalkClicked()
        {
        }

        public void OnKneelClicked()
        {
            //PlayerInteractionController.current.OnInteract(Player.Motion.Kneeling);

            m_previousInteractionMenuPanel = m_currentInteractionMenuPanel;

            // -

            m_previousInteractionMenuPanel.gameObject.SetActive(false);
            m_currentInteractionMenuPanel.gameObject.SetActive(true);
        }

        //

        public void OnCallForHelpClicked()
        {
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public bool HasPrevious()
        {
            return m_previousInteractionMenuPanel != null;
        }

        public void HideCurrent()
        {
            m_currentInteractionMenuPanel.gameObject.SetActive(false);

            m_previousInteractionMenuPanel = null;
            m_currentInteractionMenuPanel = firstInteractionMenu;
        }

        public void DisplayCurrent()
        {
            if (m_currentInteractionMenuPanel == null)
            {
                m_currentInteractionMenuPanel = firstInteractionMenu;
            }

            m_currentInteractionMenuPanel.gameObject.SetActive(true);
        }

        public void DisplayPrevious()
        {
            m_currentInteractionMenuPanel.gameObject.SetActive(false);

            m_currentInteractionMenuPanel = m_previousInteractionMenuPanel;
            m_previousInteractionMenuPanel = null;

            m_currentInteractionMenuPanel.gameObject.SetActive(true);
        }

        #endregion Other Methods

        #endregion Methods
    }
}