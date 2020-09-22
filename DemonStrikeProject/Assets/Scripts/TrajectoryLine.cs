using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour {
	public Gradient trajectoryGradient;

	private LineRenderer lr;

	private void Awake() {
		lr = GetComponent<LineRenderer>();
		lr.sortingOrder = 15; 
	}

	public void RenderLine(Vector3 startPoint, Vector3 endPoint) {
		lr.positionCount = 2;
		Vector3[] points = new Vector3[2];
		points[0] = startPoint;
		points[1] = endPoint;

		lr.SetPositions(points);
	}

	public void EndLine() {
		lr.positionCount = 0;
	}

	public void UpdateTrajectoryColor(float length, float maxLength) {
		float newColor = length / maxLength;

		Gradient gradient = new Gradient();
		GradientColorKey[] colorKey = new GradientColorKey[1];
		GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];

		colorKey[0].color = trajectoryGradient.Evaluate(newColor);
		colorKey[0].time = 0.0f;
		alphaKey[0].alpha = 1f;
		alphaKey[0].time = 1f;

		gradient.SetKeys(colorKey, alphaKey);
		lr.colorGradient = gradient;
	}
}
