using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityExtensions.UI.HUD.Markers.Waypoint
{
    public class WaypointSystem : MonoBehaviour
    {
        #region Attributes

        [SerializeField]
        private WaypointCircuit[] m_circuits;

        [Header("HUD")]
        public Transform target;

        public Camera targetCamera;

        [Range(0f, 0.5f)]
        public float safeMargin = 0.1f;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public static WaypointSystem current { get; set; }

        public WaypointCircuit[] circuits
        {
            get { return m_circuits; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void OnEnable()
        {
            current = this;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public float Distance(Waypoint waypoint)
        {
            return Vector3.Distance(target.position, waypoint.transform.position);
        }

        public bool OnScreen(Waypoint waypoint)
        {
            var waypointScreenPosition = ScreenPosition(waypoint);
            var waypointDirection = waypoint.transform.position - targetCamera.transform.position;

            if (Vector3.Dot(waypointDirection, targetCamera.transform.forward) <= 0)
            {
                return false;
            }

            var safeMargin = current.safeMargin;

            if (waypointScreenPosition.x < safeMargin || waypointScreenPosition.x > 1 - safeMargin
                || waypointScreenPosition.y < safeMargin || waypointScreenPosition.y > 1 - safeMargin)
            {
                return false;
            }
            return true;
        }

        public Vector3 ScreenPosition(Waypoint waypoint)
        {
            var screenPosition = targetCamera.WorldToScreenPoint(waypoint.transform.position);
            screenPosition.x = screenPosition.x / targetCamera.pixelWidth;
            screenPosition.y = screenPosition.y / targetCamera.pixelHeight;
            screenPosition.z = transform.position.z;

            return screenPosition;
        }

        #endregion Other Methods

        #endregion Methods
    }
}