using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
	[SerializeField]
	private ScoreUI scoreUI;
	[SerializeField]
	private RestartScreen restartScreen;

	private void Start()
	{
		GameManager.Instance.PlayerObject.onTriggerDetected += PlayerCollide;
		GameManager.Instance.onGameRestart += RestartPoints;
		GameManager.Instance.GameEnded += GameEnd;
	}

	private void PlayerCollide()
	{
		scoreUI.ScorePoint();
	}

	private void RestartPoints()
	{
		scoreUI.RestartPoints();
	}

	private void GameEnd()
	{
		restartScreen.SetActiveRestartUI();
	}

	private void OnDestroy()
	{
		GameManager.Instance.PlayerObject.onTriggerDetected -= PlayerCollide;
		GameManager.Instance.onGameRestart -= RestartPoints;
		GameManager.Instance.GameEnded -= GameEnd;
	}
}
