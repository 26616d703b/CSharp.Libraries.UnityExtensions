using System;
using UnityEngine;
using UnityExtensions.Motion.Controllers;
using UnityExtensions.UI.HUD.Markers.Minimap;
using UnityExtensions.UI.HUD.Markers.Waypoint;

namespace UnityExtensions.Characters
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class Civilian : MonoBehaviour
    {
        #region Attributes

        public Profile profile;

        private MapItem m_mapItem;
        private Waypoint m_waypoint;

        private CivilianMotionController m_motionController;

        private Status.GlobalHealth m_healthStatus = Status.GlobalHealth.Healthy;

        #endregion Attributes

        #region Properties

        public static class Attribute
        {
            // ...
        }

        [Serializable]
        public class Profile : Character.Profile
        {
            #region Attributes

            public bool random = true;

            // ...

            #endregion Attributes
        }

        public enum Motion
        {
            Locomotion,

            Dying,
            GettingUp,

            Shouting
        }

        public static class Status
        {
            public enum GlobalHealth
            {
                Healthy,
                Sick
            }
        }

        #endregion Properties

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public Status.GlobalHealth healthStatus
        {
            get { return m_healthStatus; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Awake()
        {
            if (profile.random)
            {
                // ...
            }
        }

        // Use this for initialization
        private void Start()
        {
            // MOTION
            m_motionController = GetComponent<CivilianMotionController>();
            m_motionController.OnFellToTheGround += OnFellToTheGround;

            // MARKERS
            m_mapItem = GetComponentInChildren<MapItem>();
            // m_mapItem.type = MapItemProperty.Type.Dead;

            m_waypoint = GetComponentInChildren<Waypoint>();
        }

        private void OnDestroy()
        {
            m_motionController.OnHeartAttack -= OnFellToTheGround;
        }

        // Update is called once per frame
        //void Update() {  }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public void OnFellToTheGround()
        {
            if (!m_mapItem.isActiveAndEnabled && !m_waypoint.isActiveAndEnabled)
            {
                m_mapItem.enabled = true;
                m_waypoint.enabled = true;

                m_healthStatus = Status.GlobalHealth.Sick;

                GetComponent<NavMeshAgent>().enabled = false;
            }
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }

    public static class CivilianMotionExtensions
    {
        public static string GetFullPathHash(this Civilian.Motion motion)
        {
            switch (motion)
            {
                case Civilian.Motion.Locomotion:
                    return "Base Layer.Locomotion";

                case Civilian.Motion.Dying:
                    return "Base Layer.Dying";

                case Civilian.Motion.GettingUp:
                    return "Base Layer.Getting Up";

                case Civilian.Motion.Shouting:
                    return "Shout Layer.Shouting";

                default:
                    throw new NotImplementedException();
            }
        }
    }
}