using DG.Tweening;
using UnityEngine;

namespace Popups
{
    public enum PopupType
    {
        None,
        Lobby,
        SelectCharacter
    }

    public class Popup : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float animationDuration = 0.3f;

        public PopupType popupType = PopupType.None;
    
        private bool isOpen = false;
        
        public bool IsOpen() => isOpen;

        protected PopupManager _popupManager;

        private void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            
            canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }

        public void Init(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            canvasGroup.DOKill();
            
            transform.localScale = Vector3.zero;
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(1f, animationDuration).SetEase(Ease.OutBack))
                .Join(canvasGroup.DOFade(1f, animationDuration))
                .OnComplete(() => { isOpen = true; });
        }

        public virtual void Hide()
        {
            canvasGroup.DOKill();
            
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(0f, animationDuration).SetEase(Ease.InBack))
                .Join(canvasGroup.DOFade(0f, animationDuration))
                .OnComplete(() => 
                { 
                    gameObject.SetActive(false); 
                    isOpen = false; 
                });
        }
    }
}