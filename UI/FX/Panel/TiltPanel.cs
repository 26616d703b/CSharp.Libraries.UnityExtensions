using UnityEngine;

namespace UnityExtensions.UI.FX
{
    /// <summary>
    ///     WARNING : Canvas Render Mode must be in Screen Space - Camera
    /// </summary>
    public class TiltPanel : MonoBehaviour
    {
        #region Attributes

        public Vector2 degrees = new Vector2(5f, 3f);
        public float range = 1f;

        private Quaternion m_startRotation;
        private Vector2 m_rotation = Vector2.zero;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private void Start()
        {
            m_startRotation = transform.localRotation;
        }

        private void Update()
        {
            var delta = Time.deltaTime;
            var mousePosition = Input.mousePosition;

            var halfWidth = Screen.width * 0.5f;
            var halfHeight = Screen.height * 0.5f;

            if (range < 0.1f) range = 0.1f;

            var x = Mathf.Clamp((mousePosition.x - halfWidth) / halfWidth / range, -1f, 1f);
            var y = Mathf.Clamp((mousePosition.y - halfHeight) / halfHeight / range, -1f, 1f);

            m_rotation = Vector2.Lerp(m_rotation, new Vector2(x, y), delta * 5f);

            transform.localRotation = m_startRotation *
                                      Quaternion.Euler(-m_rotation.y * degrees.y, m_rotation.x * degrees.x, 0f);
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