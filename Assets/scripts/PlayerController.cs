using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float jumpHeight;
	
	public bool hasJumped = false;

	public int collideIgnoreTime;

	private ArrayList collisions = new ArrayList();

	private class TimedCollision
	{
		public Collider2D coll1;
		public Collider2D coll2;
		public float timeLeft;
	}

	// Use this for initialization
	void Start() {
	
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		float diffY = Mathf.Abs (collision.contacts[0].otherCollider.attachedRigidbody.position.y -
		                         collision.contacts[0].collider.attachedRigidbody.position.y);

		if (diffY < 0.1)
		{
			TimedCollision timedCollision = new TimedCollision ();
			timedCollision.coll1 = collision.contacts[0].otherCollider;
			timedCollision.coll2 = collision.contacts[0].collider;
			timedCollision.timeLeft = collideIgnoreTime;
			collisions.Add(timedCollision);

			Physics2D.IgnoreCollision(collision.contacts[0].collider, collision.contacts[0].otherCollider, true);
		}

		hasJumped = false;
	}

	void FixedUpdate()
	{
		for (int i = 0; i < collisions.Count; ++i)
		{
			TimedCollision tc = collisions[i] as TimedCollision;
			tc.timeLeft -= 33; //TODO: use delta time

			if (tc.timeLeft <= 0)
			{
				Physics2D.IgnoreCollision(tc.coll1, tc.coll2, false);

				collisions.RemoveAt(i);
			}
		}
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementSpeed, 0), ForceMode2D.Force);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(movementSpeed, 0), ForceMode2D.Force);
		}

		if (Input.GetKey(KeyCode.Space) && !hasJumped)
		{
			hasJumped = true;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
		}
	}
}
