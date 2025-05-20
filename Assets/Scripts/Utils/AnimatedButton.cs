using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils
{
    public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public float clickScale = 0.9f;
        public float animationDuration = 0.1f;
        public Color normalColor = Color.white;
        public Color pressedColor = Color.gray;
        public bool enableHoverAnimation = true;

        [SerializeField]
        private Button button;
        private Vector3 originalScale;
        private Image buttonImage;

        public Action onClick;

        void Start()
        {
            originalScale = transform.localScale;

            buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = normalColor;
            }
        }

        private void OnButtonClick()
        {
            transform.DOKill();
            Sequence clickSequence = DOTween.Sequence();

            clickSequence.Append(transform.DOScale(originalScale * clickScale, animationDuration).SetEase(Ease.OutQuad))
                .Join(ChangeColor(pressedColor, animationDuration))
                .Append(transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutQuad))
                .Join(ChangeColor(normalColor, animationDuration));
            onClick?.Invoke();
        }

        private Tween ChangeColor(Color targetColor, float duration)
        {
            if (buttonImage != null)
            {
                return buttonImage.DOColor(targetColor, duration);
            }
            return null;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enableHoverAnimation && buttonImage != null)
            {
                buttonImage.DOColor(Color.yellow, 0.2f);
                transform.DOScale(originalScale * 1.05f, 0.2f).SetEase(Ease.OutQuad);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (enableHoverAnimation && buttonImage != null)
            {
                buttonImage.DOColor(normalColor, 0.2f);
                transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutQuad);
            }
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}