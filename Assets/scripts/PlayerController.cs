using UnityEngine;
using System.Collections;

public class PlayerController : MoveableController
{
    protected override bool InputGoLeft()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }
    protected override bool InputGoRight()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }
    protected override bool InputJump()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
