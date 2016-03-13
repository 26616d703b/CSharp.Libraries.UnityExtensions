using UnityEngine;
using UnityExtensions.Characters;

namespace UnityExtensions.AI.Sensors
{
    public class VisualSensor : ASensor
    {
        #region Attributes

        [SerializeField]
        [Range(0f, 180f)]
        private float m_angleOfView = 114f;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float angleOfView
        {
            get { return m_angleOfView; }
            set { m_angleOfView = Mathf.Clamp(value, 0f, 180f); }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private new void Start()
        {
            base.Start();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == Game.Tag.Civilian)
            {
                m_otherDetected = false;

                // To determine if the sick civilian is within the field of view
                var direction = other.transform.position - transform.position;
                var angle = Vector3.Angle(direction, transform.forward);

                // If the angle is less than half the field of view angle than the player is within the field of view
                if (angle < angleOfView * 0.5f)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position + transform.up / 2, direction.normalized, out hit, m_collider.radius))
                    {
                        var civilian = hit.transform.gameObject.GetComponent<Civilian>();

                        if (civilian != null)
                        {
                            if (civilian.healthStatus == Civilian.Status.GlobalHealth.Sick)
                            {
                                m_otherDetected = true;

                                OnOtherInSight(hit.transform.gameObject);
                            }
                        }
                    }
                }
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public event SensorEventHandler OnOtherInSight;

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}