using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] int damage = 10;

	void OnTriggerEnter (Collider col) {
		Component damageableComponent = col.gameObject.GetComponent(typeof(IDamageable));
		if (damageableComponent) {
			(damageableComponent as IDamageable).TakeDamage(damage);

		}
	}
}
