using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityExtensions.Characters;
using UnityExtensions.Localization;
using UnityExtensions.Motion.Controllers;
using UnityExtensions.UI.HUD.Managers;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityExtensions.Runtime.Managers
{
    public abstract class AGameManager : MonoBehaviour
    {
        #region Attributes

        [Header("Debug")]
        public bool freezeMovementWhenPaused = true;

        public Player.Interaction.Reference interactionReference = Player.Interaction.Reference.Crosshair;

        private Game.State m_currentGameState = Game.State.Running;

        #endregion Attributes

        #region Methods

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        protected void Awake()
        {
            LocaleManager.instance.OnLocaleChange += OnLocaleChange;

            var preferredLocale = PlayerPrefs.GetString(Player.Preferences.Localization.Locale, Locale.en_US.Name());
            LocaleManager.instance.ChangeLocale((Locale)Enum.Parse(typeof(Locale), preferredLocale));
        }

        protected void OnDestroy()
        {
            LocaleManager.instance.OnLocaleChange -= OnLocaleChange;
        }

        protected void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                switch (m_currentGameState)
                {
                    case Game.State.Paused:

                        Resume();

                        break;

                    case Game.State.Running:

                        Pause();

                        break;

                    default:
                        break;
                }
            }
        }

        #endregion Inherited Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public Game.State currentGameState
        {
            get { return m_currentGameState; }
        }

        #endregion Accessors and Mutators

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        protected abstract void OnLocaleChange();

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Pause()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (freezeMovementWhenPaused)
            {
                PlayerMotionController.current.selfDriven = true;
            }

            HUDManager.instance.HideAll();
            PauseMenuManager.instance.DisplayMenuPanel();

            m_currentGameState = Game.State.Paused;
        }

        public void Resume()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (freezeMovementWhenPaused)
            {
                PlayerMotionController.current.selfDriven = false;
            }

            HUDManager.instance.DisplayAll();
            PauseMenuManager.instance.HideMenuPanel();

            m_currentGameState = Game.State.Running;
        }

        public void Quit()
        {
            PlayerPrefs.SetString("SceneToLoad", "MainMenu");
            SceneManager.LoadScene("Preloader");
        }

        #endregion Other Methods

        #endregion Methods
    }
}