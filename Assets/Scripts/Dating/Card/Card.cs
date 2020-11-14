#pragma warning disable 649
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Game.Dating
{
	public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
	{
		// Perams
		[SerializeField] float fMoveUpAmount;
		[SerializeField] float fScaleAmount;

		// Data
		float fStartingY;

		// Cache
		RectTransform rect;

		void Awake()
		{
			rect = GetComponent<RectTransform>();

			fStartingY = rect.localPosition.y;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			rect.DOLocalMoveY(fStartingY + fMoveUpAmount, 0.25f);
			rect.DOScale(fScaleAmount, 0.25f);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			rect.DOLocalMoveY(fStartingY, 0.25f);
			rect.DOScale(1, 0.25f);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			DatingManager.current.UseCard(transform.GetSiblingIndex());
		}
	}
}