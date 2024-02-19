using UnityEngine;
using TMPro;

public class DayNightCycleUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI dayText;
	[SerializeField] private TextMeshProUGUI timeText;
	[SerializeField] private TextMeshProUGUI seasonText;

	// Day-night cycle script reference
	private DayNightCycle2D dayNightCycle;

	void Start()
	{
		// Get reference to DayNightCycle2D script
		dayNightCycle = FindObjectOfType<DayNightCycle2D>();
		if (dayNightCycle == null)
		{
			Debug.LogError("DayNightCycle2D script not found in the scene.");
		}
	}

	void Update()
	{
		UpdateUI();
	}

	void UpdateUI()
	{
		// Update the day text
		dayText.text = "Day: " + dayNightCycle.currentDay;

		// Update the time text
		timeText.text = "Time: " + dayNightCycle.CurrentTimeFormatted;

		// Update the season text
		seasonText.text = "Season: " + dayNightCycle.currentSeason.ToString();
	}
}
