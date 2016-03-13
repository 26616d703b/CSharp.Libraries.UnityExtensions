using UnityEngine;

namespace UnityExtensions.Environment.Spawn
{
    public class SpawnSystem : MonoBehaviour
    {
        #region Attributes

        public static SpawnSystem current { get; set; }

        [SerializeField]
        private SpawnSpot[] m_spawnSpots;

        private bool m_running;

        #endregion Attributes

        #region Methods

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void OnEnable()
        {
            current = this;
        }

        // Use this for initialization
        private void Start()
        {
        }

        #endregion Inherited Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public SpawnSpot[] spawnSpots
        {
            get { return m_spawnSpots; }
        }

        #endregion Accessors and Mutators

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}