#pragma warning disable 649
using UnityEngine;

public abstract class ProBehaviour : MonoBehaviour
{
	// Data
	[HideInInspector] public DamageData damageData;

	// Cache
	Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		rb.MovePosition(rb.position + (Vector2)transform.up * damageData.fSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<IDamagable>().OnTakeDamage(damageData.fDamage);
		}
	}
}
