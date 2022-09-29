using UnityEngine;

public class PCInputSystem : IInputSystem
{
    public Vector2 UpdateMovement()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        return new Vector2(moveHorizontal, moveVertical);
    }
}
