using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public int maxHealth = 100;
    public int damage = 10;

    private int currentHealth;

	private void Awake() {
		currentHealth = maxHealth;
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
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!GetComponent<BallController>().CanMove()) {
			return;
		}

		if (CompareTag("Player") && collision.gameObject.CompareTag("Enemy") || CompareTag("Enemy") && collision.gameObject.CompareTag("Player")) {
			collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}
