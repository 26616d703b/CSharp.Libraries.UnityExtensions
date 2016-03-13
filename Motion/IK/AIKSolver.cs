using System;
using UnityEngine;

namespace UnityExtensions.Motion.IK
{
    [Serializable]
    public abstract class AIKSolver
    {
        #region Attributes

        private float m_IKPositionWeight;
        private Vector3 m_IKPosition;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public Transform root { get; private set; }

        public float IKPositionWeight
        {
            get { return m_IKPositionWeight; }
            set { m_IKPositionWeight = Mathf.Clamp(value, 0f, 1f); }
        }

        public Vector3 IKPosition
        {
            get { return m_IKPosition; }
            set { m_IKPosition = value; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        public AIKSolver(Transform root)
        {
            this.root = root;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Update()
        {
            OnUpdate();
        }

        protected abstract void OnUpdate();

        #endregion Other Methods

        #endregion Methods
    }
}