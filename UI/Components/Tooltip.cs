using UnityEngine;

namespace UnityExtensions.UI.Components
{
    public class Tooltip : MonoBehaviour
    {
        #region Attributes

        [Tooltip("Attached or not to the GameObject.")]
        public bool attached;

        [Space]
        public RectTransform content;

        public Vector3 shifting;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private void Start()
        {
            if (content == null)
                throw new MissingReferenceException();
        }

        // Update is called once per frame
        private void Update()
        {
            if (content.gameObject.activeSelf)
            {
                RaycastHit hit;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

                content.localPosition = attached ? transform.position + shifting : hit.point * Screen.dpi + shifting;
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        private void OnMouseEnter()
        {
            if (!isActiveAndEnabled)
                return;

            content.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            if (!isActiveAndEnabled)
                return;

            content.gameObject.SetActive(false);
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}