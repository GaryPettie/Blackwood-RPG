using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

	[SerializeField] private float maxHealth = 100;
	[SerializeField]
	private float currentHealth = 100;
	public float healthAsPercentage { get { return currentHealth / maxHealth; } }


	public void TakeDamage(float damage) {
		currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
	}
}
