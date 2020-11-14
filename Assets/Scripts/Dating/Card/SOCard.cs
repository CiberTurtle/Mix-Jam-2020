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
			public int iIntrestFactor;
			[Space]
			public Operator opOpinionOperator;
			public int iOpinionFactor;
		}

		[NaughtyAttributes.ShowAssetPreview] public Sprite sprite;
		[TextArea] public string sDescription;
		[Space]
		[NaughtyAttributes.ReorderableList] public List<Action> actions = new List<Action>();

		public Action Use()
		{
			Action action = actions[actions.Count - 1];

			int iWeightSum = 0;
			for (int i = 0; i < actions.Count; ++i)
			{
				iWeightSum += actions[i].iWeight;
			}

			int index = 0;
			while (index < actions.Count - 1)
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