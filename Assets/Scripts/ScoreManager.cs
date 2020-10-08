using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public event Action<int> onUpdatedScorePoints;

	public int PointsScored { get; private set; }

	private const string highscoreString = "Highscore";

	public int Highscore { get; private set; }

	private void Start()
	{
		GameManager.Instance.Player.onTriggerDetected += ScorePoint;
		GameManager.Instance.onGameRestart += RestartPoints;
		GameManager.Instance.GameEnded += SetHighScoreIfHigher;

		Highscore = PlayerPrefs.GetInt(highscoreString, 0);
	}

	private void ScorePoint()
	{
		onUpdatedScorePoints.Invoke(++PointsScored);
	}

	private void SetHighScoreIfHigher()
	{
		if (Highscore < PointsScored)
		{
			PlayerPrefs.SetInt(highscoreString, PointsScored);
			Highscore = PointsScored;
		}
	}
	private void RestartPoints()
	{
		PointsScored = 0;
		onUpdatedScorePoints.Invoke(PointsScored);
	}

	private void OnDestroy()
	{
		GameManager.Instance.Player.onTriggerDetected -= ScorePoint;
		GameManager.Instance.onGameRestart -= RestartPoints;
		GameManager.Instance.GameEnded -= SetHighScoreIfHigher;
	}
}
