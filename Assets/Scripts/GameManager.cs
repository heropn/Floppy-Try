﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//singleton
	public static GameManager Instance { get; private set; }
	public float GameBorderX { get; private set; }

	public event Action onGameStarted;
	public event Action onGameRestart;
	public event Action GameEnded;

	[SerializeField] 
	private int obstaclesOnScreenCount = 10;
	[SerializeField] 
	private float distanceBetweenObstacles = 5.0f;
	[SerializeField] 
	private float obstacleRandomRangeY = 2.0f;

	[SerializeField] 
	private BoxCollider2D topCameraCollider;
	[SerializeField] 
	private BoxCollider2D bottomCameraCollider;
	[SerializeField] 
	private GameObject obstaclePrefab;
	[SerializeField] 
	private Transform obstacleParentTransform;

	private List<Obstacle> obstacles = new List<Obstacle>();

	private float gameSpeed = 3.0f;

	private bool isGamePlaying;

	private enum GameState
	{
		Waiting,
		Playing,
		Stopped
	}

	private GameState currentStateOfGame;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		Player.Instance.onCollisionDetected += EndGame;
		Obstacle.onDestroyed += SpawnObstacles;
		RestartScreen.Instance.onRestartButtonClicked += RestartGame;

		//calculating height and witdh of screen and setting up bottom and top colliders
		var camera = Camera.main;
		float height = 2.0f * camera.orthographicSize;
		float width = height * camera.aspect;

		topCameraCollider.size = new Vector2(width, 1);
		topCameraCollider.transform.position = new Vector2(0, (height / 2) + 0.5f);

		bottomCameraCollider.size = new Vector2(width, 1);
		bottomCameraCollider.transform.position = new Vector2(0, -((height / 2) + 0.5f));

		GameBorderX = -(width / 2.0f) - 2.0f;

		obstaclesOnScreenCount = (int)(width / distanceBetweenObstacles + 2); //2 because we need to keep some additional ones for other ones to spawn off screen

		SpawnObstacles(obstaclesOnScreenCount);

		currentStateOfGame = GameState.Waiting;
	}

	private void Update()
	{
		if (currentStateOfGame == GameState.Playing)
		{
			MoveBoard();
		}
		else if (Input.GetKeyDown(KeyCode.Space) && currentStateOfGame == GameState.Waiting)
		{
			onGameStarted?.Invoke();
			currentStateOfGame = GameState.Playing;
		}
	}
	public void SpawnObstacles(int count)
	{
		if (count == 1 && obstacles.Count > 0)
		{
			float x = obstacles[obstacles.Count - 1].transform.position.x + distanceBetweenObstacles;
			float y = UnityEngine.Random.Range(-obstacleRandomRangeY, obstacleRandomRangeY);
			obstacles.Add(Instantiate(obstaclePrefab, new Vector2(x, y), Quaternion.identity, obstacleParentTransform).GetComponent<Obstacle>());
			obstacles.RemoveAt(0);
			return;
		}

		for (int i = 1; i < count + 1; i++)
		{
			float x = i * distanceBetweenObstacles;
			float y = UnityEngine.Random.Range(-obstacleRandomRangeY, obstacleRandomRangeY);
			obstacles.Add(Instantiate(obstaclePrefab, new Vector2(x, y), Quaternion.identity, obstacleParentTransform).GetComponent<Obstacle>());
		}
	}

	private void MoveBoard()
	{
		obstacleParentTransform.Translate(Vector2.left * Time.deltaTime * gameSpeed);
	}

	private void EndGame()
	{
		isGamePlaying = false;
		currentStateOfGame = GameState.Stopped;
		GameEnded?.Invoke();
	}

	private void RestartGame()
	{
		onGameRestart?.Invoke();
		obstacleParentTransform.localPosition = Vector2.zero;
		obstacles.Clear();
		SpawnObstacles(obstaclesOnScreenCount);
		currentStateOfGame = GameState.Waiting;
	}

	private void OnDestroy()
	{
		Player.Instance.onCollisionDetected -= EndGame;
		Obstacle.onDestroyed -= SpawnObstacles;
		RestartScreen.Instance.onRestartButtonClicked -= RestartGame;
	}
}
