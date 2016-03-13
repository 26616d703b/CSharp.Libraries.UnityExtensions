using UnityEngine;
using UnityEngine.UI;
using UnityExtensions.Localization;
using UnityExtensions.UI.Components;

namespace UnityExtensions.Runtime.Managers
{
    public class PauseMenuManager : AMenuManager
    {
        #region Attributes

        public static PauseMenuManager instance { get; set; }

        [Header("Navigation")]
        public Button resumeButton;

        public Button quitButton;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private new void Awake()
        {
            base.Awake();

            instance = this;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        protected override void OnLocaleChanged()
        {
            resumeButton.GetComponentInChildren<Label>(true).text =
                LocaleManager.instance.GetValue("Game.Pause.Resume");
            quitButton.GetComponentInChildren<Label>(true).text =
                LocaleManager.instance.GetValue("Game.Pause.ReturnToMenu");
        }

        // UI

        public void OnResumeClicked()
        {
        }

        public void OnConfirmQuitClicked()
        {
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}