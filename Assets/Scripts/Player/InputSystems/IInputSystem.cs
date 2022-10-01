using UnityEngine;

public interface IInputSystem
{
    public float MovingHorizontal();

    public bool IsPressJump();
    
    public bool IsPressPunch();
    
    public bool IsPressShot();

    public bool IsLaunchEscMenu();
}
