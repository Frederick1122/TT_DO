using UnityEngine;
using UnityEngine.UI;

public class EscMenu : Menu
{
    [SerializeField] private Button _exitGameButton;

    private bool _panelStatus = true;
    
    private void Start()
    {
        _exitGameButton.onClick.RemoveAllListeners();
        _exitGameButton.onClick.AddListener(Application.Quit);

        PlayerInputSystem.Instance.LaunchEscMenuAction += ChangeActivePanel;

        ChangeActivePanel();
    }

    private void ChangeActivePanel()
    {
        _panelStatus = !_panelStatus;
        SetActivePanel(_panelStatus);
    }
}
