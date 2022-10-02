using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    #endregion

    [SerializeField] private EscMenu _escMenu;
    [SerializeField] private WinnerMenu _winnerMenu;
    
    private int _remainingEnemies = 0;

    private void Start() => PlayerInputSystem.Instance.LaunchEscMenuAction += OpenEscMenu;

    public void UpdateQuantityRemainingEnemies(int changes)
    {
        _remainingEnemies += changes;

        if (_remainingEnemies != 0)
            return;
        
        if(_escMenu.GetMenuStatus())
            _escMenu.ChangeActiveMenu();
            
        _winnerMenu.OpenWinnerMenu();
    }

    public void OpenEscMenu()
    {
        if(_winnerMenu.GetMenuStatus()) return;
        
        _escMenu.ChangeActiveMenu();
    }
}
