#pragma warning disable 649
using UnityEngine;

namespace Game.Battling
{
	public class Player : MonoBehaviour
	{
		[SerializeField] Transform tRotator;
		[SerializeField] Transform tHolder;

		public void Aim(Vector2 v2Pos)
		{
			tRotator.rotation = Util.PointToRot(v2Pos, transform.position);
		}
	}
}