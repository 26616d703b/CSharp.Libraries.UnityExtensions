using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExtensions.AI.Systems.MAS.Core
{
    [Serializable]
    public class Scheduler
    {
        #region Attributes

        //private Agent m_owner;

        private readonly List<Behaviour> m_enabledBehaviours = new List<Behaviour>();
        private readonly List<Behaviour> m_disabledBehaviours = new List<Behaviour>();

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public List<Behaviour> behaviours
        {
            get
            {
                var list = new List<Behaviour>();
                list.AddRange(m_enabledBehaviours);
                list.AddRange(m_disabledBehaviours);

                return list;
            }
            set
            {
                m_enabledBehaviours.Clear();
                m_disabledBehaviours.Clear();

                foreach (var behaviour in value)
                {
                    if (behaviour.enabled)
                    {
                        m_enabledBehaviours.Add(behaviour);
                    }
                    else
                    {
                        m_disabledBehaviours.Add(behaviour);
                    }
                }
            }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        /*public Scheduler(Agent owner)
        {
            m_owner = owner;
        }*/

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Add(Behaviour behaviour)
        {
            m_enabledBehaviours.Add(behaviour);
        }

        public void AddRange(List<Behaviour> behaviours)
        {
            m_enabledBehaviours.AddRange(behaviours);
        }

        public void Block(Behaviour behaviour)
        {
            if (m_enabledBehaviours.Remove(behaviour))
            {
                m_disabledBehaviours.Add(behaviour);
            }
        }

        public void Remove(Behaviour behaviour)
        {
            if (!m_disabledBehaviours.Remove(behaviour))
            {
                m_enabledBehaviours.Remove(behaviour);
            }
        }

        public void RemoveFromEnabled(Behaviour behaviour)
        {
            m_enabledBehaviours.Remove(behaviour);
        }

        public void RemoveFromDisabled(Behaviour behaviour)
        {
            m_disabledBehaviours.Remove(behaviour);
        }

        #endregion Other Methods

        #endregion Methods
    }
}