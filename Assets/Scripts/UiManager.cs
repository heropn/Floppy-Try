using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
	[SerializeField]
	private ScoreManager scoreManager;
	[SerializeField]
	private RestartScreen restartScreen;

	private void Start()
	{
		Player.Instance.onTriggerDetected += PlayerCollide;
		GameManager.Instance.onGameRestart += RestartGame;
		GameManager.Instance.GameEnded += GameEnd;
	}

	private void PlayerCollide()
	{
		scoreManager.ScorePoint();
	}

	private void RestartGame()
	{
		scoreManager.RestartPoints();
	}

	private void GameEnd()
	{
		scoreManager.SetHighScoreIfHigher();
		restartScreen.SetActiveRestartUI();
	}

	private void OnDestroy()
	{
		Player.Instance.onTriggerDetected -= PlayerCollide;
		GameManager.Instance.onGameRestart -= RestartGame;
		GameManager.Instance.GameEnded -= GameEnd;
	}
}
