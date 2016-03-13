using UnityEngine;
using UnityExtensions.Motion.IK;

namespace UnityExtensions.Motion.Controllers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(LookAtIK))]
    public abstract class AMotionController : MonoBehaviour
    {
        #region Attributes

        public bool selfDriven;

        protected Animator m_animator;

        protected AnimatorStateInfo m_baseLayerAnimatorState;
        // a reference to the current state of the animator, used for base layer

        protected NavMeshAgent m_agent;

        protected LookAtIK m_lookAtIK;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public LookAtIK lookAtIK
        {
            get { return m_lookAtIK; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        protected void Start()
        {
            m_animator = GetComponent<Animator>();
            m_agent = GetComponent<NavMeshAgent>();

            m_lookAtIK = GetComponent<LookAtIK>();

            var lookAtTargetTransform = new GameObject("LookAtTarget").transform;
            lookAtTargetTransform.parent = transform;
            lookAtTargetTransform.localPosition = new Vector3(0f, 1.6f, 1f);
            lookAtTargetTransform.localRotation = Quaternion.identity;
            lookAtTargetTransform.localScale = Vector3.one;

            m_lookAtIK.target = lookAtTargetTransform;
        }

        protected void FixedUpdate()
        {
            m_baseLayerAnimatorState = m_animator.GetCurrentAnimatorStateInfo(0);
        }

        protected void OnAnimatorMove()
        {
            // Set the NavMeshAgent's velocity to the change in position since the last frame, by the time it took for the last frame.
            m_agent.velocity = m_animator.deltaPosition / Time.deltaTime;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public delegate void MotionEventHandler();

        // Animator

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}