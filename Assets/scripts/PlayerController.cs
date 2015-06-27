using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float jumpHeight;
	
	public bool isInJump = false;

	public float collideIgnoreTime;

	private ArrayList collisions = new ArrayList();

    private Collider2D currentGround = null;
    private bool isCurrentGroundSolid = false;

    private class IgnoreCollision
	{
		public Collider2D coll1;
		public Collider2D coll2;
		public float timeLeft;
	}

    private float epsilon = 0.1f;

    void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.collider.gameObject.tag == "platform")
        {
            isInJump = false;
            currentGround = collision.collider;
            isCurrentGroundSolid = currentGround.gameObject.GetComponent<Platform>().isSolid;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            var colliders = other.gameObject.GetComponents<EdgeCollider2D>();

            Debug.Assert(colliders.Length == 2, "There should be exactly 2 colliders, trigger and nontrigger!");

            foreach (EdgeCollider2D nonTriggerCollider in colliders)
            {
                if (!nonTriggerCollider.isTrigger)
                {
                    //we have found non-trigger collider
                    float topPos = (transform.position.y + GetComponent<BoxCollider2D>().size.y / 2);
                    float diffY = Mathf.Abs(nonTriggerCollider.attachedRigidbody.position.y - topPos);

                    if (diffY < epsilon)
                    {
                        //we're jumping from down
                        IgnoreCollision timedCollision = new IgnoreCollision();
                        timedCollision.coll1 = nonTriggerCollider;
                        timedCollision.coll2 = GetComponent<BoxCollider2D>();
                        timedCollision.timeLeft = collideIgnoreTime;
                        collisions.Add(timedCollision);

                        Physics2D.IgnoreCollision(nonTriggerCollider, GetComponent<BoxCollider2D>(), true);
                    }
                }
            }
        }
    }

	void FixedUpdate()
	{
        HandleInput();

        for (int i = 0; i < collisions.Count; ++i)
		{
			IgnoreCollision tc = collisions[i] as IgnoreCollision;
			tc.timeLeft -= Time.fixedDeltaTime;

			if (tc.timeLeft <= 0)
			{
				Physics2D.IgnoreCollision(tc.coll1, tc.coll2, false);

				collisions.RemoveAt(i);
			}
		}
	}

	void HandleInput()
	{
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementSpeed, 0), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(movementSpeed, 0), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.Space) && !isInJump && GetComponent<Rigidbody2D>().velocity.y < epsilon)
        {
            isInJump = true;

            if (Input.GetKey(KeyCode.DownArrow) && !isCurrentGroundSolid)
            {
                //fall from platform
                IgnoreCollision timedCollision = new IgnoreCollision();
                timedCollision.coll1 = currentGround;
                timedCollision.coll2 = GetComponent<BoxCollider2D>();
                timedCollision.timeLeft = collideIgnoreTime;
                collisions.Add(timedCollision);

                Physics2D.IgnoreCollision(currentGround, GetComponent<BoxCollider2D>(), true);
            }
            else
            {
                //regular jump
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }

            currentGround = null;
        }
    }
}
