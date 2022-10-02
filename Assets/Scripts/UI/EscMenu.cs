using UnityEngine;
using UnityEngine.UI;

public class EscMenu : Menu
{
    [SerializeField] private Button _exitGameButton;

    protected override void Start()
    {
        _exitGameButton.onClick.RemoveAllListeners();
        _exitGameButton.onClick.AddListener(Application.Quit);

        base.Start();
    }
}
