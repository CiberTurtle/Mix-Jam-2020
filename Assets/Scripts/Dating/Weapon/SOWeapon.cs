#pragma warning disable 649
using UnityEngine;

namespace Game.Dating
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "Game/Dating/Weapon", order = 1)]
	public class SOWeapon : ScriptableObject
	{
		[NaughtyAttributes.ShowAssetPreview] public Sprite sprite;
		[Space]
		public int iIntrestToWin;
		[MinMaxSlider(0, 100)] public MinMax mmStartingIntrest;
	}
}