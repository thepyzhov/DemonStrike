using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { MOVE, STAND, DEFENCE };

public class BallController : MonoBehaviour
{
	public const float MaxForce = 1000f;
	public float movementTime = 2f;

	private State state = State.STAND;

	private Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		CanMove(false);
	}

	public void Launch(float force) {
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPosition.z = 0f;

		Vector2 dir = (worldPosition - transform.position).normalized * -1f;
		rb.AddForce(dir * force);

		StartCoroutine(StopMovement());
	}

	public void CanMove(bool canMove) {
		rb.isKinematic = !canMove;
	}

	public bool CanMove() {
		return !rb.isKinematic;
	}

	public bool TurnEnded() {
		return !CanMove();
	}

	private IEnumerator StopMovement() {

		yield return new WaitForSeconds(movementTime);

		if (rb.velocity.magnitude >= 1f) {
			rb.velocity = Vector2.zero;
			CanMove(false);
		}
	}
}
