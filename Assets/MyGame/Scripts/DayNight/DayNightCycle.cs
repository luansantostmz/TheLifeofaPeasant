using UnityEngine;
using System.Collections.Generic;

public class DayNightCycle2D : MonoBehaviour
{
	public enum Season { Spring, Summer, Autumn, Winter }

	[SerializeField] private float seasonDuration = 15f;
	public Season currentSeason = Season.Spring;
	[SerializeField] private float fullDayDurationInMinutes = 10f;
	[Range(0, 1)]
	public float currentTimeOfDay = 0.5f;

	public int currentDay = 1;
	private Dictionary<Season, GameObject> seasonSprites = new Dictionary<Season, GameObject>();
	private float dayCycleSpeed;

	private void Start()
	{
		dayCycleSpeed = 1f / (fullDayDurationInMinutes * 60);
		CacheSeasonSprites();
		SetActiveSeasonSprite(currentSeason);
	}

	private void Update()
	{
		UpdateTime();
	}

	private void UpdateTime()
	{
		currentTimeOfDay += Time.deltaTime * dayCycleSpeed;
		if (currentTimeOfDay >= 1f)
		{
			currentTimeOfDay = 0f;
			AdvanceDay();
		}
	}

	private void AdvanceDay()
	{
		currentDay++;
		if (currentDay > seasonDuration) AdvanceSeason();
	}

	private void AdvanceSeason()
	{
		currentDay = 0;
		currentSeason = (Season)(((int)currentSeason + 1) % System.Enum.GetValues(typeof(Season)).Length);
		SetActiveSeasonSprite(currentSeason);
	}

	private void CacheSeasonSprites()
	{
		foreach (Season season in System.Enum.GetValues(typeof(Season)))
		{
			GameObject spriteObject = GameObject.Find(season.ToString() + "Sprite");
			if (spriteObject != null)
			{
				seasonSprites.Add(season, spriteObject);
				spriteObject.SetActive(false);
			}
			else
			{
				Debug.LogError("Sprite not found for season: " + season.ToString());
			}
		}
	}

	private void SetActiveSeasonSprite(Season season)
	{
		foreach (var sprite in seasonSprites)
		{
			sprite.Value.SetActive(sprite.Key == season);
		}
	}	

	public string CurrentTimeFormatted
	{
		get
		{
			int hour = Mathf.FloorToInt(currentTimeOfDay * 24);
			int minute = Mathf.FloorToInt((currentTimeOfDay * 24 - hour) * 60);
			return hour.ToString("00") + ":" + minute.ToString("00");
		}
	}
}
