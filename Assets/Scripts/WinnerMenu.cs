using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerMenu : MonoBehaviour
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
    [SerializeField] private Button _restartGame;
    
    private int _remainingEnemies = 0;
    
    private DateTime _startTime;
    private void Start()
    {
        SetActivePanel(false);
        _startTime = DateTime.Now;
        
        _restartGame.onClick.RemoveAllListeners();
        _restartGame.onClick.AddListener(RestartGame);
    }

    private void OpenWinnerMenu()
    {
        var difference = (DateTime.Now - _startTime).Seconds;
        
        SetActivePanel(true);
        _endedTimer.text = $"{difference / 60} m {difference % 60} s";
    }

    public void UpdateQuantityRemainingEnemies(int changes)
    {
        _remainingEnemies += changes;
        
        if(_remainingEnemies == 0)
            OpenWinnerMenu();
    }

    private void SetActivePanel(bool isActive)
    {
        transform.GetChild(0).gameObject.SetActive(isActive);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
