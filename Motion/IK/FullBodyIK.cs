using UnityEngine;

namespace UnityExtensions.Motion.IK
{
    [RequireComponent(typeof(Animator))]
    public class FullBodyIK : MonoBehaviour
    {
        #region Attributes

        [SerializeField]
        private readonly Biped.Bone.Map m_bones = new Biped.Bone.Map();

        private Animator m_animator;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public Biped.Bone.Map bones
        {
            get { return m_bones; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        [ContextMenu("Auto-Assign Bones")]
        private void Start()
        {
            m_animator = GetComponent<Animator>();

            if (!m_bones.IsValid())
            {
                m_bones.Assign(m_animator);
            }
        }

        private void OnAnimatorIK()
        {
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