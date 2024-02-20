using UnityEngine;
using System;

public class Plant : MonoBehaviour
{
	[Serializable]
	public class GrowthStage
	{
		public Sprite sprite;
		public float growthTime;
	}

	public string plantName = "Planta";
	public GrowthStage[] growthStages;
	public Action<Plant> onReadyToHarvest;

	private int currentStageIndex = 0;
	private float growTimer = 0f;
	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = growthStages[0].sprite;
	}

	private void Update()
	{
		if (currentStageIndex < growthStages.Length)
		{
			growTimer += Time.deltaTime;

			if (growTimer >= growthStages[currentStageIndex].growthTime)
			{
				NextStage();
			}
		}
	}

	private void NextStage()
	{
		growTimer = 0f;
		currentStageIndex++;

		if (currentStageIndex < growthStages.Length)
		{
			spriteRenderer.sprite = growthStages[currentStageIndex].sprite;
		}
		else
		{
			// A planta está pronta para colher
			if (onReadyToHarvest != null)
			{
				onReadyToHarvest(this);
			}
		}
	}

	public void Harvest()
	{
		// Lógica para colher a planta
		Debug.Log("Colhendo a planta " + plantName);
		// Reiniciar a planta para outro ciclo de crescimento, se necessário
		currentStageIndex = 0;
		growTimer = 0f;
		spriteRenderer.sprite = growthStages[currentStageIndex].sprite;
	}
}
