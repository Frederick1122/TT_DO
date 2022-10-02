using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerMenu : Menu
{
    [SerializeField] private TMP_Text _endedTimer;

    private DateTime _startTime;
    private bool _isPanelActive;

    protected override void Start()
    {
        _startTime = DateTime.Now;
        
        base.Start();
    }

    public void OpenWinnerMenu()
    {
        var difference = DateTime.Now - _startTime;
        _endedTimer.text = $"{difference.Minutes} m {difference.Seconds} s";
        
        ChangeActiveMenu();
    }
}
