using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
	private Button startButton;

	private void Awake()
	{
		startButton = GetComponent<Button>();
	}

	private void Start()
	{
		startButton.onClick.AddListener(LoadGameScene);
	}

	private void LoadGameScene()
	{
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}
}
