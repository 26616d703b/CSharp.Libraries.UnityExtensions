using UnityEngine;

namespace UnityExtensions.Audio.Components.Controllers
{
    public abstract class AAudioController : MonoBehaviour
    {
        #region Attributes

        protected AudioSource m_audioSource;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        protected void Start()
        {
            m_audioSource = GetComponentInChildren<AudioSource>();
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