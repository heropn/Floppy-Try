using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//singleton
	public static GameManager instance;

	public float gameSpeed = 3.0f;

	[SerializeField] private GameObject topCameraCollider;
	[SerializeField] private GameObject bottomCameraCollider;
	[SerializeField] private GameObject obstaclePrefab;
	[SerializeField] private Transform obstacleParentTransform;

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

		//calculating height and witdh of screen and setting up bottom and top colliders
		Camera camera = Camera.main;
		float height = 2.0f * camera.orthographicSize;
		float width = height * camera.aspect;

		topCameraCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, 1);
		topCameraCollider.transform.position = new Vector2(0, (height / 2) + 0.5f);

		bottomCameraCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, 1);
		bottomCameraCollider.transform.position = new Vector2(0, -((height / 2) + 0.5f));

		SpawnObstacle(10);
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

	private void SpawnObstacle(int howMany)
	{
		for (int i = 1; i < howMany + 1; i++)
		{
			float xVar = i * 5;
			float yVar = UnityEngine.Random.Range(-1, 1);
			Instantiate(obstaclePrefab, new Vector2(xVar, yVar), Quaternion.identity, obstacleParentTransform);
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
