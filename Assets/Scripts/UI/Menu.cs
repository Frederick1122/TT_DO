using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool _menuStatus = true;

    protected virtual void Start() => ChangeActiveMenu();

    public void ChangeActiveMenu()
    {
        _menuStatus = !_menuStatus;
        SetActivePanel(_menuStatus);
    }
    
    private void SetActivePanel(bool isActive) => transform.GetChild(0).gameObject.SetActive(isActive);

    public bool GetMenuStatus() => _menuStatus;
}
