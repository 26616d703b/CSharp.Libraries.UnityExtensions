using UnityEngine;

namespace UnityExtensions.Network.Components
{
    public abstract class CharacterNetworkSync : MonoBehaviour
    {
        #region Attributes

        //public float smoothingDelay = 8f;  // smooth interpolation amount

        protected bool m_gotFirstUpdate = false;

        protected Animator m_animator;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Awake()
        {
        }

        // Use this for initialization
        private void Start()
        {
            m_animator = GetComponent<Animator>();

            if (m_animator == null)
            {
                Debug.LogError("You forgot to put an Animator component on this character prefab!");
            }
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnDestroy()
        {
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void UpdatePosition()
        {
        }

        public void UpdateRotation()
        {
        }

        #endregion Other Methods

        #endregion Methods
    }
}