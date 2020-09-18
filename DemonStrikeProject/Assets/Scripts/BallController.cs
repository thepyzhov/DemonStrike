using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	public const float MaxForce = 500f;
	public float movementTime = 2f;

	private Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		//if (rb.velocity.magnitude <= 1f) {
		//	rb.velocity = Vector2.zero;
		//}
	}

	public void Launch(float force) {
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPosition.z = 0f;

		Vector2 dir = (worldPosition - transform.position).normalized * -1f;
		rb.AddForce(dir * force);
		//rb.velocity = dir * force;

		StartCoroutine(StopMovement());
	}

	private IEnumerator StopMovement() {

		yield return new WaitForSeconds(movementTime);

		if (rb.velocity.magnitude >= 1f) {
			rb.velocity = Vector2.zero;
		}
	}
}
