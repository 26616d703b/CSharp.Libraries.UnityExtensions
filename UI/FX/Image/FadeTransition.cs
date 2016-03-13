using UnityEngine;
using UnityEngine.UI;

namespace UnityExtensions.UI.FX
{
    [RequireComponent(typeof(Image))]
    public class FadeTransition : MonoBehaviour
    {
        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Start()
        {
            GetComponent<Image>().CrossFadeAlpha(m_endAlpha, m_duration, false);
            Destroy(gameObject, m_duration);
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods

        #region Attributes

        [SerializeField]
        private readonly float m_endAlpha = 0f;

        [Range(1f, 10f)]
        [SerializeField]
        private readonly float m_duration = 3f;

        #endregion Attributes
    }
}