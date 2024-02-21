using UnityEngine;
using System;

public class Seed : MonoBehaviour
{
	[SerializeField] private GameObject drop;

	[Serializable]
	public class GrowthStage
	{
		public Sprite sprite;
		public float growthTime;
	}

	public string plantName = "Planta";
	public GrowthStage[] growthStages;
	public Action<Seed> onReadyToHarvest;

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
		if(currentStageIndex>= growthStages.Length) 
		{
			Instantiate(drop, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	
	}
}
