using UnityEngine;

public class PCInputSystem : IInputSystem
{
    public float MovingHorizontal() => Input.GetAxis("Horizontal");

    public bool IsPressJump() => Input.GetButtonDown("Jump");
    
    public bool IsPressPunch() => Input.GetKeyDown(KeyCode.E);

    public bool IsPressShot() => Input.GetKeyDown(KeyCode.Q);
    
    public bool IsLaunchEscMenu() => Input.GetKeyDown(KeyCode.Escape);
}
