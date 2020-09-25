using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCharacter : MonoBehaviour {
	public string characterName;
	public int maxHealth = 100;
	public int damage = 10;

	private int currentHealth;
	private TurnBasedSystem tbs;

	[Header("Unity Stuff")]
	public Image healthBar;
	public DamageIcon damageIcon;

	private void Awake() {
		currentHealth = maxHealth;
	}

	private void Start() {
		tbs = FindObjectOfType<TurnBasedSystem>();
	}

	public void TakeDamage(int damage) {
		currentHealth = (currentHealth - damage) <= 0 ? 0 : (currentHealth - damage);
		healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
		if (currentHealth <= 0) {
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
			Vector3 contactPoint = new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 0f);
			DamageIcon icon = Instantiate(damageIcon, contactPoint, Quaternion.identity);
			icon.SetDamageText(damage);
			//collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
			character.TakeDamage(damage);
		}
	}
}
