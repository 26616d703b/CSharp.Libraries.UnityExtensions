using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityExtensions.UI.FX
{
    public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        #region Attributes

        private Vector2 m_originalLocalPointerPosition;
        private Vector3 m_originalPanelLocalPosition;
        private RectTransform m_panelRectTransform;
        private RectTransform m_parentRectTransform;

        #endregion Attributes

        #region Methods

        private void Awake()
        {
            m_panelRectTransform = transform.parent as RectTransform;
            m_parentRectTransform = m_panelRectTransform.parent as RectTransform;
        }

        public void OnPointerDown(PointerEventData data)
        {
            m_originalPanelLocalPosition = m_panelRectTransform.localPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parentRectTransform, data.position,
                data.pressEventCamera, out m_originalLocalPointerPosition);
        }

        public void OnDrag(PointerEventData data)
        {
            if (m_panelRectTransform == null || m_parentRectTransform == null)
                return;

            Vector2 localPointerPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parentRectTransform, data.position,
                data.pressEventCamera, out localPointerPosition))
            {
                Vector3 offsetToOriginal = localPointerPosition - m_originalLocalPointerPosition;
                m_panelRectTransform.localPosition = m_originalPanelLocalPosition + offsetToOriginal;
            }

            ClampToWindow();
        }

        // Clamp panel to area of parent
        private void ClampToWindow()
        {
            var pos = m_panelRectTransform.localPosition;

            Vector3 minPosition = m_parentRectTransform.rect.min - m_panelRectTransform.rect.min;
            Vector3 maxPosition = m_parentRectTransform.rect.max - m_panelRectTransform.rect.max;

            pos.x = Mathf.Clamp(m_panelRectTransform.localPosition.x, minPosition.x, maxPosition.x);
            pos.y = Mathf.Clamp(m_panelRectTransform.localPosition.y, minPosition.y, maxPosition.y);

            m_panelRectTransform.localPosition = pos;
        }

        #endregion Methods
    }
}