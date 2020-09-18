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

	private int highScore;

	private const string highscoreBase = "Highscore: ";

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		restartButton.onClick.AddListener(RestartButtonClicked);
		restartButton.gameObject.SetActive(false);
		highscoreText.gameObject.SetActive(false);
	}

	public void SetActiveRestartUI()
	{
		highScore = PlayerPrefs.GetInt("Highscore", 0);
		highscoreText.text = highscoreBase + highScore.ToString();
		restartButton.gameObject.SetActive(true);
		highscoreText.gameObject.SetActive(true);
	}
	
	private void RestartButtonClicked()
	{
		onRestartButtonClicked?.Invoke();
		restartButton.gameObject.SetActive(false);
		highscoreText.gameObject.SetActive(false);
	}
}
