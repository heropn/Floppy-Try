using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//singleton
	public static GameManager instance { get; private set; }

	private float gameBorderXvar = 0.0f;
	public float GameBorderXvar { get { return gameBorderXvar; } private set { gameBorderXvar = value; } }
	
	[SerializeField] private int howManyObstaclesOnScreen = 10;
	[SerializeField] private float distanceBetweenObstacles = 5.0f;
	[SerializeField] private float obstacleRandomRangeY = 2.0f;

	[SerializeField] private BoxCollider2D topCameraCollider;
	[SerializeField] private BoxCollider2D bottomCameraCollider;
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
		var camera = Camera.main;
		float height = 2.0f * camera.orthographicSize;
		float width = height * camera.aspect;

		topCameraCollider.size = new Vector2(width, 1);
		topCameraCollider.transform.position = new Vector2(0, (height / 2) + 0.5f);

		bottomCameraCollider.size = new Vector2(width, 1);
		bottomCameraCollider.transform.position = new Vector2(0, -((height / 2) + 0.5f));

		gameBorderXvar = -(width / 2.0f) - 2.0f;

		SpawnObstacles(howManyObstaclesOnScreen);
	}

	private void Update()
	{
		if (isGamePlaying)
		{
			MoveBoard();
		}
		else if (Input.GetKeyDown(KeyCode.Space) && !isGamePlaying)
		{
			onGameStarted?.Invoke();
			isGamePlaying = true;
		}
	}
	public void SpawnObstacles(int count)
	{
		if (count == 1)
		{
			float xVar = howManyObstaclesOnScreen * distanceBetweenObstacles;
			float yVar = UnityEngine.Random.Range(-obstacleRandomRangeY, obstacleRandomRangeY);
			Instantiate(obstaclePrefab, new Vector2(xVar, yVar), Quaternion.identity, obstacleParentTransform);
			return;
		}

		for (int i = 1; i < count + 1; i++)
		{
			float xVar = i * distanceBetweenObstacles;
			float yVar = UnityEngine.Random.Range(-obstacleRandomRangeY, obstacleRandomRangeY);
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
		onGameRestart?.Invoke();
		obstacleParentTransform.localPosition = Vector2.zero;
		SpawnObstacles(howManyObstaclesOnScreen);
	}

	private void OnDestroy()
	{
		Player.instance.onCollisionDetected -= EndGame;
	}
}
