using System;
using UnityEngine;
using UnityEngine.UI;
using UnityExtensions.Characters;
using UnityExtensions.UI.HUD.Managers;

namespace UnityExtensions.UI.HUD.Markers.Minimap
{
    public class MapItem : MonoBehaviour
    {
        #region Attributes

        public Character.Team characterTeam;
        public Character.Type characterType;

        private Image m_miniMapItemImage;

        [Space(10)]
        public bool keepInBounds = true;

        public bool lockRotation = false;
        public bool lockScale = false;

        [Space(10)]
        [SerializeField]
        private bool m_pulse;

        [Range(0.5f, 5f)]
        public float pulsePeriod = 2f;

        private float m_pulseSetTime;

        private Transform m_target;
        private RectTransform m_targetRectTransform;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public bool pulse
        {
            get { return m_pulse; }
            set
            {
                m_pulse = value;
                m_pulseSetTime = Time.time;
            }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Start()
        {
            GameObject miniMapItem;

            switch (characterType)
            {
                case Character.Type.NPC:

                    miniMapItem = Instantiate(Resources.Load<GameObject>("MapItem - NPC"));

                    break;

                case Character.Type.Player:

                    miniMapItem = Instantiate(Resources.Load<GameObject>("MapItem - BetaPlayer"));

                    break;

                default:
                    throw new EntryPointNotFoundException();
            }

            miniMapItem.transform.SetParent(HUDManager.instance.miniMap.FindChild("Items"));
            m_miniMapItemImage = miniMapItem.GetComponent<Image>();

            m_target = transform.parent;
            m_targetRectTransform = miniMapItem.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (m_pulse)
            {
                var newItemColor = m_miniMapItemImage.color;
                newItemColor.a = Mathf.PingPong(Time.time - m_pulseSetTime, pulsePeriod / 2);

                m_miniMapItemImage.color = newItemColor;
            }
        }

        private void LateUpdate()
        {
            var newPosition = MinimapSystem.current.Position(m_target.position);

            if (keepInBounds)
            {
                newPosition = MinimapSystem.current.MoveInside(newPosition);
            }

            if (!lockRotation)
            {
                m_targetRectTransform.localEulerAngles = MinimapSystem.current.Rotation(m_target.eulerAngles);
            }

            if (!lockScale)
            {
                m_targetRectTransform.localScale = new Vector3(MinimapSystem.current.zoomLevel,
                    MinimapSystem.current.zoomLevel, 1f);
            }

            m_targetRectTransform.localPosition = newPosition;
        }

        private void OnDestroy()
        {
            if (m_miniMapItemImage != null)
            {
                Destroy(m_miniMapItemImage.gameObject);
            }
        }

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