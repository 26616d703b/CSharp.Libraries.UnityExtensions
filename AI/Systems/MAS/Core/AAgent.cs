using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions.AI.Systems.MAS.Langage.ACL;

namespace UnityExtensions.AI.Systems.MAS.Core
{
    public abstract class AAgent : MonoBehaviour
    {
        #region Properties

        public enum State
        {
            Idle,
            Active,
            Terminating
        }

        #endregion Properties

        #region Attributes

        public ID id;

        //protected Scheduler m_scheduler;
        protected State m_state;

        protected List<ID> m_addresses = new List<ID>();

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public List<ID> addresses
        {
            get { return m_addresses; }
        }

        public State state
        {
            get { return m_state; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Start()
        {
            //m_scheduler = new Scheduler(this);

            m_state = State.Active;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public abstract void OnMessageReceived(Message message);

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void AddAddress(ID address)
        {
            if (!m_addresses.Contains(address))
            {
                m_addresses.Add(id);
            }
        }

        public void AddAddresses(List<ID> addresses)
        {
            foreach (var address in addresses)
            {
                AddAddress(address);
            }
        }

        /*public void AddBehaviour(Behavior behavior)
        {
            m_scheduler.Add(this);
        }

        public void AddBehaviours(List<Behaviour> behaviors)
        {
            m_scheduler.AddRange(behaviors);
        }*/

        public void RemoveAddress(ID address)
        {
            m_addresses.Remove(address);
        }

        public void RemoveAllAddresses()
        {
            m_addresses.Clear();
        }

        public abstract void Broadcast(object content);

        public abstract void Send(object content, ID destination);

        public void Wait(float seconds)
        {
            m_state = State.Idle;

            StopCoroutine("Wait_Coroutine");
            StartCoroutine("Wait_Coroutine", Wait_Coroutine(seconds));
        }

        private IEnumerable Wait_Coroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            m_state = State.Active;
        }

        #endregion Other Methods

        #endregion Methods
    }
}