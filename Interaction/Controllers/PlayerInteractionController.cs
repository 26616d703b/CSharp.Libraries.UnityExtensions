using UnityEngine;
using UnityExtensions.Characters;
using UnityExtensions.Runtime.Managers;

namespace UnityExtensions.Interaction.Controllers
{
    public class PlayerInteractionController : ACharacterInteractionController
    {
        #region Attributes

        public static PlayerInteractionController current { get; set; }

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void OnEnable()
        {
            current = this;
        }

        // Use this for initialization
        private new void Start()
        {
        }

        private void Update()
        {
            if (Camera.main != null)
            {
                if (OfflineGameManager.instance.interactionReference == Player.Interaction.Reference.Crosshair)
                {
                    var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                    var hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit, m_maxDistance))
                    {
                        // ignore trigger colliders
                        if (hit.collider.isTrigger)
                            return;

                        var otherInteractionController = hit.transform.GetComponent<AInteractionController>();

                        if (otherInteractionController)
                        {
                            if (otherInteractionController.canInteractWithOthers)
                            {
                                canInteractWith = otherInteractionController;

                                canInteractWith.Highlight();
                            }
                        }
                    }
                    else
                    {
                        if (canInteractWith)
                            canInteractWith.StopHighlight();

                        canInteractWith = null;
                    }
                }
            }
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