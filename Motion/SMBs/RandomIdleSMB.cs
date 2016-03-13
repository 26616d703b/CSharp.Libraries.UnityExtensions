using UnityEngine;

namespace UnityExtensions.Motion.SMBs
{
    public class RandomIdleSMB : StateMachineBehaviour
    {
        [SerializeField]
        [Range(0, 10)]
        private int m_numberOfStates;

        public int numberOfStates
        {
            get { return m_numberOfStates; }
            set { m_numberOfStates = Mathf.Clamp(value, 0, 10); }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Set RandomIdle based on how many states there are.
            var randomSelection = Random.Range(0, m_numberOfStates);
            animator.SetInteger("RandomIdle", randomSelection);
        }
    }
}