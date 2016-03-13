using System;
using UnityEngine;
using UnityExtensions.AI.Sensors;
using UnityExtensions.Characters;
using UnityExtensions.Motion.Controllers;
using UnityExtensions.UI.HUD.Markers.Waypoint;
using Random = UnityEngine.Random;

namespace UnityExtensions.AI.Controllers
{
    [RequireComponent(typeof(AudioSensor))]
    [RequireComponent(typeof(VisualSensor))]
    public class CivilianAIController : MonoBehaviour
    {
        #region Attributes

        public bool randomCircuit = true;
        public bool randomPath = true;

        [Space(10)]
        [SerializeField]
        [Range(3f, 5f)]
        private float m_alertSpeed = 5f;

        [SerializeField]
        [Range(0f, 10f)]
        private float m_alertWaitTime;

        [SerializeField]
        [Range(0.5f, 2.5f)]
        private float m_walkAroundSpeed = 2f;

        [SerializeField]
        [Range(0f, 60f)]
        private float m_walkAroundWaitTime;

        private NavMeshAgent m_agent;

        private AudioSensor m_audioSensor;
        private VisualSensor m_visualSensor;

        private CivilianMotionController m_motionController;

        private bool m_sickCivilianHeard;
        private bool m_sickCivilianInSight;

        private float m_alertTimer;
        private float m_walkAroundTimer;

        private int m_currentWaypointCircuitIndex;
        private int m_currentWaypointIndex;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float alertSpeed
        {
            get { return m_alertSpeed; }
            set { m_alertSpeed = Mathf.Clamp(value, 3f, 5f); }
        }

        public float alertWaitTime
        {
            get { return m_alertWaitTime; }
            set { m_alertWaitTime = Mathf.Clamp(value, 0f, 5f); }
        }

        public float walkAroundSpeed
        {
            get { return m_walkAroundSpeed; }
            set { m_walkAroundSpeed = Mathf.Clamp(value, 0.5f, 2.5f); }
        }

        public float walkAroundWaitTime
        {
            get { return m_walkAroundWaitTime; }
            set { m_walkAroundWaitTime = Mathf.Clamp(value, 0f, 60f); }
        }

        public bool sickCivilianDetected
        {
            get { return m_sickCivilianHeard || m_sickCivilianInSight; }
        }

        public GameObject sickCivilian { get; private set; }

        public T GetSensor<T>() where T : ASensor
        {
            if (typeof(T) == typeof(AudioSensor))
            {
                return m_audioSensor as T;
            }
            if (typeof(T) == typeof(VisualSensor))
            {
                return m_visualSensor as T;
            }
            throw new Exception("The given type must inherit from the Sensor class");
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private void Start()
        {
            m_agent = GetComponentInParent<NavMeshAgent>();

            m_audioSensor = GetComponent<AudioSensor>();
            m_visualSensor = GetComponent<VisualSensor>();

            m_audioSensor.OnOtherHeard += OnSickCivilianHeard;
            m_visualSensor.OnOtherInSight += OnSickCivilianInSight;

            m_motionController = GetComponentInParent<CivilianMotionController>();
        }

        private void OnDestroy()
        {
            m_audioSensor.OnOtherHeard -= OnSickCivilianHeard;
            m_visualSensor.OnOtherInSight -= OnSickCivilianInSight;
        }

        // Update is called once per frame
        private void Update()
        {
            if (m_sickCivilianHeard || m_sickCivilianInSight)
            {
                Alert();
            }
            else
            {
                if (m_motionController.state != Civilian.Motion.Dying &&
                    m_motionController.state != Civilian.Motion.GettingUp)
                {
                    WalkAround();
                }
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        private void OnSickCivilianHeard(GameObject civilian)
        {
            m_sickCivilianHeard = true;
            sickCivilian = civilian;
        }

        private void OnSickCivilianInSight(GameObject civilian)
        {
            m_sickCivilianInSight = true;
            sickCivilian = civilian;
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Alert()
        {
            m_agent.speed = m_alertSpeed;

            m_motionController.lookAtIK.enabled = true;
            m_motionController.lookAtIK.target.position =
                sickCivilian.GetComponent<CivilianMotionController>().fullBodyIK.bones.head.position;

            if (m_agent.remainingDistance < m_agent.stoppingDistance)
            {
                /*m_alertTimer += Time.deltaTime;

                if (m_walkAroundTimer >= m_walkAroundWaitTime)
                {
                }

                m_alertTimer = 0f;*/

                m_agent.speed = 0f;
            }

            m_agent.destination = sickCivilian.transform.position;
        }

        public void WalkAround()
        {
            if (WaypointSystem.current == null || WaypointSystem.current.circuits == null
                || WaypointSystem.current.circuits.Length == 0)
            {
                Debug.LogError("You must first put a WaypointSystem in the scene, then (at least) a WaypointCircuit...");
                return;
            }

            m_agent.speed = m_walkAroundSpeed;

            if (m_agent.remainingDistance < m_agent.stoppingDistance)
            {
                m_walkAroundTimer += Time.deltaTime;

                if (m_walkAroundTimer >= m_walkAroundWaitTime)
                {
                    if (randomCircuit)
                    {
                        m_currentWaypointCircuitIndex = Random.Range(0, WaypointSystem.current.circuits.Length - 1);
                    }

                    if (randomPath)
                    {
                        m_currentWaypointIndex = Random.Range(0,
                            WaypointSystem.current.circuits[m_currentWaypointCircuitIndex].Waypoints.Length - 1);
                    }
                    else
                    {
                        if (m_currentWaypointIndex ==
                            WaypointSystem.current.circuits[m_currentWaypointCircuitIndex].Waypoints.Length - 1)
                        {
                            m_currentWaypointIndex = 0;
                        }
                        else
                        {
                            m_currentWaypointIndex++;
                        }
                    }

                    m_walkAroundTimer = 0f;
                }
            }
            else
            {
                m_walkAroundTimer = 0f;
            }

            m_agent.destination =
                WaypointSystem.current.circuits[m_currentWaypointCircuitIndex].Waypoints[m_currentWaypointIndex].position;
        }

        #endregion Other Methods

        #endregion Methods
    }
}