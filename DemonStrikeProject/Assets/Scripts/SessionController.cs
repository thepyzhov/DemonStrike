using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    public BallController character;

    private float holdDownStartTime;

	void Update() {
        if (Input.GetMouseButtonDown(0)) {
            holdDownStartTime = Time.time;
		}

        if (Input.GetMouseButton(0)) {
            float holdDownTime = Time.time - holdDownStartTime;
            
        }

        if (Input.GetMouseButtonUp(0)) {
            float holdDownTime = Time.time - holdDownStartTime;
            character.Launch(CalculateHoldDownForce(holdDownTime));
        }
    }

	private float CalculateHoldDownForce(float holdTime) {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = holdTimeNormalized * BallController.MaxForce;

        return force;
	}
}
