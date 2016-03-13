using UnityEngine;
using UnityExtensions.Motion.Controllers;

namespace UnityExtensions.Audio.Components.Controllers
{
    public abstract class BipedAudioController : AAudioController
    {
        #region Attributes

        public bool useFootstepSounds = true;

        [SerializeField]
        protected AudioClip[] m_footstepSounds;

        // an array of footstep sounds that will be randomly selected from.

        protected BipedMotionController m_motionController;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        protected void OnDestroy()
        {
            m_motionController.OnLeftFootTouchesGround -= PlayFootstepSound;
            m_motionController.OnRightFootTouchesGround -= PlayFootstepSound;
        }

        // Use this for initialization
        private new void Start()
        {
            base.Start();

            m_audioSource = GetComponentInChildren<AudioSource>();

            m_motionController = GetComponentInParent<BipedMotionController>();
            m_motionController.OnLeftFootTouchesGround += PlayFootstepSound;
            m_motionController.OnRightFootTouchesGround += PlayFootstepSound;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void PlayFootstepSound()
        {
            if (useFootstepSounds)
            {
                // pick & play a random footstep sound from the array,
                // excluding sound at index 0
                var n = Random.Range(1, m_footstepSounds.Length);
                m_audioSource.clip = m_footstepSounds[n];
                m_audioSource.PlayOneShot(m_audioSource.clip);
                // move picked sound to index 0 so it's not picked next time
                m_footstepSounds[n] = m_footstepSounds[0];
                m_footstepSounds[0] = m_audioSource.clip;
            }
        }

        #endregion Other Methods

        #endregion Methods
    }
}