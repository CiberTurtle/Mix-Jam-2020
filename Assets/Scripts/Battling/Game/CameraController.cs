#pragma warning disable 649
using UnityEngine;
using DG.Tweening;

namespace Game.Battling
{
	public class CameraController : MonoBehaviour
	{
		// Singletons
		static CameraController m_current;
		public static CameraController current { get => m_current; }

		// Parameters
		public float fMidPoint;
		public float fSmoothing;
		public float fMaxDist;

		// Public Data
		Camera cam;

		void Ready()
		{
			m_current = this;

			cam = Camera.main;
		}

		void LateUpdate()
		{
			if (!BattlingManager.current.player) return;

			Vector2 v2PlayerPos = BattlingManager.current.player.transform.position;
			Vector2 v2MousePos = BattlingManager.current.v2MousePos;

			Vector2 v2TargetPos = v2PlayerPos + Vector2.ClampMagnitude((v2MousePos - v2PlayerPos) * fMidPoint, fMaxDist);

			transform.position = Vector2.Lerp(transform.position, v2TargetPos, Time.deltaTime * 10 * fSmoothing);

			BattlingManager.current.GetMousePos();
		}

		public void Shake(float fTime, float fStrength)
		{
			cam.transform.DOShakePosition(fTime, fStrength);
		}
	}
}