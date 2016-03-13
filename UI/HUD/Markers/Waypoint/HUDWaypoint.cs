using UnityEngine;

namespace UnityExtensions.UI.HUD.Markers.Waypoint
{
    public class HUDWaypoint : MonoBehaviour
    {
        #region Attributes

        [SerializeField]
        private WaypointArrow m_arrow;

        [SerializeField]
        private GUIText m_distance;

        [SerializeField]
        private GUITexture m_icon;

        [SerializeField]
        private GUIText m_label;

        [SerializeField]
        private GUIText m_timer;

        private bool m_onScreen;
        private Waypoint m_waypoint;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Update is called once per frame
        private void Update()
        {
            if (m_waypoint == null)
                return;

            var alphaFactor = m_waypoint.falloff
                ? m_waypoint.falloffAlpha.Evaluate((m_waypoint.distance - m_waypoint.falloffStart) /
                                                   (m_waypoint.falloffEnd - m_waypoint.falloffStart))
                : 1f;
            alphaFactor *= m_waypoint.pulse
                ? Mathf.PingPong(Time.time - m_waypoint.pulseSetTime, m_waypoint.pulsePeriod / 2)
                : 1f;

            if (m_icon != null)
            {
                m_icon.texture = m_waypoint.icon.texture;

                var newColor = m_waypoint.iconTint * 0.5f;
                newColor.a *= alphaFactor;
                m_icon.color = newColor;

                m_icon.gameObject.SetActive(true);
            }

            if (WaypointSystem.current.OnScreen(m_waypoint))
            {
                if (m_arrow != null)
                {
                    m_arrow.gameObject.SetActive(false);
                }

                if (m_distance != null)
                {
                    m_distance.text = string.Format("{0:N0}m", m_waypoint.distance);
                    var newColor = m_waypoint.labelTextColor;
                    newColor.a *= alphaFactor;
                    m_distance.material.color = newColor;

                    m_distance.gameObject.SetActive(true);
                }

                if (m_label != null)
                {
                    m_label.text = m_waypoint.labelText;
                    var newColor = m_waypoint.labelTextColor;
                    newColor.a *= alphaFactor;
                    m_label.material.color = newColor;

                    m_label.gameObject.SetActive(true);
                }

                if (m_timer != null)
                {
                    var minutes = Mathf.FloorToInt(m_waypoint.timer / 60F);
                    var seconds = Mathf.FloorToInt(m_waypoint.timer - minutes * 60);

                    m_timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                    var newColor = m_waypoint.labelTextColor;
                    newColor.a *= alphaFactor;
                    m_timer.material.color = newColor;

                    m_timer.gameObject.SetActive(true);
                }
            }
            else
            {
                if (m_arrow != null)
                {
                    var newColor = m_waypoint.iconTint;
                    newColor.a *= alphaFactor;
                    m_arrow.color = newColor;

                    var direction = new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y - 0.5f);
                    m_arrow.rotation = Vector2.Angle(Vector2.up, direction.normalized);

                    if (direction.x < 0)
                    {
                        m_arrow.rotation *= -1f;
                    }

                    m_arrow.gameObject.SetActive(true);
                }

                if (m_distance != null)
                {
                    m_distance.gameObject.SetActive(false);
                }

                if (m_label != null)
                {
                    m_label.gameObject.SetActive(false);
                }

                if (m_timer != null)
                {
                    m_timer.gameObject.SetActive(false);
                }
            }

            SetPosition(WaypointSystem.current.ScreenPosition(m_waypoint));
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Init(Waypoint waypoint)
        {
            m_waypoint = waypoint;
        }

        private void SetPosition(Vector3 screenPosition)
        {
            transform.localPosition = screenPosition;
        }

        #endregion Other Methods

        #endregion Methods
    }
}