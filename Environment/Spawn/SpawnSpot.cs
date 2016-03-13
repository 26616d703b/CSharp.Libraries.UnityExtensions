using UnityEngine;
using UnityExtensions.Characters;

namespace UnityExtensions.Environment.Spawn
{
    public class SpawnSpot : MonoBehaviour
    {
        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public float timeBetweenSpawn
        {
            get { return m_timeBetweenSpawn; }
            set { m_timeBetweenSpawn = Mathf.Clamp(value, 0f, 1000f); }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods

        #region Attributes

        public bool autoSpawn;

        [SerializeField]
        [Range(0f, 1000f)]
        private float m_timeBetweenSpawn = 10f;

        public Character.Type characterType;
        public Character.Team team = Character.Team.All;

        #endregion Attributes
    }
}