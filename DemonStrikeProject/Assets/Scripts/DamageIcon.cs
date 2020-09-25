using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIcon : MonoBehaviour {
	float timeDisappearance = 1f;

	private void Start() {
		Destroy(gameObject, timeDisappearance);
	}

	public void SetDamageText(int damage) {
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
	}
}
