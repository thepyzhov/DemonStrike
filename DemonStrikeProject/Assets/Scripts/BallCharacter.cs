using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCharacter : MonoBehaviour {
	public string characterName;
	public int maxHealth = 100;
	public int damage = 10;

	private int currentHealth;
	private TurnBasedSystem tbs;

	private void Awake() {
		currentHealth = maxHealth;
	}

	private void Start() {
		tbs = FindObjectOfType<TurnBasedSystem>();
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
		if (currentHealth <= 0) {
			currentHealth = 0;
			Die();
		}
	}

	private void Die() {
		Destroy(gameObject, 0.1f);
		tbs.CharacterDied(GetComponent<BallAttack>());
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		// When objects have the same tags
		if (collision.gameObject.CompareTag(tag)) {
			return;
		}

		if (!GetComponent<BallAttack>().CanMove()) {
			return;
		}

		BallCharacter character = collision.gameObject.GetComponent<BallCharacter>();

		if (character != null) {
			//collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
			character.TakeDamage(damage);
		}
	}
}
