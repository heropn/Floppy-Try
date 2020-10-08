using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
	public static UiManager Instance { get; private set; }

	public event Action onRestartButtonClicked;

	[SerializeField]
	private ScoreUI scoreUI;
	[SerializeField]
	private RestartScreen restartScreen;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		GameManager.Instance.ScoreManager.onUpdatedScorePoints += UpdateScorePoints;
		GameManager.Instance.GameEnded += GameEnd;

		restartScreen.RestartButton.onClick.AddListener(RestartButtonClicked);
	}

	private void UpdateScorePoints(int score)
	{
		scoreUI.UpdateScore(score);
	}

	private void RestartButtonClicked()
	{
		onRestartButtonClicked?.Invoke();
		restartScreen.SetActiveRestartScreen(false);
	}

	private void GameEnd()
	{
		restartScreen.SetActiveRestartUI();
	}

	private void OnDestroy()
	{
		GameManager.Instance.GameEnded -= GameEnd;
		GameManager.Instance.ScoreManager.onUpdatedScorePoints -= UpdateScorePoints;
	}
}
