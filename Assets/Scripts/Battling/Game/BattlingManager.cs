#pragma warning disable 649
using UnityEngine;

namespace Game.Battling
{
	public class BattlingManager : MonoBehaviour
	{
		// Singleton
		static BattlingManager m_current;
		public static BattlingManager current { get => m_current; }

		// Perams
		[SerializeField] GameObject pfPlayer;

		// Public Data
		[HideInInspector] public Player player;
		[HideInInspector] public Vector2 v2MousePos;

		// Data
		InputMain controls;
		Vector2 m_v2MousePos;

		void Awake()
		{
			m_current = this;

			player = Instantiate(pfPlayer).GetComponent<Player>();

			controls = new InputMain();
			controls.Player.Aim.performed += (x) => m_v2MousePos = x.ReadValue<Vector2>();
		}

		public void GetMousePos()
		{

		}
	}
}