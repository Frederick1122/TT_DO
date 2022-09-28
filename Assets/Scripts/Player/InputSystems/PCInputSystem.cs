using UnityEngine;

public class PCInputSystem : IInputSystem
{
    public Vector3 UpdateMovement()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        return new Vector3(moveHorizontal, 0.0f, moveVertical);
    }
}
