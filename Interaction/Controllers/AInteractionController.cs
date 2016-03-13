using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions.Characters;
using UnityExtensions.Runtime.Managers;

namespace UnityExtensions.Interaction.Controllers
{
    public abstract class AInteractionController : MonoBehaviour
    {
        #region Attributes

        [SerializeField]
        [Tooltip("Allow multiple interactions")]
        public bool multiple;

        public Color highlightColor = new Color(247f, 97f, 97f);

        [Range(3f, 10f)]
        [SerializeField]
        private readonly float m_highlightSpeed = 3f;

        //protected float m_minDistance = 0.5f;
        [Range(0f, 1000f)]
        [SerializeField]
        protected float m_maxDistance = 10f;

        protected List<GameObject> m_interactingWith = new List<GameObject>();

        private Material[] m_materials;
        private Color[] m_materialsBaseColors;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public bool canInteractWithOthers
        {
            get { return multiple || (!multiple && m_interactingWith.Count == 0); }
        }

        public bool interactingWithOthers
        {
            get { return m_interactingWith.Count > 0; }
        }

        public List<GameObject> interactingWith
        {
            get { return m_interactingWith; }
            set
            {
                m_interactingWith = value;

                if (!multiple && m_interactingWith.Count > 1)
                {
                    throw new Exception("Multiple interactions are not allowed on this object...");
                }
            }
        }

        public float maxDistance
        {
            get { return m_maxDistance; }
            set { m_maxDistance = Mathf.Clamp(value, 0f, 1000f); }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        protected void Start()
        {
            m_materials = GetComponent<Renderer>().materials;

            m_materialsBaseColors = new Color[m_materials.Length];

            for (var i = 0; i < m_materials.Length; i++)
                m_materialsBaseColors[i] = m_materials[i].color;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        private void OnMouseEnter()
        {
            if (!isActiveAndEnabled ||
                OfflineGameManager.instance.interactionReference != Player.Interaction.Reference.Cursor)
                return;

            if (Vector3.Distance(Camera.main.transform.position, transform.position) <= m_maxDistance)
                Highlight();
        }

        private void OnMouseExit()
        {
            if (!isActiveAndEnabled ||
                OfflineGameManager.instance.interactionReference != Player.Interaction.Reference.Cursor)
                return;

            StopHighlight();
        }

        //___________________________________________________________________________ Implemented

        public void Highlight()
        {
            StopCoroutine(ResetMaterialsColors());
            StartCoroutine(ChangeMaterialsColor());
        }

        public void StopHighlight()
        {
            StopCoroutine(ChangeMaterialsColor());
            StartCoroutine(ResetMaterialsColors());
        }

        protected void OnStartInteractWith(GameObject other)
        {
            m_interactingWith.Add(other);
        }

        protected void OnStopInteractWith(GameObject other)
        {
            m_interactingWith.Remove(other);
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        private IEnumerator ChangeMaterialsColor()
        {
            var done = 0f; // 1f represents 100%

            while (done < 1f)
            {
                foreach (var material in m_materials)
                {
                    material.color = Color.Lerp(material.color, highlightColor, done);

                    yield return null;

                    done += m_highlightSpeed * Time.deltaTime;
                }
            }
        }

        private IEnumerator ResetMaterialsColors()
        {
            var done = 0f;

            while (done < 1f)
            {
                for (var i = 0; i < m_materials.Length; i++)
                {
                    m_materials[i].color = Color.Lerp(m_materials[i].color, m_materialsBaseColors[i], done);

                    yield return null;

                    done += m_highlightSpeed * Time.deltaTime;
                }
            }
        }

        #endregion Other Methods

        #endregion Methods
    }
}