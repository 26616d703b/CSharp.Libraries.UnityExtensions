using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityExtensions.UI.Components
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollList : MonoBehaviour
    {
        #region Attributes

        [SerializeField]
        private bool m_static;

        private RectTransform m_contentTransform;

        [Space(10)]
        [SerializeField]
        private RectTransform m_focusTransform;

        private List<GameObject> m_content;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public bool isStatic
        {
            get { return m_static; }
        }

        public GameObject value { get; private set; }

        public int count
        {
            get { return m_content.Count; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        private void Start()
        {
            m_content = new List<GameObject>();
            m_contentTransform = GetComponent<ScrollRect>().content;

            if (m_static)
            {
                /*foreach (Transform itemTransform in m_contentTransform)
                {
                    GameObject copy = Instantiate(itemTransform.gameObject);
                    m_content.Add(copy);
                }*/
            }
            else
            {
                Refresh();
            }

            //Refresh();
        }

        private void Update()
        {
            if (value != null)
            {
                FocusOn(value);
            }
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public void OnSelectionChanged(GameObject content)
        {
            value = content;

            FocusOn(content);
        }

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public void Add(GameObject content)
        {
            m_content.Add(content);
        }

        public void Clear()
        {
            m_content = new List<GameObject>();
            value = null;

            Purge();
        }

        private void Purge()
        {
            foreach (Transform listItemTransform in m_contentTransform)
            {
                Destroy(listItemTransform.gameObject);
            }
        }

        public void Refresh()
        {
            Purge();

            foreach (var content in m_content)
            {
                var contentTransform = Instantiate(content).transform;
                contentTransform.SetParent(m_contentTransform);

                var contentRectTransform = contentTransform.GetComponent<RectTransform>();
                contentRectTransform.localPosition = Vector3.zero;
                contentRectTransform.localRotation = Quaternion.identity;
                contentRectTransform.localScale = Vector3.one;
            }

            value = m_content.Count > 0 ? m_content[0] : value;
        }

        private void FocusOn(GameObject content)
        {
            var contentTransform = content.GetComponent<RectTransform>();
            m_focusTransform.position = contentTransform.position;
            m_focusTransform.rotation = contentTransform.rotation;
            m_focusTransform.sizeDelta = contentTransform.sizeDelta;
        }

        #endregion Other Methods

        #endregion Methods
    }
}