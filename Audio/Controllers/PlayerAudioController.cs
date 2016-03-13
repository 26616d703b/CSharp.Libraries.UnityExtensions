using UnityEngine;
using UnityExtensions.Characters;

namespace UnityExtensions.Audio.Components.Controllers
{
    public class PlayerAudioController : AAudioController
    {
        #region Attributes

        public static PlayerAudioController current { get; set; }

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
            base.Start();

            AudioListener.volume = PlayerPrefs.GetFloat(Player.Preferences.Audio.Sound.Volume);
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