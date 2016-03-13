using System;
using UnityEngine;
using UnityExtensions.Cameras.Controllers;
using UnityExtensions.Characters;
using UnityExtensions.Motion.SMBs;
using UnityStandardAssets.CrossPlatformInput;
using XInputDotNetPure;

namespace UnityExtensions.Motion.Controllers
{
    public class PlayerMotionController : BipedMotionController
    {
        #region Attributes

        public static PlayerMotionController current { get; set; }

        //private bool m_autoMoving;

        private AnimatorStateInfo m_upperBodyLayerAnimatorState;

        private AnimatorStateInfo m_rightArmLayerAnimatorState;
        // a reference to the current state of the animator, used for RightArmLayer

        private Player.Motion m_state;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public bool hasReachedDestination
        {
            get { return m_agent.remainingDistance < m_agent.stoppingDistance; }
        }

        public bool isIdling
        {
            get
            {
                return m_state == Player.Motion.Idle
                       || m_state == Player.Motion.Idle01
                       || m_state == Player.Motion.Idle02
                       || m_state == Player.Motion.Idle03
                       || m_state == Player.Motion.Idle04
                       || m_state == Player.Motion.Idle05
                       || m_state == Player.Motion.Idle06
                       || m_state == Player.Motion.Idle07;
            }
        }

        public bool isWalking
        {
            get
            {
                return m_state == Player.Motion.WalkingForward || m_state == Player.Motion.WalkingBackwards
                       || m_state == Player.Motion.WalkingStrafLeft || m_state == Player.Motion.WalkingStrafRight;
            }
        }

        public bool isRunning
        {
            get
            {
                return m_state == Player.Motion.RunningForward || m_state == Player.Motion.RunningBackwards
                       || m_state == Player.Motion.RunningStrafLeft || m_state == Player.Motion.RunningStrafRight;
            }
        }

        public bool isMoving
        {
            get { return isWalking || isRunning; }
        }

