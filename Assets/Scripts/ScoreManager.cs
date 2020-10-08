using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public int pointsScored { get; private set; }

	private const string hihgscoreString = "Highscore";

	public int highscore { get; private set; }

	private void Start()
	{
		GameManager.Instance.PlayerObject.onTriggerDetected += ScorePoint;
		GameManager.Instance.onGameRestart += RestartPoints;
		GameManager.Instance.GameEnded += SetHighScoreIfHigher;

		highscore = PlayerPrefs.GetInt(hihgscoreString, 0);
	}

	public void ScorePoint()
	{
		++pointsScored;
	}

	public void SetHighScoreIfHigher()
	{
		if (highscore < pointsScored)
		{
			PlayerPrefs.SetInt(hihgscoreString, pointsScored);
			highscore = pointsScored;
		}
	}
	public void RestartPoints()
	{
		pointsScored = 0;
	}

	private void OnDestroy()
	{
		GameManager.Instance.PlayerObject.onTriggerDetected -= ScorePoint;
		GameManager.Instance.onGameRestart -= RestartPoints;
		GameManager.Instance.GameEnded -= SetHighScoreIfHigher;
	}
}
