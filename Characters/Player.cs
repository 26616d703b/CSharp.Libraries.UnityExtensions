using System;
using UnityEngine;
using UnityExtensions.UI.HUD.Markers.Minimap;
using UnityExtensions.UI.HUD.Markers.Waypoint;

namespace UnityExtensions.Characters
{
    public class Player : MonoBehaviour
    {
        #region Attributes

        public Profile profile;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        // Use this for initialization
        private void Start()
        {
            MinimapSystem.current.target = transform;
            WaypointSystem.current.target = transform;
        }

        // Update is called once per frame
        //void Update() { }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods

        #region Properties

        public static class Camera
        {
            public enum POV
            {
                FirstPerson,
                ThirdPerson
            }

            public static class Anchor
            {
                public static readonly Vector3 FirstPerson = new Vector3(0f, 0.08f, 0.15f);
                public static readonly Vector3 ThirdPerson = new Vector3(0.5f, 1.65f, -0.7f);
            }
        }

        public static class Interaction
        {
            public enum Reference
            {
                Crosshair, // The center of the screen
                Cursor // The mouse position
            }

            public enum Type
            {
                None,
                NotPossible,
                Regular,
                Cooperation,
                Rescue
            }
        }

        public enum Motion
        {
            Idle = 0,
            Idle01 = 11,
            Idle02 = 12,
            Idle03 = 13,
            Idle04 = 14,
            Idle05 = 15,
            Idle06 = 16,
            Idle07 = 17,

            WalkingForward = 21,
            RunningForward = 23,
            WalkingBackwards = 22,
            RunningBackwards = 24,
            WalkingStrafLeft = 25,
            RunningStrafLeft = 27,
            WalkingStrafRight = 26,
            RunningStrafRight = 28,

            // SOCIAL SKILLS

            // RightArm Layer
            Waving = 33,

            //
            Kneeling = 4
        }

        public static class Preferences
        {
            public static class Audio
            {
                public static class Sound
                {
                    public static readonly string Volume = "SoundVolume";
                }

                public static class Music
                {
                    public static readonly string Volume = "MusicVolume";
                }

                public static class Voice
                {
                    public static readonly string Volume = "VoiceVolume";
                }
            }

            public static class Authentication
            {
                public static readonly string Connected = "Connected";

                public static readonly string Username = "Username";
                public static readonly string Password = "Password";
                public static readonly string RememberMe = "RemeberMe";
            }

            public static class Avatar
            {
                public static string Name = "AvatarName";
            }

            public static class Display
            {
                public static readonly string DepthOfField = "DepthOfField";
                public static readonly string MotionBlur = "MotionBlur";
                public static readonly string Bloom = "Bloom";
            }

            public static class Localization
            {
                public static readonly string Locale = "Locale";
            }

            public static class Network
            {
                public static class Server
                {
                    public static readonly string Location = "ServerLocation";
                }
            }
        }

        [Serializable]
        public class Profile : Character.Profile
        {
            #region Attributes

            [Header("Network Informations")]
            public string localName;

            #endregion Attributes
        }

        #endregion Properties
    }

    public static class PlayerMotionExtensions
    {
        public static string GetFullHashPath(this Player.Motion motion)
        {
            switch (motion)
            {
                // IDLES

                case Player.Motion.Idle:
                    return "Base Layer.Idles.Idle";

                case Player.Motion.Idle01:
                    return "Base Layer.Idles.Idle01";

                case Player.Motion.Idle02:
                    return "Base Layer.Idles.Idle02";

                case Player.Motion.Idle03:
                    return "Base Layer.Idles.Idle03";

                case Player.Motion.Idle04:
                    return "Base Layer.Idles.Idle04";

                case Player.Motion.Idle05:
                    return "Base Layer.Idles.Idle05";

                case Player.Motion.Idle06:
                    return "Base Layer.Idles.Idle06";

                case Player.Motion.Idle07:
                    return "Base Layer.Idles.Idle07";

                // LOCOMOTION

                case Player.Motion.WalkingForward:
                    return "Base Layer.Walking Forward";

                case Player.Motion.WalkingBackwards:
                    return "Base Layer.Walking Backwards";

                case Player.Motion.RunningForward:
                    return "Base Layer.Running Forward";

                case Player.Motion.RunningBackwards:
                    return "Base Layer.Running Backwards";

                case Player.Motion.WalkingStrafLeft:
                    return "Base Layer.Walking Strafe Left";

                case Player.Motion.WalkingStrafRight:
                    return "Base Layer.Walking Strafe Right";

                case Player.Motion.RunningStrafLeft:
                    return "Base Layer.Running Strafe Left";

                case Player.Motion.RunningStrafRight:
                    return "Base Layer.Running Strafe Right";

                // SOCIAL SKILLS

                case Player.Motion.Waving:
                    return "Base Layer.Actions.Waving";

                case Player.Motion.Kneeling:
                    return "Base Layer.Actions.Kneeling";

                default:
                    throw new NotImplementedException();
            }
        }
    }
}