        public Player.Motion state
        {
            get { return m_state; }
            set
            {
                switch (value)
                {
                    case Player.Motion.Idle:
                    case Player.Motion.Idle01:
                    case Player.Motion.Idle02:
                    case Player.Motion.Idle03:
                    case Player.Motion.Idle04:
                    case Player.Motion.Idle05:
                    case Player.Motion.Idle06:
                    case Player.Motion.Idle07:

                        m_animator.SetInteger("CurrentAction", Player.Motion.Idle.GetHashCode());
                        m_animator.SetInteger("CurrentAltAction", Player.Motion.Idle.GetHashCode());

                        break;

                    case Player.Motion.WalkingForward:
                    case Player.Motion.WalkingBackwards:
                    case Player.Motion.RunningForward:
                    case Player.Motion.RunningBackwards:
                    case Player.Motion.WalkingStrafLeft:
                    case Player.Motion.WalkingStrafRight:
                    case Player.Motion.RunningStrafLeft:
                    case Player.Motion.RunningStrafRight:
                        throw new Exception("Locomotion states are not allowed.");

                    case Player.Motion.Waving:

                        m_animator.SetInteger("CurrentAltAction", Player.Motion.Waving.GetHashCode());

                        break;

                    case Player.Motion.Kneeling:

                        m_animator.SetInteger("CurrentAction", Player.Motion.Kneeling.GetHashCode());

                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public RandomIdleSMB randomIdleSMB { get; private set; }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void OnEnable()
        {
            current = this;
        }

        // Use this for initialization
        private new void Start()
        {
            base.Start();

            if (m_animator.layerCount == 3)
            {
                //m_animator.SetLayerWeight(0, 1f);
                m_animator.SetLayerWeight(1, 1f);
                m_animator.SetLayerWeight(2, 1f);
            }

            randomIdleSMB = (RandomIdleSMB)m_animator.GetBehaviour<StateMachineBehaviour>();
        }

        private void Update()
        {
            m_lookAtIK.target.position = MultiPOVCameraController.current.focusPoint;
        }

        // Update is called once per frame
        private new void FixedUpdate()
        {
            base.FixedUpdate();

            float verticalSpeed = 0;
            float horizontalSpeed = 0;

            if (selfDriven)
            {
                verticalSpeed = Vector3.Project(m_agent.desiredVelocity, transform.forward).magnitude;
            }
            else
            {
                verticalSpeed = CrossPlatformInputManager.GetAxis("Vertical");
                horizontalSpeed = CrossPlatformInputManager.GetAxis("Horizontal");

                //m_autoMoving = false;
            }

            m_animator.SetFloat("VerticalSpeed", verticalSpeed);
            m_animator.SetFloat("HorizontalSpeed", horizontalSpeed);

            // if we are in any state

            //Wave
            if (CrossPlatformInputManager.GetButtonDown("Wave"))
            {
                m_animator.SetInteger("CurrentAction", Player.Motion.Waving.GetHashCode());
            }

            // BASE LAYER

            // if we are currently in idle state
            if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle.GetFullHashPath())
                || m_baseLayerAnimatorState.IsName(Player.Motion.Idle01.GetFullHashPath())
                || m_baseLayerAnimatorState.IsName(Player.Motion.Idle02.GetFullHashPath())
                || m_baseLayerAnimatorState.IsName(Player.Motion.Idle03.GetFullHashPath())
                || m_baseLayerAnimatorState.IsName(Player.Motion.Idle04.GetFullHashPath())
                || m_baseLayerAnimatorState.IsName(Player.Motion.Idle05.GetFullHashPath())
                || m_baseLayerAnimatorState.IsName(Player.Motion.Idle06.GetFullHashPath())
                || m_baseLayerAnimatorState.IsName(Player.Motion.Idle07.GetFullHashPath()))
            {
                if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle01.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle01;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle02.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle02;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle03.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle03;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle04.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle04;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle05.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle05;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle06.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle06;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.Idle07.GetFullHashPath()))
                {
                    m_state = Player.Motion.Idle07;
                }

                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }
            // if we are currently in a state called walkForward or walkBackward
            else if (m_baseLayerAnimatorState.IsName(Player.Motion.WalkingForward.GetFullHashPath())
                     || m_baseLayerAnimatorState.IsName(Player.Motion.WalkingBackwards.GetFullHashPath())
                     || m_baseLayerAnimatorState.IsName(Player.Motion.WalkingStrafLeft.GetFullHashPath())
                     || m_baseLayerAnimatorState.IsName(Player.Motion.WalkingStrafRight.GetFullHashPath()))
            {
                if (m_baseLayerAnimatorState.IsName(Player.Motion.WalkingForward.GetFullHashPath()))
                {
                    m_state = Player.Motion.WalkingForward;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.WalkingBackwards.GetFullHashPath()))
                {
                    m_state = Player.Motion.WalkingBackwards;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.WalkingStrafLeft.GetFullHashPath()))
                {
                    m_state = Player.Motion.WalkingStrafLeft;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.WalkingStrafLeft.GetFullHashPath()))
                {
                    m_state = Player.Motion.WalkingStrafRight;
                }

                if (CrossPlatformInputManager.GetButton("Run"))
                {
                    m_animator.SetBool("Running", true);
                }
            }
            else if (m_baseLayerAnimatorState.IsName(Player.Motion.RunningForward.GetFullHashPath())
                     || m_baseLayerAnimatorState.IsName(Player.Motion.RunningBackwards.GetFullHashPath())
                     || m_baseLayerAnimatorState.IsName(Player.Motion.RunningStrafLeft.GetFullHashPath())
                     || m_baseLayerAnimatorState.IsName(Player.Motion.RunningStrafRight.GetFullHashPath()))
            {
                if (m_baseLayerAnimatorState.IsName(Player.Motion.RunningForward.GetFullHashPath()))
                {
                    m_state = Player.Motion.RunningForward;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.RunningBackwards.GetFullHashPath()))
                {
                    m_state = Player.Motion.RunningBackwards;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.RunningStrafLeft.GetFullHashPath()))
                {
                    m_state = Player.Motion.RunningStrafLeft;
                }
                else if (m_baseLayerAnimatorState.IsName(Player.Motion.RunningStrafRight.GetFullHashPath()))
                {
                    m_state = Player.Motion.RunningStrafRight;
                }

                if (!CrossPlatformInputManager.GetButton("Run"))
                {
                    m_animator.SetBool("Running", false);
                }
            }

            // ACTIONS

            // Kneeling
            else if (m_baseLayerAnimatorState.IsName(Player.Motion.Kneeling.GetFullHashPath()))
            {
                m_state = Player.Motion.Kneeling;

                if (CrossPlatformInputManager.GetButtonDown("Cancel"))
                {
                    m_animator.SetInteger("CurrentAction", Player.Motion.Idle.GetHashCode());
                }
            }

            // UPPER_BODY LAYER

            // RIGHT_ARM LAYER

            m_rightArmLayerAnimatorState = m_animator.GetCurrentAnimatorStateInfo(2);

            if (m_rightArmLayerAnimatorState.IsName(Player.Motion.Waving.GetFullHashPath()))
            {
                m_state = Player.Motion.Waving;

                if (CrossPlatformInputManager.GetButtonUp("Wave"))
                {
                    m_animator.SetInteger("CurrentAltAction", Player.Motion.Idle.GetHashCode());
                }
            }
        }

        /*float holdWeight;
        float holdWeightVel;
        float pickUpTime = 0.8f;*/

        private void OnAnimatorIK(int layer)
        {
            /*holdWeight = Mathf.SmoothDamp(holdWeight, 1f, ref holdWeightVel, pickUpTime);

            m_animator.SetIKPosition(AvatarIKGoal.LeftHand, Vector3.Lerp(m_animator.GetIKPosition(AvatarIKGoal.LeftHand), handTransform.position, holdWeight));
            m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

            m_animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Lerp(m_animator.GetIKRotation(AvatarIKGoal.LeftHand), handTransform.rotation, holdWeight));
            m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);*/
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public event MotionEventHandler OnKneelEnd;

        // Animator

        private new void __OnLeftFootTouchesGround()
        {
            base.__OnLeftFootTouchesGround();

            GamePad.SetVibration(PlayerIndex.One, 0.3f, 0f);
        }

        private new void __OnRightFootTouchesGround()
        {
            base.__OnRightFootTouchesGround();

            GamePad.SetVibration(PlayerIndex.One, 0f, 0.3f);
        }

        private void __OnKneelEnd()
        {
            OnKneelEnd();
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Move(Vector3 destination)
        {
            var wasSelfDriven = selfDriven;

            selfDriven = true;

            transform.LookAt(destination);

            MultiPOVCameraController.current.useMouseLook = false;
            MultiPOVCameraController.current.focusPoint = destination;

            m_agent.destination = destination;

            selfDriven = wasSelfDriven;
        }

        #endregion Other Methods

        #endregion Methods
    }
}