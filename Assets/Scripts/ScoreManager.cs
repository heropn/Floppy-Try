using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private TextMeshProUGUI textUI;
	private int scorePoints;

	private void Awake()
	{
		textUI = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		scorePoints = 0;
		textUI.text = scorePoints.ToString();
	}

	public void ScorePoint()
	{
		textUI.text = (++scorePoints).ToString();
	}

	public void SetHighScoreIfHigher()
	{
		if (PlayerPrefs.GetInt("Highscore", 0) < scorePoints)
		{
			PlayerPrefs.SetInt("Highscore", scorePoints);
		}
	}
	public void RestartPoints()
	{
		scorePoints = 0;
		textUI.text = scorePoints.ToString();
	}
}
