using Popups;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private PopupManager popupManager;

    private void Start()
    {
        popupManager.Init();
        popupManager.ShowPopupByType(PopupType.Lobby);
    }
}