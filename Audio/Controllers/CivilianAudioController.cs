using UnityEngine;
using UnityExtensions.Motion.Controllers;

namespace UnityExtensions.Audio.Components.Controllers
{
    public class CivilianAudioController : BipedAudioController
    {
        #region Attributes

        [SerializeField]
        private AudioClip m_dyingSound;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private new void Start()
        {
            base.Start();

            CivilianMotionController motionController = (CivilianMotionController)m_motionController;
            motionController.OnHeartAttack += PlayDyingSound;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void PlayDyingSound()
        {
            m_audioSource.PlayOneShot(m_dyingSound);
        }

        #endregion Other Methods

        #endregion Methods
    }
}