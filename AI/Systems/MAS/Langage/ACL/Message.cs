using System.Collections.Generic;
using UnityExtensions.AI.Systems.MAS.Core;

namespace UnityExtensions.AI.Systems.MAS.Langage.ACL
{
    public class Message
    {
        #region Attributes

        private List<ID> m_destination = new List<ID>();

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public ID sender { get; set; }

        public List<ID> receivers
        {
            get { return m_destination; }
            set { m_destination = value; }
        }

        public Performative performative { get; set; }

        public object content { get; set; }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void AddReceiver(ID receiver)
        {
            if (!m_destination.Contains(receiver))
            {
                m_destination.Add(receiver);
            }
        }

        public void AddReceivers(List<ID> receivers)
        {
            foreach (var receiver in receivers)
            {
                AddReceiver(receiver);
            }
        }

        public void RemoveReceiver(ID receiver)
        {
            m_destination.Remove(receiver);
        }

        public void RemoveAllReceivers()
        {
            m_destination.Clear();
        }

        #endregion Other Methods

        #endregion Methods
    }
}