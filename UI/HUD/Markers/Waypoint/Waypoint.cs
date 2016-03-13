using UnityEngine;
using UnityExtensions.Characters;

namespace UnityExtensions.UI.HUD.Markers.Waypoint
{
    public class Waypoint : MonoBehaviour
    {
        #region Attributes

        public Character.Team characterTeam;
        public Character.Type characterType;

        private HUDWaypoint m_HUDWaypoint;

        [Header("HUD")]
        public Sprite icon;

        public Color iconTint = Color.white;
        public string labelText;
        public Color labelTextColor = Color.white;
        public float timer;

        [Space(10)]
        public bool falloff = false;

        public float falloffMax = 50f;
        public float falloffStart = 10f;
        public float falloffEnd = 25f;
        public AnimationCurve falloffAlpha = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

        [SerializeField]
        public bool m_pulse;

        [Range(0.5f, 5f)]
        public float pulsePeriod = 2f;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float distance { get; private set; }

        public bool pulse
        {
            get { return m_pulse; }
            set
            {
                m_pulse = value;
                pulseSetTime = Time.time;
            }
        }

        public float pulseSetTime { get; private set; }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private void Start()
        {
            m_HUDWaypoint =
                ((GameObject)Instantiate(Resources.Load<GameObject>("HUDWaypoint"), Vector3.zero, Quaternion.identity))
                    .GetComponent<HUDWaypoint>();

            m_HUDWaypoint.transform.parent = WaypointSystem.current.transform;
            m_HUDWaypoint.Init(this);
        }

        // Update is called once per frame
        private void Update()
        {
            distance = WaypointSystem.current.Distance(this);
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