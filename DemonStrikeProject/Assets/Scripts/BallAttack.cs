using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour {
	public float power = 10f;

	public Vector2 minPower;
	public Vector2 maxPower;

	private Rigidbody2D rb;
	private Camera cam;
	private Vector2 force;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private TrajectoryLine tl;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		tl = GetComponent<TrajectoryLine>();
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			//startPoint = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			startPoint = Vector3.zero;
			startPoint.z = 0;
		}

		if (Input.GetMouseButton(0)) {
			Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			currentPoint *= -1;
			currentPoint.z = 0;

			tl.RenderLine(startPoint, currentPoint);
		}

		if (Input.GetMouseButtonUp(0)) {
			endPoint = cam.ScreenToWorldPoint(Input.mousePosition) * -1;
			endPoint.z = 0;

			force = new Vector2(Mathf.Clamp(endPoint.x - startPoint.x, minPower.x, maxPower.x),
								Mathf.Clamp(endPoint.y - startPoint.y, minPower.y, maxPower.y));
			rb.AddForce(force, ForceMode2D.Impulse);

			tl.EndLine();
		}
	}
}
 