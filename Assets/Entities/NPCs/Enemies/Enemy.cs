using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] private float maxHealth = 100;

	private float currentHealth = 100;

	public float healthAsPercentage { get { return currentHealth / maxHealth; } }
}
