using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    public BallController[] characters;

    private int currentUnitIndex = 0;

	private void Start() {
        characters[currentUnitIndex].CanMove(true);
	}

	void Update() {

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
}
