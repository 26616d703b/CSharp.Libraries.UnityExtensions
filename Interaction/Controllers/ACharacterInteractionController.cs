using UnityEngine;

namespace UnityExtensions.Interaction.Controllers
{
    public abstract class ACharacterInteractionController : AInteractionController
    {
        #region Attributes

        [HideInInspector]
        public AInteractionController canInteractWith;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public bool canInteract
        {
            get { return m_interactingWith != null; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void RequestInteraction(AInteractionController otherInteractionController)
        {
            // ...
        }

        #endregion Other Methods

        #endregion Methods
    }
}