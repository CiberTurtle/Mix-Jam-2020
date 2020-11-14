#pragma warning disable 649
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ciber_Turtle.UI;

namespace Game.Dating
{
	public class DatingManager : MonoBehaviour
	{
		// Singleton
		static DatingManager m_current;
		public static DatingManager current { get => m_current; }

		// Perams
		public SOWeapon weapon;
		public List<SOCard> hand;
		[Header("UI")]
		[SerializeField] TMP_Text textOpinion;
		[SerializeField] UIProgressBar barIntrest;
		[Space]
		[SerializeField] Image imgWeapon;
		[SerializeField] Transform tHandHolder;
		[Space]
		[SerializeField] GameObject pfCard;

		// Data
		WeaponBehaviour currentWeapon;

		void Awake()
		{
			m_current = this;

			currentWeapon = new WeaponBehaviour(weapon);

			RefreshWeapon();
			RefreshHand();
		}

		void Update()
		{
			if (currentWeapon.iOpinion > 0) textOpinion.text = "+" + currentWeapon.iOpinion.ToString();
			else textOpinion.text = currentWeapon.iOpinion.ToString();

			barIntrest.maxValue = currentWeapon.weapon.iIntrestToWin;
			barIntrest.value = currentWeapon.iIntrest;
		}

		public void RefreshWeapon()
		{
			imgWeapon.sprite = weapon.sprite;
		}

		public void RefreshHand()
		{
			foreach (SOCard card in hand)
			{
				Transform spawnedCard = Instantiate(pfCard, tHandHolder).transform;

				spawnedCard.GetChild(1).GetChild(0).GetComponent<Image>().sprite = card.sprite;
				spawnedCard.GetChild(2).GetComponent<TMP_Text>().text = card.name;
				spawnedCard.GetChild(3).GetComponent<TMP_Text>().text = card.sDescription;
			}
		}

		public void UseCard(int index)
		{
			SOCard.Action action = hand[index].Use();

			switch (action.opIntrestOperator)
			{
				case Operator.Set:
					currentWeapon.iIntrest = action.iIntrestFactor;
					break;
				case Operator.Add:
					currentWeapon.iIntrest += action.iIntrestFactor;
					break;
				case Operator.Subtract:
					currentWeapon.iIntrest -= action.iIntrestFactor;
					break;
				case Operator.Multiply:
					currentWeapon.iIntrest *= action.iIntrestFactor;
					break;
				case Operator.Divide:
					if (action.iIntrestFactor > 0) currentWeapon.iIntrest = Mathf.RoundToInt(currentWeapon.iIntrest / action.iIntrestFactor);
					break;
			}

			switch (action.opOpinionOperator)
			{
				case Operator.Set:
					currentWeapon.iOpinion = action.iOpinionFactor;
					break;
				case Operator.Add:
					currentWeapon.iOpinion += action.iOpinionFactor;
					break;
				case Operator.Subtract:
					currentWeapon.iOpinion -= action.iOpinionFactor;
					break;
				case Operator.Multiply:
					currentWeapon.iOpinion *= action.iOpinionFactor;
					break;
				case Operator.Divide:
					if (action.iOpinionFactor > 0) currentWeapon.iOpinion = Mathf.RoundToInt(currentWeapon.iOpinion / action.iIntrestFactor);
					break;
			}

			if (action.bApplyOpinion) ApplyOpinion();

			currentWeapon.iIntrest = Mathf.Clamp(currentWeapon.iIntrest, 0, currentWeapon.weapon.iIntrestToWin);
		}

		public void ApplyOpinion()
		{
			currentWeapon.iIntrest += currentWeapon.iOpinion;
			currentWeapon.iOpinion = 0;
		}
	}
}