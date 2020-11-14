#pragma warning disable 649
using UnityEngine;

namespace Game.Battling
{
	public class PlayerMovement : MonoBehaviour
	{
		// Perams
		public float fMoveAcc;

		// Data
		InputMain controls;
		Vector2 v2MoveInput;

		// Cache
		Rigidbody2D rb;

		void Awake()
		{
			rb = GetComponent<Rigidbody2D>();

			controls = new InputMain();
			controls.Player.Move.performed += (x) => v2MoveInput = x.ReadValue<Vector2>();
		}

		void FixedUpdate()
		{
			rb.MovePosition(rb.position + v2MoveInput * fMoveAcc * Time.fixedDeltaTime);
		}

		void OnEnable()
		{
			controls.Enable();
		}

		void OnDisable()
		{
			controls.Disable();
		}
	}
}