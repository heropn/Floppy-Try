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

	//private const string highscoreBase = "Highscore: ";

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		GameManager.Instance.GameEnded += SetActiveRestartUI;

		restartButton.onClick.AddListener(RestartButtonClicked);
		restartButton.gameObject.SetActive(false);
		highscoreText.gameObject.SetActive(false);

		//highscoreText.text = highscoreBase + PlayerPrefs.GetInt("TestHighScore").ToString();
	}

	private void SetActiveRestartUI()
	{
		restartButton.gameObject.SetActive(true);
		highscoreText.gameObject.SetActive(true);
	}
	
	private void RestartButtonClicked()
	{
		onRestartButtonClicked?.Invoke();
		restartButton.gameObject.SetActive(false);
		highscoreText.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		GameManager.Instance.GameEnded -= SetActiveRestartUI;
	}
}
