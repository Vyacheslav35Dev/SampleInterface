using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Popups
{
    public class PopupManager : MonoBehaviour
    {
        public List<Popup> popups;

        private Popup currentPopup;

        public void Init()
        {
            foreach (var popup in popups)
            {
                popup.Init(this);
            }
        }
    
        private void ShowPopup(Popup popup)
        {
            if (currentPopup != null && currentPopup != popup)
            {
                currentPopup.Hide();
            }
            currentPopup = popup;
            currentPopup.Show();
        }
    
        public void ShowPopupByIndex(int index)
        {
            if (index >= 0 && index < popups.Count)
            {
                ShowPopup(popups[index]);
            }
        }
    
        public void ShowPopupByType(PopupType type)
        {
            var popup = popups.FirstOrDefault(x => x.popupType == type);
            if (popup != null)
            {
                ShowPopup(popup);
            }
            else
            {
                Debug.LogError("Not found popup by type: " + type);
            }
        }
    
        public void HideCurrent()
        {
            if (currentPopup != null)
            {
                currentPopup.Hide();
                currentPopup = null;
            }
        }
    }
}