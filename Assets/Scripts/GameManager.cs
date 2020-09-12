using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] public static float gameSpeed = 3.0f;

	public static GameManager instance;

	public event Action onGameStarted;
	public event Action onGameRestart;

	private bool isGamePlaying;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Player.instance.onCollisionDetected += EndGame;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !isGamePlaying)
		{
			onGameStarted.Invoke();
			isGamePlaying = true;
			Debug.Log("GAME STARTED");
		}
	}

	private void EndGame()
	{
		onGameRestart.Invoke();
		isGamePlaying = false;
	}

	private void OnDestroy()
	{
		Player.instance.onCollisionDetected -= EndGame;
	}
}
