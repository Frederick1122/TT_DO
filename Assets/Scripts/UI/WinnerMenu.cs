using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerMenu : Menu
{
    #region Singleton
    public static WinnerMenu Instance
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

    [SerializeField] private TMP_Text _endedTimer;
    [SerializeField] private Button _restartGameButton;
    
    private int _remainingEnemies = 0;
    
    private DateTime _startTime;
    
    private void Start()
    {
        SetActivePanel(false);
        _startTime = DateTime.Now;
        
        _restartGameButton.onClick.RemoveAllListeners();
        _restartGameButton.onClick.AddListener(RestartGame);
    }

    private void OpenWinnerMenu()
    {
        var difference = DateTime.Now - _startTime;
        _endedTimer.text = $"{difference.Minutes} m {difference.Seconds} s";
        
        SetActivePanel(true);
    }

    public void UpdateQuantityRemainingEnemies(int changes)
    {
        _remainingEnemies += changes;
        
        if(_remainingEnemies == 0)
            OpenWinnerMenu();
    }
    
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
