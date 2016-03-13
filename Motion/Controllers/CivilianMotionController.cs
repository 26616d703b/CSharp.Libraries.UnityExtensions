using UnityEngine;
using UnityExtensions.AI.Controllers;
using UnityExtensions.AI.Sensors;
using UnityExtensions.Characters;

namespace UnityExtensions.Motion.Controllers
{
    public class CivilianMotionController : BipedMotionController
    {
        #region Attributes

        [SerializeField]
        [Range(0f, 1f)]
        private float m_speedDampTime = 0.1f; // Damping time for the Speed parameter.

        // Damping time for the AngularSpeed parameter
        [SerializeField]
        [Range(0f, 1f)]
        private float m_angularSpeedDampTime = 0.7f;

        // Response time for turning an angle into angularSpeed.
        [SerializeField]
        [Range(0f, 1f)]
        private float m_angleResponseTime = 0.6f;

        [SerializeField]
        [Range(0f, 360f)]
        private float m_deadZoneAngle = 5f;

        private AnimatorStateInfo m_shoutLayerAnimatorState;

        private CivilianAIController m_AIController;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float speedDampTime
        {
            get { return m_speedDampTime; }
            set { m_speedDampTime = Mathf.Clamp(value, 0f, 1f); }
        }

        public float angularSpeedDampTime
        {
            get { return m_angularSpeedDampTime; }
            set { m_angularSpeedDampTime = Mathf.Clamp(value, 0f, 1f); }
        }

        public float angleResponseTime
        {
            get { return m_angleResponseTime; }
            set { m_angleResponseTime = Mathf.Clamp(value, 0f, 1f); }
        }

        public float deadZoneAngle
        {
            get { return m_deadZoneAngle; }
            set { m_deadZoneAngle = Mathf.Clamp(value, 0f, 360f) * Mathf.Deg2Rad; }
        }

        public Civilian.Motion state { get; private set; }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private new void Start()
        {
            base.Start();

            m_deadZoneAngle *= Mathf.Deg2Rad;

            // ANIMATION

            if (m_animator.layerCount == 2)
            {
                m_animator.SetLayerWeight(0, 1f);
                m_animator.SetLayerWeight(1, 1f);
            }

            //m_lookAtIK.enabled = false;

            // NAVIGATION
            m_agent.updateRotation = false;

            // AI
            m_AIController = GetComponentInChildren<CivilianAIController>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (selfDriven)
            {
                if (state != Civilian.Motion.Dying && state != Civilian.Motion.GettingUp)
                {
                    // Calculate the parameters that need to be passed to the animator component.

                    float speed;
                    float angle;

                    // If the sick civilian is heard or is in sight...
                    if (m_AIController.sickCivilianDetected)
                    {
                        speed = m_agent.speed;

                        // ... and the angle to turn through is towards the sick civilian.
                        angle = FindAngle(transform.forward,
                            m_AIController.sickCivilian.transform.position - transform.position, transform.up);
                    }
                    else
                    {
                        // Otherwise the speed is a projection of desired velocity on to the forward vector...
                        speed = Vector3.Project(m_agent.desiredVelocity, transform.forward).magnitude;

                        // ... and the angle is the angle between forward and the desired velocity.
                        angle = FindAngle(transform.forward, m_agent.desiredVelocity, transform.up);

                        // If the angle is within the deadZone...
                        if (Mathf.Abs(angle) < m_deadZoneAngle)
                        {
                            // ... set the direction to be along the desired direction and set the angle to be zero.
                            transform.LookAt(transform.position + m_agent.desiredVelocity);

                            angle = 0f;
                        }
                    }

                    // Angular speed is the number of degrees per second.
                    float angularSpeed = angle / m_angleResponseTime;

                    // Set the mecanim parameters and apply the appropriate damping to them.
                    m_animator.SetFloat("AngularSpeed", angularSpeed, angularSpeedDampTime, Time.deltaTime);
                    m_animator.SetFloat("VerticalSpeed", speed, m_speedDampTime, Time.deltaTime);
                }
            }
        }

        private new void FixedUpdate()
        {
            base.FixedUpdate();

            if (m_baseLayerAnimatorState.IsName(Civilian.Motion.Locomotion.GetFullPathHash()))
            {
                state = Civilian.Motion.Locomotion;
            }
            else if (m_baseLayerAnimatorState.IsName(Civilian.Motion.Dying.GetFullPathHash()))
            {
                state = Civilian.Motion.Dying;
            }
            else if (m_baseLayerAnimatorState.IsName(Civilian.Motion.GettingUp.GetFullPathHash()))
            {
                state = Civilian.Motion.GettingUp;
            }

            // ACTIONS

            m_shoutLayerAnimatorState = m_animator.GetCurrentAnimatorStateInfo(1);

            if (m_shoutLayerAnimatorState.IsName(Civilian.Motion.Shouting.GetFullPathHash()))
            {
                state = Civilian.Motion.Shouting;
            }
        }

        private new void OnAnimatorMove()
        {
            base.OnAnimatorMove();

            // The game object's rotation is driven by the animation's rotation.
            transform.rotation = m_animator.rootRotation;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public delegate void EmergencyEventHandler();

        public event EmergencyEventHandler OnHeartAttack;

        public event EmergencyEventHandler OnFellToTheGround;

        // Animator

        private void __OnHeartAttack()
        {
            OnHeartAttack();

            m_AIController.GetSensor<AudioSensor>().enabled = false;
            m_AIController.GetSensor<VisualSensor>().enabled = false;
            m_AIController.GetComponent<SphereCollider>().enabled = false;

            m_animator.SetFloat("VerticalSpeed", 0f);
            m_animator.SetFloat("AngularSpeed", 0f);
        }

        private void __OnFellToTheGround()
        {
            OnFellToTheGround();

            transform.position = new Vector3(transform.position.x, transform.position.y + 0.04f, transform.position.z);
        }

        /*private void __OnGetUpFromTheGround()
        {
            OnGetUpFromTheGround();

            m_AIController.GetSensor<AudioSensor>().enabled = true;
            m_AIController.GetSensor<VisualSensor>().enabled = true;
            m_AIController.GetComponent<SphereCollider>().enabled = true;
        }*/

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        private float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
        {
            // If the vector the angle is being calculated to is 0...
            if (toVector == Vector3.zero)
                // ... the angle between them is 0.
                return 0f;

            // Create a float to store the angle between the facing of the enemy and the direction it's traveling.
            var angle = Vector3.Angle(fromVector, toVector);

            // Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
            var normal = Vector3.Cross(fromVector, toVector);

            // The dot product of the normal with the upVector will be positive if they point in the same direction.
            angle *= Mathf.Sign(Vector3.Dot(normal, upVector));

            // We need to convert the angle we've found from degrees to radians.
            angle *= Mathf.Deg2Rad;

            return angle;
        }

        #endregion Other Methods

        #endregion Methods
    }
}