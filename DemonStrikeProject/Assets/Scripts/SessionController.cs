using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    public BallController[] characters;

    private int currentUnitIndex = 0;

    private float holdDownStartTime;

	private void Start() {
        characters[currentUnitIndex].CanMove(true);
	}

	void Update() {
        if (Input.GetMouseButtonDown(0)) {
            holdDownStartTime = Time.time;
		}

        if (Input.GetMouseButton(0)) {
            float holdDownTime = Time.time - holdDownStartTime;
            
        }

        if (Input.GetMouseButtonUp(0)) {
            float holdDownTime = Time.time - holdDownStartTime;
            characters[currentUnitIndex].Launch(CalculateHoldDownForce(holdDownTime));
        }

        if (characters[currentUnitIndex].TurnEnded()) {
            NextUnit();
		}
    }

    void NextUnit() {
        currentUnitIndex += 1;
        if (currentUnitIndex >= characters.Length) {
            currentUnitIndex = 0;
		}

        characters[currentUnitIndex].CanMove(true);
	}

	private float CalculateHoldDownForce(float holdTime) {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = holdTimeNormalized * BallController.MaxForce;

        return force;
	}
}
