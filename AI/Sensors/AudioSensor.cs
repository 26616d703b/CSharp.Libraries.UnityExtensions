using UnityEngine;
using UnityExtensions.Characters;

namespace UnityExtensions.AI.Sensors
{
    public class AudioSensor : ASensor
    {
        #region Attributes

        [SerializeField]
        [Range(0f, 100f)]
        private float m_range = 20f;

        private NavMeshAgent m_agent;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float range
        {
            get { return m_range; }
            set
            {
                m_range = Mathf.Clamp(value, 0f, 100f);

                m_collider.radius = m_range;
            }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private new void Start()
        {
            base.Start();

            m_agent = GetComponentInParent<NavMeshAgent>();

            m_collider.radius = m_range;
        }

        private void OnTriggerStay(Collider other)
        {
            if (enabled)
            {
                if (other.gameObject.tag == Game.Tag.Civilian)
                {
                    m_otherDetected = false;

                    var civilian = other.gameObject.GetComponent<Civilian>();

                    if (civilian != null)
                    {
                        if (civilian.healthStatus == Civilian.Status.GlobalHealth.Sick)
                        {
                            // Calculates the path length
                            var path = new NavMeshPath();

                            if (m_agent.enabled)
                            {
                                m_agent.CalculatePath(other.transform.position, path);
                            }

                            var allWayPoints = new Vector3[path.corners.Length + 2];
                            allWayPoints[0] = transform.position;
                            allWayPoints[allWayPoints.Length - 1] = other.transform.position;

                            for (var i = 0; i < path.corners.Length; i++)
                            {
                                allWayPoints[i + 1] = path.corners[i];
                            }

                            float pathLength = 0;

                            for (var i = 0; i < allWayPoints.Length - 1; i++)
                            {
                                pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
                            }

                            // To determine if the sick civilian is within the field of hearing
                            if (pathLength <= m_collider.radius)
                            {
                                m_otherDetected = true;

                                OnOtherHeard(other.gameObject);
                            }
                        }
                    }
                }
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public event SensorEventHandler OnOtherHeard;

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}