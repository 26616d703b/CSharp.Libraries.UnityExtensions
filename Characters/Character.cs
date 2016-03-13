using System;
using UnityEngine;

namespace UnityExtensions.Characters
{
    public static class Character
    {
        public enum Team
        {
            None = 0,

            All,

            Alpha,
            Beta
            /*Gamma,
            Delta*/
        }

        public enum Type
        {
            NPC,
            Player
        }

        public static class Attribute
        {
            public enum Gender
            {
                Female,
                Male
            }

            public static class Avatar
            {
                public enum Name
                {
                    // Females
                    Alice,

                    Asma,
                    Natalie,
                    Susie,
                    Theresa,

                    // Males
                    Amir,

                    Andrew,
                    Angus,
                    Bob,
                    Louis,
                    Rick,
                    Roman,
                    Tom
                }
            }
        }

        [Serializable]
        public class Profile
        {
            #region Attributes

            public Team team;

            #endregion Attributes
        }
    }

    public static class CharacterExtensions
    {
        public static Character.Attribute.Gender Gender(this Character.Attribute.Avatar.Name avatarName)
        {
            switch (avatarName)
            {
                case Character.Attribute.Avatar.Name.Alice:
                case Character.Attribute.Avatar.Name.Asma:
                case Character.Attribute.Avatar.Name.Natalie:
                case Character.Attribute.Avatar.Name.Susie:
                case Character.Attribute.Avatar.Name.Theresa:
                    return Character.Attribute.Gender.Female;

                case Character.Attribute.Avatar.Name.Amir:
                case Character.Attribute.Avatar.Name.Andrew:
                case Character.Attribute.Avatar.Name.Angus:
                case Character.Attribute.Avatar.Name.Bob:
                case Character.Attribute.Avatar.Name.Louis:
                case Character.Attribute.Avatar.Name.Rick:
                case Character.Attribute.Avatar.Name.Roman:
                case Character.Attribute.Avatar.Name.Tom:
                    return Character.Attribute.Gender.Male;

                default:
                    throw new NotImplementedException();
            }
        }

        public static Color Color(this Character.Team team)
        {
            switch (team)
            {
                case Character.Team.All:
                    return UnityEngine.Color.black;

                case Character.Team.Alpha:
                    return UnityEngine.Color.red;

                case Character.Team.Beta:
                    return UnityEngine.Color.blue;

                default:
                    break;
            }

            return UnityEngine.Color.white;
        }
    }
}