#pragma warning disable 649
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ciber_Turtle.UI;
using DG.Tweening;

namespace Game.Dating
{
	public class DatingManager : MonoBehaviour
	{
		// Singleton
		static DatingManager m_current;
		public static DatingManager current { get => m_current; }

		// Perams
		public SOWeapon weapon;
		[Space]
		[SerializeField] int iMaxCards;
		[NaughtyAttributes.ReorderableList] public SOCard[] cards;
		[Header("UI")]
		[SerializeField] TMP_Text textOpinion;
		[SerializeField] UIProgressBar barIntrest;
		[Space]
		[SerializeField] Image imgWeapon;
		[SerializeField] Transform tHandHolder;
		[Space]
		[SerializeField] float fDioTime;
		[SerializeField] TMP_Text textPlayerDio;
		[SerializeField] TMP_Text textWeaponDio;
		[Space]
		[SerializeField] GameObject pfCard;

		// Data
		List<SOCard> hand;
		WeaponBehaviour currentWeapon;
		Color cOgDioColor;

		void Awake()
		{
			m_current = this;

			cOgDioColor = textPlayerDio.color;
			textPlayerDio.color = Color.clear;
			textWeaponDio.color = Color.clear;

			currentWeapon = new WeaponBehaviour(weapon);

			hand = new List<SOCard>();
			for (int i = 0; i < iMaxCards; i++)
			{
				hand.Add(Util.GetRandomItem(cards));
			}

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
			foreach (Transform child in tHandHolder)
			{
				Destroy(child.gameObject);
			}

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

			int iIntrestFactor = action.iIntrestFactor;

			switch (action.opmIntrestOperatorMode)
			{
				case OperatorMode.Abs:
					iIntrestFactor = Mathf.Abs(iIntrestFactor);
					break;
				case OperatorMode.Neg_Abs:
					iIntrestFactor = -Mathf.Abs(iIntrestFactor);
					break;
			}

			switch (action.opIntrestOperator)
			{
				case Operator.Set:
					currentWeapon.iIntrest = iIntrestFactor;
					break;
				case Operator.Add:
					currentWeapon.iIntrest += iIntrestFactor;
					break;
				case Operator.Subtract:
					currentWeapon.iIntrest -= iIntrestFactor;
					break;
				case Operator.Multiply:
					currentWeapon.iIntrest *= iIntrestFactor;
					break;
				case Operator.Divide:
					if (iIntrestFactor > 0) currentWeapon.iIntrest = Mathf.RoundToInt(currentWeapon.iIntrest / iIntrestFactor);
					break;
			}

			int iOpinionFactor = action.iOpinionFactor;

			switch (action.opmIntrestOperatorMode)
			{
				case OperatorMode.Abs:
					iOpinionFactor = Mathf.Abs(iOpinionFactor);
					break;
				case OperatorMode.Neg_Abs:
					iOpinionFactor = -Mathf.Abs(iOpinionFactor);
					break;
			}

			switch (action.opOpinionOperator)
			{
				case Operator.Set:
					currentWeapon.iOpinion = iOpinionFactor;
					break;
				case Operator.Add:
					currentWeapon.iOpinion += iOpinionFactor;
					break;
				case Operator.Subtract:
					currentWeapon.iOpinion -= iOpinionFactor;
					break;
				case Operator.Multiply:
					currentWeapon.iOpinion *= iOpinionFactor;
					break;
				case Operator.Divide:
					if (iOpinionFactor > 0) currentWeapon.iOpinion = Mathf.RoundToInt(currentWeapon.iOpinion / iOpinionFactor);
					break;
			}

			if (action.bApplyOpinion) ApplyOpinion();

			currentWeapon.iIntrest = Mathf.Clamp(currentWeapon.iIntrest, 0, currentWeapon.weapon.iIntrestToWin);

			SayDialogue(Util.GetRandomItem(action.sPosibleDialogues), Util.GetRandomItem(action.sPosibleResponses));

			hand.RemoveAt(index);
			hand.Add(Util.GetRandomItem(cards));
			RefreshHand();
		}

		public void ApplyOpinion()
		{
			currentWeapon.iIntrest += currentWeapon.iOpinion;
			currentWeapon.iOpinion = 0;
		}

		public void SayDialogue(string sPlayerText, string sWeaponText)
		{
			textPlayerDio.text = sPlayerText;
			textWeaponDio.text = sWeaponText;

			textPlayerDio.DOColor(cOgDioColor, 0.1f).OnComplete(() => textPlayerDio.DOColor(Color.clear, 0.1f).SetDelay(fDioTime).OnComplete
				(() =>
					textPlayerDio.DOColor(cOgDioColor, 0.1f).OnComplete(() => textPlayerDio.DOColor(Color.clear, 0.1f).SetDelay(fDioTime))
				));
		}
	}
}