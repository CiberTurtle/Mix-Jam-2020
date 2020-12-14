#pragma warning disable 649
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
	[SerializeField] protected float fTimeBtwShots;
	[Space]

	protected float fShootCooldown;

	protected virtual void Update()
	{
		fShootCooldown -= Time.deltaTime;
	}

	public virtual void Use()
	{
		if (fShootCooldown < 0)
		{
			fShootCooldown = fTimeBtwShots;
			Fire();
		}
	}

	protected abstract void Fire();
}
