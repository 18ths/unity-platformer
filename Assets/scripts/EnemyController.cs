using UnityEngine;
using System.Collections;

public class EnemyController : MoveableController {

    [ShowOnly] public bool actionLeft;
    [ShowOnly] public bool actionRight;
    private bool actionJump;

    private bool isMovingLeft = false;

    [ShowOnly] public float patrolToLeft;
    [ShowOnly] public float patrolToRight;

    private bool isDecided = false;

    void Update()
    {
        if (currentGround != null && !isDecided)
        {
            float enemyWidth = gameObject.GetComponent<BoxCollider2D>().size.x / 2;

            patrolToLeft = currentGround.gameObject.GetComponent<Platform>().tileLeftXPos + enemyWidth;
            patrolToRight = currentGround.gameObject.GetComponent<Platform>().tileRightXPos - enemyWidth;

            //return random initial value
            isMovingLeft = (Random.value > 0.5f);
            isDecided = true;
        }

        if (isDecided)
        {
            if (isMovingLeft)
            {
                actionLeft = true;
                actionRight = false;

                //do we want to change the direction?
                if (transform.position.x <= patrolToLeft)
                    isMovingLeft = false;
            }
            else
            {
                actionLeft = false;
                actionRight = true;

                //do we want to change the direction?
                if (transform.position.x >= patrolToRight)
                    isMovingLeft = true;
            }
        }     
    }

    protected override bool InputGoLeft()
    {
        return actionLeft;
    }
    protected override bool InputGoRight()
    {
        return actionRight;
    }
    protected override bool InputJump()
    {
        return false;
    }
}
