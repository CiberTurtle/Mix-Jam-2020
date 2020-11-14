#pragma warning disable 649
using UnityEngine;

namespace Game.Dating
{
	public class WeaponBehaviour
	{
		public SOWeapon weapon;

		public int iIntrest;
		public int iOpinion;

		public WeaponBehaviour(SOWeapon weapon)
		{
			this.weapon = weapon;

			iIntrest = Mathf.RoundToInt(weapon.mmStartingIntrest.randomValue);
		}
	}
}