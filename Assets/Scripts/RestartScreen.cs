using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartScreen : MonoBehaviour
{
	[SerializeField]
	private Button restartButton;

	public Button RestartButton => restartButton;

	[SerializeField]
	private TextMeshProUGUI highscoreText;

	private void Start()
	{
		SetActiveRestartScreen(false);
	}

	public void SetActiveRestartUI()
	{
		string highscore = GameManager.Instance.ScoreManager.Highscore.ToString();
		highscoreText.text = $"Highscore: {highscore}";
		SetActiveRestartScreen(true);
	}

	public void SetActiveRestartScreen(bool value)
	{
		restartButton.gameObject.SetActive(value);
		highscoreText.gameObject.SetActive(value);
	}
}
