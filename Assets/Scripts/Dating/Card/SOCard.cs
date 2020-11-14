#pragma warning disable 649
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dating
{
	[CreateAssetMenu(fileName = "Card", menuName = "Game/Dating/Card", order = 2)]
	public class SOCard : ScriptableObject
	{
		[System.Serializable]
		public struct Action
		{
			[Min(1)] public int iWeight;

			public bool bApplyOpinion;
			[Space]
			public Operator opIntrestOperator;
			public OperatorMode opmIntrestOperatorMode;
			public int iIntrestFactor;
			[Space]
			public Operator opOpinionOperator;
			public int iOpinionFactor;
			public OperatorMode opmOpinionOperatorMode;
			[Space]
			public string[] sPosibleDialogues;
		}

		[NaughtyAttributes.ShowAssetPreview] public Sprite sprite;
		[TextArea] public string sDescription;
		[Space]
		[NaughtyAttributes.ReorderableList] public Action[] actions;

		public Action Use()
		{
			Action action = actions[actions.Length - 1];

			int iWeightSum = 0;
			for (int i = 0; i < actions.Length; ++i)
			{
				iWeightSum += actions[i].iWeight;
			}

			int index = 0;
			while (index < actions.Length - 1)
			{
				if (Random.Range(0, iWeightSum) < actions[index].iWeight)
				{
					action = actions[index];
					break;
				}

				iWeightSum -= actions[index++].iWeight;
			}

			return action;
		}
	}
}