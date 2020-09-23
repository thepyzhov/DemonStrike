using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour {
	public float power = 10f;
	public float maxLength = 3f;
	public float movementTime = 3f;

	private Rigidbody2D rb;
	private Camera cam;
	private Vector2 force;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private TrajectoryLine tl;

	private TurnBasedSystem tbs;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		tl = GetComponent<TrajectoryLine>();
		tbs = FindObjectOfType<TurnBasedSystem>();
	}

	private void Update() {
		if (!CanMove()) {
			return;
		}

		if (Input.GetMouseButtonDown(0)) {
			startPoint = Vector3.zero;
			startPoint.z = 0;
		}

		if (Input.GetMouseButton(0)) {
			Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			currentPoint.z = 0;
			Vector3 clampedCurrentPoint = Vector3.ClampMagnitude(currentPoint, maxLength) * -1;

			tl.UpdateTrajectoryColor(clampedCurrentPoint.magnitude, maxLength);
			tl.RenderLine(startPoint, clampedCurrentPoint);
		}

		if (Input.GetMouseButtonUp(0)) {
			endPoint = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			endPoint.z = 0;
			Vector3 clampedEndPoint = Vector3.ClampMagnitude(endPoint, maxLength) * -1;

			force = new Vector2(clampedEndPoint.x, clampedEndPoint.y);
			rb.AddForce(force * power, ForceMode2D.Impulse);

			tl.EndLine();
			StartCoroutine(EndMovement());
		}
	}

	private IEnumerator EndMovement() {
		yield return new WaitForSeconds(movementTime);
		tbs.EndTurn();
	}

	public void SetIsKinematicTo(bool isKinematic) {
		rb.isKinematic = isKinematic;
		rb.velocity = Vector2.zero;
	}

	public bool CanMove() {
		return !rb.isKinematic;
	}
}
 