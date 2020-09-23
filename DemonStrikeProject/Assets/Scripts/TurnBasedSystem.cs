using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour {
	private static TurnBasedSystem _instance;
	public static TurnBasedSystem Instance {
		get {
			return _instance;
		}
	}


	//public BallAttack[] characters;
	public List<BallAttack> characters;
	private int unitIndex;

	private void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
			return;
		}

		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		ResetAllCharacters();
		unitIndex = 0;
		characters[unitIndex].SetIsKinematicTo(false);
	}

	private void NextUnitIndex() {
		unitIndex += 1;

		if (unitIndex >= characters.Count) {
			unitIndex = 0;
		}

		characters[unitIndex].SetIsKinematicTo(false);
	}

	public void EndTurn() {
		characters[unitIndex].SetIsKinematicTo(true);
		NextUnitIndex();
	}

	public void CharacterDied(BallAttack character) {
		characters.Remove(character);
	}

	private void ResetAllCharacters() {
		foreach (BallAttack character in characters) {
			character.SetIsKinematicTo(true);
		}
	}
}
