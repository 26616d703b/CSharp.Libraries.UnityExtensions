using UnityEngine;
using UnityExtensions.Motion.IK;

namespace UnityExtensions.Motion.Controllers
{
    [RequireComponent(typeof(FullBodyIK))]
    public class BipedMotionController : AMotionController
    {
        #region Attributes

        protected FullBodyIK m_fullBodyIK;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________Z

        public FullBodyIK fullBodyIK
        {
            get { return m_fullBodyIK; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private new void Start()
        {
            base.Start();

            m_fullBodyIK = GetComponent<FullBodyIK>();
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public delegate void FootstepEventHandler();

        public event FootstepEventHandler OnLeftFootTouchesGround;

        public event FootstepEventHandler OnRightFootTouchesGround;

        // Animator

        protected void __OnLeftFootTouchesGround()
        {
            OnLeftFootTouchesGround();
        }

        protected void __OnRightFootTouchesGround()
        {
            OnRightFootTouchesGround();
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget target,
            MatchTargetWeightMask weightMask, float normalisedStartTime, float normalisedEndTime)
        {
            if (m_animator.isMatchingTarget)
                return;

            var normalizeTime = Mathf.Repeat(m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);

            if (normalizeTime > normalisedEndTime)
                return;

            m_animator.MatchTarget(matchPosition, matchRotation, target, weightMask, normalisedStartTime,
                normalisedEndTime);
        }

        #endregion Other Methods

        #endregion Methods
    }
}