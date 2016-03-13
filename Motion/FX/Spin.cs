using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using XInputDotNetPure;

namespace UnityExtensions.Motion.FX
{
    [RequireComponent(typeof(Collider))]
    public class Spin : MonoBehaviour
    {
        #region Properties

        public enum Direction
        {
            Positive = 1,
            Negative = -1
        }

        #endregion Properties

        #region Attributes

        public bool auto = true;

        public Direction direction = Direction.Negative;

        [Range(0f, 45f)]
        public float xSpeed = 3f;

        [Range(0f, 45f)]
        public float ySpeed = 3f;

        [Range(0f, 45f)]
        public float zSpeed = 3f;

        private bool m_canSpinWithMouse;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void OnMouseDown()
        {
            if (auto)
                return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            m_canSpinWithMouse = false;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    m_canSpinWithMouse = true;
                }
            }
        }

        private void OnMouseDrag()
        {
            if (auto || !m_canSpinWithMouse)
                return;

            transform.localRotation = Quaternion.Euler(
                direction.GetHashCode() * xSpeed * CrossPlatformInputManager.GetAxis("Mouse Y"),
                direction.GetHashCode() * ySpeed * CrossPlatformInputManager.GetAxis("Mouse X"),
                direction.GetHashCode() * zSpeed * CrossPlatformInputManager.GetAxis("Mouse Y")) * transform.localRotation;
        }

        private void Update()
        {
            if (auto)
            {
                transform.localRotation =
                    Quaternion.Euler(direction.GetHashCode() * xSpeed, direction.GetHashCode() * ySpeed,
                        direction.GetHashCode() * zSpeed) * transform.localRotation;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(
                    direction.GetHashCode() * xSpeed * GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y,
                    direction.GetHashCode() * ySpeed * GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X,
                    direction.GetHashCode() * zSpeed * GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y) *
                                          transform.localRotation;
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