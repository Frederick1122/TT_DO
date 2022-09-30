using UnityEngine;

public class PCInputSystem : IInputSystem
{
    public float MovingHorizontal() => Input.GetAxis("Horizontal");

    public bool IsPressJump() => Input.GetButtonUp("Jump");
}
