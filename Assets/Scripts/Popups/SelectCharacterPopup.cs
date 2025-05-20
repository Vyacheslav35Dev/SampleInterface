using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Popups
{
    public class SelectCharacterPopup : Popup
    {
        [Header("Select character icon")]
        [SerializeField]
        private Image targetImage;
    
        [Header("Array avatars sprites")]
        [SerializeField]
        private Sprite[] sprites;
    
        [Header("Settings animation show avatar icon")]
        [SerializeField]
        private float transitionDuration = 0.5f;
    
        private int currentIndex = 0;
    
        [Header("Back button")]
        [SerializeField]
        private AnimatedButton backButton;
    
        [Header("Previous sprite button")]
        [SerializeField]
        private AnimatedButton selectLeftButton;
    
        [Header("Next sprite button")]
        [SerializeField]
        private AnimatedButton selectRightButton;
    
        private void NextSprite()
        {
            ChangeSprite((currentIndex + 1) % sprites.Length);
        }
    
        private void PreviousSprite()
        {
            int newIndex = currentIndex - 1;
            if (newIndex < 0)
                newIndex = sprites.Length - 1;
            ChangeSprite(newIndex);
        }

        private void ChangeSprite(int newIndex)
        {
            if (sprites.Length == 0 || targetImage == null || newIndex == currentIndex)
                return;
        
            targetImage.DOFade(0f, transitionDuration).OnComplete(() =>
            {
                currentIndex = newIndex;
                targetImage.sprite = sprites[currentIndex];
            
                targetImage.DOFade(1f, transitionDuration);
            });
        }
    
        private void BackLobby()
        {
            _popupManager.ShowPopupByType(PopupType.Lobby);
        }

        public override void Show()
        {
            base.Show();
            backButton.onClick += BackLobby;
            selectLeftButton.onClick += PreviousSprite;
            selectRightButton.onClick += NextSprite;
        }
    
        public override void Hide()
        {
            backButton.onClick -= BackLobby;
            selectLeftButton.onClick -= PreviousSprite;
            selectRightButton.onClick -= NextSprite;
            base.Hide();
        }    
    }
}