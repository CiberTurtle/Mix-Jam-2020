#pragma warning disable 649
using UnityEngine;

public class GenericRangedWeapon : WeaponBehaviour
{
	[SerializeField] protected DamageData damageData;
	[Space]
	[SerializeField] int iNumberOfPros;
	[SerializeField, Min(0)] float fProVar;
	[Space]
	[SerializeField] Transform tShootPos;
	[SerializeField] GameObject pfProjectile;

	protected override void Fire()
	{
		for (int i = 0; i < iNumberOfPros; i++)
		{
			Instantiate(pfProjectile, tShootPos.position, Quaternion.Euler(0, 0, tShootPos.rotation.z + Random.Range(-fProVar / 2, fProVar / 2)));
		}
	}
}
