using UnityEngine;
using UnityEngine.UI;
using UnityExtensions.Characters;
using UnityExtensions.UI.Components;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityExtensions.UI.HUD.Managers
{
    public class HUDManager : MonoBehaviour
    {
        #region Attributes

        public static HUDManager instance { get; set; }

        [Header("Activity Log")]
        [SerializeField]
        private RectTransform m_activityLogPanel;

        [Header("Chest Press Gauge")]
        [SerializeField]
        private RectTransform m_chestPressGaugePanel;

        [Header("Crosshair")]
        [SerializeField]
        private Image m_crosshairImage;

        [Space(10)]
        [SerializeField]
        private Sprite m_defaultCrosshairSprite;

        [SerializeField]
        private Sprite m_interactCrosshairSprite;

        [SerializeField]
        private Sprite m_cannotInteractCrosshairSprite;

        [SerializeField]
        private Sprite m_cooperateCrosshairSprite;

        [Header("MiniMap")]
        [SerializeField]
        private RectTransform m_miniMapPanel;

        [Header("Score")]
        [SerializeField]
        private RectTransform m_scorePanel;

        [Header("Time")]
        [SerializeField]
        private RectTransform m_timePanel;

        [Header("Timer")]
        [SerializeField]
        private RectTransform m_timerPanel;

        [Header("Tooltips")]
        // CPR
        [SerializeField]
        private RectTransform m_startChestPressTooltipPanel;

        // Interaction
        [SerializeField]
        private RectTransform m_startInteractionTooltipPanel;

        [SerializeField]
        private RectTransform m_cancelInteractionTooltipPanel;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public RectTransform miniMap
        {
            get { return m_miniMapPanel; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Show/Hide Activity Log"))
            {
                if (!m_activityLogPanel.gameObject.activeInHierarchy)
                {
                    DisplayActivityLog();
                }
                else
                {
                    HideActivityLog();
                }
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void DisplayAll()
        {
            DisplayCrosshair(Player.Interaction.Type.None);
            DisplayMiniMap();
        }

        public void HideAll()
        {
            HideCrosshair();
            HideMiniMap();
        }

        // Activity Log

        public void DisplayActivityLog()
        {
            m_activityLogPanel.gameObject.SetActive(true);
        }

        public void HideActivityLog()
        {
            m_activityLogPanel.gameObject.SetActive(false);
        }

        public void SetActivityLogText(string text)
        {
            m_activityLogPanel.GetComponentInChildren<Text>().text = text;
        }

        // Chest Press

        public void DisplayChestPressGauge()
        {
            m_chestPressGaugePanel.gameObject.SetActive(true);
        }

        public void HideChestPressGauge()
        {
            m_chestPressGaugePanel.gameObject.SetActive(false);
        }

        public void SetChestPressGaugeValue(float value)
        {
            m_chestPressGaugePanel.GetComponent<Slider>().value = value;
        }

        // Crosshair

        public void DisplayCrosshair(Player.Interaction.Type interactionType)
        {
            if (!m_crosshairImage.gameObject.activeInHierarchy)
                m_crosshairImage.gameObject.SetActive(true);

            switch (interactionType)
            {
                case Player.Interaction.Type.None:

                    m_crosshairImage.sprite = m_defaultCrosshairSprite;

                    break;

                case Player.Interaction.Type.NotPossible:

                    m_crosshairImage.sprite = m_cannotInteractCrosshairSprite;

                    break;

                case Player.Interaction.Type.Regular:

                    m_crosshairImage.sprite = m_interactCrosshairSprite;

                    break;

                case Player.Interaction.Type.Cooperation:

                    m_crosshairImage.sprite = m_cooperateCrosshairSprite;

                    break;

                case Player.Interaction.Type.Rescue:

                    //crosshairImage.sprite =

                    break;

                default:
                    break;
            }
        }

        public void HideCrosshair()
        {
            m_crosshairImage.gameObject.SetActive(false);
        }

        // MiniMap

        public void DisplayMiniMap()
        {
            m_miniMapPanel.gameObject.SetActive(true);
        }

        public void HideMiniMap()
        {
            m_miniMapPanel.gameObject.SetActive(false);
        }

        // Score

        public void DisplayScore()
        {
            m_scorePanel.gameObject.SetActive(true);
        }

        public void HideScore()
        {
            m_scorePanel.gameObject.SetActive(false);
        }

        public void SetRatioText(string ratio)
        {
            m_scorePanel.GetComponentsInChildren<Label>()[0].text = ratio;
        }

        public void SetAverageSpeedText(string averageSpeed)
        {
            m_scorePanel.GetComponentsInChildren<Label>()[1].text = averageSpeed;
        }

        // Time

        public void DisplayTime()
        {
            m_timePanel.gameObject.SetActive(true);
        }

        public void HideTime()
        {
            m_timePanel.gameObject.SetActive(false);
        }

        public void SetTimeText(string roomTime)
        {
            if (m_timePanel.gameObject.activeInHierarchy)
            {
                m_timePanel.GetComponentInChildren<Text>().text = roomTime;
            }
        }

        // Timer

        public void DisplayTimer()
        {
            m_timerPanel.gameObject.SetActive(true);
        }

        public void HideTimer()
        {
            m_timerPanel.gameObject.SetActive(false);
        }

        public void SetTimerText(string timeLeft)
        {
            if (m_timerPanel.gameObject.activeInHierarchy)
            {
                m_timerPanel.GetComponentInChildren<Text>().text = timeLeft;
            }
        }

        public void SetTimerSliderValue(float value)
        {
            if (m_timerPanel.gameObject.activeInHierarchy)
            {
                m_timerPanel.GetComponentInChildren<Slider>().value = value;
            }
        }

        // Tooltips

        // Chest Press

        public void DisplayStartChestPressTooltip()
        {
            m_startChestPressTooltipPanel.gameObject.SetActive(true);
        }

        public void HideStartChestPressTooltip()
        {
            m_startChestPressTooltipPanel.gameObject.SetActive(false);
        }

        // Interaction

        public void DisplayInteractionTooltips()
        {
            m_startInteractionTooltipPanel.gameObject.SetActive(true);
            m_cancelInteractionTooltipPanel.gameObject.SetActive(true);
        }

        public void HideInteractionTooltips()
        {
            m_startInteractionTooltipPanel.gameObject.SetActive(false);
            m_cancelInteractionTooltipPanel.gameObject.SetActive(false);
        }

        public void DisplayStartInteractionTooltip()
        {
            m_startInteractionTooltipPanel.gameObject.SetActive(true);
        }

        public void HideStartInteractionTooltip()
        {
            m_startInteractionTooltipPanel.gameObject.SetActive(false);
        }

        public void DisplayCancelInteractionTooltip()
        {
            m_cancelInteractionTooltipPanel.gameObject.SetActive(true);
        }

        public void HideCancelInteractionTooltip()
        {
            m_cancelInteractionTooltipPanel.gameObject.SetActive(false);
        }

        #endregion Other Methods

        #endregion Methods
    }
}