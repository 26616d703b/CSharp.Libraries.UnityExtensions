namespace UnityExtensions.Runtime.Managers
{
    public class OfflineGameManager : AGameManager
    {
        #region Attributes

        static OfflineGameManager()
        {
            instance = null;
        }

        #endregion Attributes

        #region Methods

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        public static OfflineGameManager instance { get; private set; }

        #endregion Inherited Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        private new void Awake()
        {
            instance = this;
        }

        #endregion Accessors and Mutators

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        protected override void OnLocaleChange()
        {
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}