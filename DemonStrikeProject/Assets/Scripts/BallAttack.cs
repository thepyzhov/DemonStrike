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
			startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
			startPoint.z = 10;

			endPoint = transform.position;
			endPoint.z = 10;
		}

		if (Input.GetMouseButton(0)) {
			Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition) * -1;
			currentPoint.z = 10;

			tl.RenderLine(currentPoint, endPoint);
		}

		if (Input.GetMouseButtonUp(0)) {
			startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
			startPoint.z = 10;

			force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
								Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

			tl.EndLine();
		}
	}
}
 