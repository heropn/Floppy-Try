using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartScreen : MonoBehaviour
{
	[SerializeField]
	private Button restartButton;
	[SerializeField]
	private Button exitGameButton;

	public Button RestartButton => restartButton;

	[SerializeField]
	private TextMeshProUGUI highscoreText;

	private void Start()
	{
		exitGameButton.onClick.AddListener(Application.Quit);
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
		exitGameButton.gameObject.SetActive(value);
		restartButton.gameObject.SetActive(value);
		highscoreText.gameObject.SetActive(value);
	}
}
