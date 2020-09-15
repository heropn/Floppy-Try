using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartScreen : MonoBehaviour
{
	public static RestartScreen Instance { get; private set; }

	public event Action onRestartButtonClicked;

	[SerializeField]
	private Button restartButton;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		GameManager.Instance.onGameEnded += SetActiveButton;
		restartButton.onClick.AddListener(RestartButtonClicked);
		restartButton.gameObject.SetActive(false);
	}

	private void SetActiveButton()
	{
		restartButton.gameObject.SetActive(true);
	}
	
	private void RestartButtonClicked()
	{
		onRestartButtonClicked?.Invoke();
		restartButton.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		GameManager.Instance.onGameEnded -= SetActiveButton;
	}
}
