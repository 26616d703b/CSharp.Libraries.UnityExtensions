using UnityEngine;

namespace UnityExtensions.AI.Sensors
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class ASensor : MonoBehaviour
    {
        #region Attributes

        protected bool m_otherDetected;

        protected SphereCollider m_collider;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        protected void Start()
        {
            m_collider = GetComponent<SphereCollider>();
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public delegate void SensorEventHandler(GameObject detectedObject);

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}