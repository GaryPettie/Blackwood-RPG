using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

	[SerializeField] float maxHealth = 100;
	float currentHealth = 100;
	public float healthAsPercentage { get { return currentHealth / maxHealth; } }

	AICharacterControl aiCharacterControl;
	[SerializeField] float attackRadius = 10f;
	GameObject player;
	Transform startLocation;

	void Start () {
		aiCharacterControl = GetComponent<AICharacterControl>();
		player = GameObject.FindGameObjectWithTag("Player");
		startLocation = new GameObject("EnemyStart").GetComponent<Transform>();
		startLocation.position = transform.position;
	}

	void Update () {
		float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

		if (distanceToPlayer <= attackRadius) {
			aiCharacterControl.SetTarget(player.transform);
		}
		else {
			aiCharacterControl.SetTarget(startLocation);
		}
	}

	public void TakeDamage(float damage) {
		currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
	}
}
