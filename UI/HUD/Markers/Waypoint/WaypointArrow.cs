using UnityEngine;

namespace UnityExtensions.UI.HUD.Markers.Waypoint
{
    public class WaypointArrow : MonoBehaviour
    {
        #region Attributes

        public Sprite icon;
        public Color color = Color.white;
        public float rotation = 0;
        public Vector2 size = new Vector2(128, 128);
        public Vector2 pivot = new Vector2(0, 0);

        private Vector2 m_position = new Vector2(0, 0);
        private Vector2 m_screenPivot;
        private Rect m_rect;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void OnGUI()
        {
            var matrixBackup = GUI.matrix;
            GUIUtility.RotateAroundPivot(rotation, m_screenPivot);
            GUI.color = color;
            GUI.DrawTexture(m_rect, icon.texture);
            GUI.matrix = matrixBackup;
        }

        // Update is called once per frame
        private void Update()
        {
            m_position = new Vector2(transform.position.x, transform.position.y);

            var pixelPosition = new Vector2(WaypointSystem.current.targetCamera.pixelWidth * m_position.x,
                WaypointSystem.current.targetCamera.pixelHeight * (1f - m_position.y));
            m_screenPivot = pixelPosition;
            m_rect = new Rect(pixelPosition.x - size.x * 0.5f + pivot.x, pixelPosition.y - size.y * 0.5f + pivot.y, size.x,
                size.y);
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