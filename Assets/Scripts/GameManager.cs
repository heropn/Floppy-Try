using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//singleton
	public static GameManager instance;

	public float gameBorderXvar = 0.0f;

	[SerializeField] private GameObject topCameraCollider;
	[SerializeField] private GameObject bottomCameraCollider;
	[SerializeField] private GameObject obstaclePrefab;
	[SerializeField] private Transform obstacleParentTransform;

	public event Action onGameStarted;
	public event Action onGameRestart;

	private float gameSpeed = 3.0f;

	private bool isGamePlaying;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Player.instance.onCollisionDetected += EndGame;

		//calculating height and witdh of screen and setting up bottom and top colliders
		Camera camera = Camera.main;
		float height = 2.0f * camera.orthographicSize;
		float width = height * camera.aspect;

		topCameraCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, 1);
		topCameraCollider.transform.position = new Vector2(0, (height / 2) + 0.5f);

		bottomCameraCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, 1);
		bottomCameraCollider.transform.position = new Vector2(0, -((height / 2) + 0.5f));

		gameBorderXvar = -(width / 2) - 2;

		SpawnObstacle(10);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !isGamePlaying)
		{
			onGameStarted.Invoke();
			isGamePlaying = true;
		}
		else if (isGamePlaying)
		{
			MoveBoard();
		}
	}

	private void SpawnObstacle(int howMany)
	{
		for (int i = 1; i < howMany + 1; i++)
		{
			float xVar = i * 5;
			float yVar = UnityEngine.Random.Range(-1, 1);
			Instantiate(obstaclePrefab, new Vector2(xVar, yVar), Quaternion.identity, obstacleParentTransform);
		}
	}

	private void MoveBoard()
	{
		obstacleParentTransform.Translate(Vector2.left * Time.deltaTime * gameSpeed);
	}

	private void EndGame()
	{
		isGamePlaying = false;
		RestartGame();
	}

	private void RestartGame()
	{
		onGameRestart.Invoke();
		obstacleParentTransform.localPosition = Vector2.zero;
		SpawnObstacle(10);
	}

	private void OnDestroy()
	{
		Player.instance.onCollisionDetected -= EndGame;
	}
}
