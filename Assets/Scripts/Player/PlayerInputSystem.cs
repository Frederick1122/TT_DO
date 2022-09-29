using System;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField] private ControllerType _controllerType;
    private IInputSystem _currentInputSystem;

    private void Start() => UpdateCurrentInputSystem();

    public Vector2 UpdateMovement() => _currentInputSystem.UpdateMovement();

    public void SetControllerType(ControllerType newControllerType)
    {
        _controllerType = newControllerType;
        UpdateCurrentInputSystem();
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

