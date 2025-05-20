using UnityEngine;
using Utils;

namespace Popups
{
    public class LobbyPopup : Popup
    {
        [Header("Select character button")]
        [SerializeField]
        private AnimatedButton selectButton;
    
        [Header("Exit button")]
        [SerializeField]
        private AnimatedButton exitButton;
    
        private void ClickSelectCharacter()
        {
            _popupManager.ShowPopupByType(PopupType.SelectCharacter);
        }
    
        private void ClickExit()
        {
            Debug.Log("Exit game");
            Application.Quit();
        }

        public override void Show()
        {
            base.Show();
            selectButton.onClick += ClickSelectCharacter;
            exitButton.onClick += ClickExit;
        }
    
        public override void Hide()
        {
            selectButton.onClick -= ClickSelectCharacter;
            exitButton.onClick -= ClickExit;
            base.Hide();
        }
    }
}