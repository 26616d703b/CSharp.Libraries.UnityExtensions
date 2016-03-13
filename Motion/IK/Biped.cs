using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExtensions.Motion.IK
{
    public class Biped
    {
        #region Properties

        public static class Bone
        {
            public enum Side
            {
                Center,
                Left,
                Right
            }

            public enum Type
            {
                Arm,
                Eye,
                Head,
                Leg,
                Spine
            }

            [Serializable]
            public class Map
            {
                // Attributes

                public Transform hips;

                public Transform leftUpperArm, leftLowerArm, leftHand;

                public Transform leftUpperLeg, leftLowerLeg, leftFoot;

                public Transform neck, head;
                public Transform rightUpperArm, rightLowerArm, rightHand;
                public Transform rightUpperLeg, rightLowerLeg, rightFoot;

                public Transform spine, chest;

                // Methods

                public bool IsValid()
                {
                    if (hips == null)
                        return false;

                    if (leftLowerLeg == null || leftLowerLeg == null || leftFoot == null)
                        return false;

                    if (rightUpperLeg == null || rightLowerLeg == null || rightFoot == null)
                        return false;

                    if (spine == null || chest == null)
                        return false;

                    if (neck == null || head == null)
                        return false;

                    if (leftUpperArm == null || leftLowerArm == null || leftHand == null)
                        return false;

                    if (rightUpperArm == null || rightLowerArm == null || rightHand == null)
                        return false;

                    return true;
                }

                public void Assign(Animator animator)
                {
                    if (animator == null)
                        throw new ArgumentNullException();

                    if (animator.isHuman)
                    {
                        hips = animator.GetBoneTransform(HumanBodyBones.Hips);

                        leftUpperLeg = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
                        leftLowerLeg = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
                        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);

                        rightUpperLeg = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
                        rightLowerLeg = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
                        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);

                        spine = animator.GetBoneTransform(HumanBodyBones.Spine);
                        chest = animator.GetBoneTransform(HumanBodyBones.Chest);

                        neck = animator.GetBoneTransform(HumanBodyBones.Neck);
                        head = animator.GetBoneTransform(HumanBodyBones.Head);

                        leftUpperArm = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
                        leftLowerArm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                        leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);

                        rightUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
                        rightLowerArm = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
                        rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
                    }
                }
            }
        }

        public enum Type
        {
            Humanoid
        }

        #endregion Properties

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

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
    }

    public static class BipedBoneExtensions
    {
        // SIDE

        public static List<Transform> Get(this Biped.Bone.Side boneSide, List<Transform> bones)
        {
            if (bones == null)
                throw new ArgumentException();

            var bonesOfSide = new List<Transform>();

            foreach (var bone in bones)
            {
                foreach (string identifier in boneSide.GetIdentifiers())
                {
                    if (bone.name.Contains(identifier) || bone.name.Contains(identifier.ToLower()))
                    {
                        bonesOfSide.Add(bone);
                    }
                }
            }

            return bonesOfSide;
        }

        public static List<string> GetIdentifiers(this Biped.Bone.Side boneSide)
        {
            var identifiers = new List<string>();

            switch (boneSide)
            {
                case Biped.Bone.Side.Center:
                    throw new Exception();

                case Biped.Bone.Side.Left:

                    identifiers.Add("-L-");
                    identifiers.Add("_L_");
                    identifiers.Add("L");
                    identifiers.Add("Left");

                    break;

                case Biped.Bone.Side.Right:

                    identifiers.Add("-R-");
                    identifiers.Add("_R_");
                    identifiers.Add("R");
                    identifiers.Add("Right");

                    break;

                default:
                    throw new NotImplementedException();
            }

            return identifiers;
        }

        // TYPE

        public static List<Transform> Get(this Biped.Bone.Type boneType, List<Transform> bones)
        {
            var bonesOfType = new List<Transform>();

            foreach (var bone in bones)
            {
                foreach (string identifier in boneType.GetIdentifiers())
                {
                    if (bone.name.Contains(identifier) || bone.name.Contains(identifier.ToLower()))
                    {
                        bonesOfType.Add(bone);
                    }
                }
            }

            return bonesOfType;
        }

        public static List<string> GetIdentifiers(this Biped.Bone.Type boneType)
        {
            var identifiers = new List<string>();

            switch (boneType)
            {
                case Biped.Bone.Type.Arm:

                    identifiers.Add("Arm");
                    identifiers.Add("Elbow");
                    identifiers.Add("Hand");
                    identifiers.Add("Palm");
                    identifiers.Add("Wrist");

                    break;

                case Biped.Bone.Type.Eye:

                    identifiers.Add("Eye");

                    break;

                case Biped.Bone.Type.Head:

                    identifiers.Add("Head");

                    break;

                case Biped.Bone.Type.Leg:

                    identifiers.Add("Ankle");
                    identifiers.Add("Calf");
                    identifiers.Add("Femur");
                    identifiers.Add("Foot");
                    identifiers.Add("Hip");
                    identifiers.Add("Knee");
                    identifiers.Add("Leg");
                    identifiers.Add("Thigh");

                    break;

                case Biped.Bone.Type.Spine:

                    identifiers.Add("Body");
                    identifiers.Add("Chest");
                    identifiers.Add("Hips");
                    identifiers.Add("Neck");
                    identifiers.Add("Pelvis");
                    identifiers.Add("Root");
                    identifiers.Add("Spine");
                    identifiers.Add("Torso");

                    break;

                default:
                    throw new NotImplementedException();
            }

            return identifiers;
        }
    }
}