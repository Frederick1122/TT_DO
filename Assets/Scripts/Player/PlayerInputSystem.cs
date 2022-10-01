using System;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    #region Singleton
    public static PlayerInputSystem Instance
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
    
    [SerializeField] private ControllerType _controllerType;
    private IInputSystem _currentInputSystem;

    public Action LaunchEscMenuAction;
    
    private void Start() => UpdateCurrentInputSystem();

    public IInputSystem GetPIS() => _currentInputSystem;

    private void Update()
    {
        if(_currentInputSystem.IsLaunchEscMenu())
            LaunchEscMenuAction?.Invoke();
    }

    private void UpdateCurrentInputSystem()
    {
        _currentInputSystem = _controllerType switch
        {
            ControllerType.PC => new PCInputSystem(),
            ControllerType.Mobile => new PCInputSystem(),
            ControllerType.Xbox => new PCInputSystem()
        };
    }

}

public enum ControllerType
{
    PC,
    Mobile,
    Xbox
}

