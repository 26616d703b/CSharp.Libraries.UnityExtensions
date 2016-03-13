using UnityEngine;

namespace UnityExtensions.Motion.IK
{
    [RequireComponent(typeof(Animator))]
    public class LookAtIK : MonoBehaviour
    {
        #region Attributes

        public Transform target;

        [Range(0f, 1f)]
        [SerializeField]
        private float m_weight = 1f;

        [Range(0f, 1f)]
        [SerializeField]
        private float m_bodyWeight = 1f;

        [Range(0f, 1f)]
        [SerializeField]
        private float m_headWeight = 1f;

        [Range(0f, 1f)]
        [SerializeField]
        private float m_eyesWeight = 1f;

        [Range(0f, 1f)]
        [SerializeField]
        private float m_clampWeight = 0.5f;

        private Animator m_animator;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float weight
        {
            get { return m_weight; }
            set { m_weight = Mathf.Clamp(value, 0f, 1f); }
        }

        public float bodyWeight
        {
            get { return m_bodyWeight; }
            set { m_bodyWeight = Mathf.Clamp(value, 0f, 1f); }
        }

        public float headWeight
        {
            get { return m_headWeight; }
            set { m_headWeight = Mathf.Clamp(value, 0f, 1f); }
        }

        public float eyesWeight
        {
            get { return m_eyesWeight; }
            set { m_eyesWeight = Mathf.Clamp(value, 0f, 1f); }
        }

        public float clampWeight
        {
            get { return m_clampWeight; }
            set { m_clampWeight = Mathf.Clamp(value, 0f, 1f); }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layer)
        {
            m_animator.SetLookAtPosition(target.position);
            m_animator.SetLookAtWeight(m_weight, m_bodyWeight, m_headWeight, m_eyesWeight, m_clampWeight);
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