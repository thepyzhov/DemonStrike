using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour {
	public float power = 10f;
	public float maxLength = 3f;

	private Rigidbody2D rb;
	private Camera cam;
	private Vector2 force;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private TrajectoryLine tl;

	//private GradientColorKey[] colorKey;
	//private GradientAlphaKey[] alphaKey;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		tl = GetComponent<TrajectoryLine>();

		SetUpGradient();
	}

	private void Update() {
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
		}
	}

	private void SetUpGradient() {
		//trajectoryGradient = new Gradient();

		//// Populate the color keys at the relative time 0 and 1 (0 and 100%)
		//colorKey = new GradientColorKey[2];
		//colorKey[0].color = Color.red;
		//colorKey[0].time = 0.0f;
		//colorKey[1].color = Color.blue;
		//colorKey[1].time = 1.0f;

		//// Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
		//alphaKey = new GradientAlphaKey[2];
		//alphaKey[0].alpha = 1.0f;
		//alphaKey[0].time = 0.0f;
		//alphaKey[1].alpha = 0.0f;
		//alphaKey[1].time = 1.0f;

		//gradient.SetKeys(colorKey, alphaKey);
	}
}
 