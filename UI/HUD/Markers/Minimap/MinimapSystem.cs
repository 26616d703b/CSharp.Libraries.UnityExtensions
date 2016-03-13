using UnityEngine;
using UnityExtensions.UI.HUD.Managers;

namespace UnityExtensions.UI.HUD.Markers.Minimap
{
    public class MinimapSystem : MonoBehaviour
    {
        #region Attributes

        public static MinimapSystem current { get; set; }

        public Transform target;

        public bool lockRotation = false;

        [Range(3, 30)]
        public float zoomLevel = 10f;

        private readonly float m_minZoom = 3f;
        private readonly float m_maxZoom = 30f;

        private Vector2 m_xRotation = Vector2.right;
        private Vector2 m_yRotation = Vector2.up;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void OnEnable()
        {
            current = this;
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                zoomLevel += 0.1f;
                zoomLevel = Mathf.Min(m_maxZoom, zoomLevel);
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                zoomLevel -= 0.1f;
                zoomLevel = Mathf.Max(m_minZoom, zoomLevel);
            }
        }

        private void LateUpdate()
        {
            if (!lockRotation)
            {
                m_xRotation = new Vector2(target.right.x, -target.right.z);
                m_yRotation = new Vector2(-target.forward.x, target.forward.z);
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public Vector2 Position(Vector3 position)
        {
            var offset = position - target.position;
            var newPosition = offset.x * m_xRotation;
            newPosition += offset.z * m_yRotation;
            newPosition *= zoomLevel;

            return newPosition;
        }

        public Vector3 Rotation(Vector3 rotation)
        {
            if (lockRotation)
            {
                return new Vector3(0f, 0f, -rotation.y);
            }
            return new Vector3(0f, 0f, target.eulerAngles.y - rotation.y);
        }

        public Vector2 MoveInside(Vector2 point)
        {
            point = Vector2.Max(point, HUDManager.instance.miniMap.rect.min);
            point = Vector2.Min(point, HUDManager.instance.miniMap.rect.max);

            return point;
        }

        #endregion Other Methods

        #endregion Methods
    }
}