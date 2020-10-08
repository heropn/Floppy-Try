using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartScreen : MonoBehaviour
{
	public static RestartScreen Instance { get; private set; }

	public event Action onRestartButtonClicked;

	[SerializeField]
	private Button restartButton;
	[SerializeField]
	private TextMeshProUGUI highscoreText;

	private const string highscoreBase = "Highscore: ";

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		restartButton.onClick.AddListener(RestartButtonClicked);
		SetActiveRestartScreen(false);
	}

	public void SetActiveRestartUI()
	{
		highscoreText.text = highscoreBase + GameManager.Instance.scoreManager.highscore.ToString();
		SetActiveRestartScreen(true);
	}
	
	private void RestartButtonClicked()
	{
		onRestartButtonClicked?.Invoke();
		SetActiveRestartScreen(false);
	}

	private void SetActiveRestartScreen(bool value)
	{
		restartButton.gameObject.SetActive(value);
		highscoreText.gameObject.SetActive(value);
	}
}
