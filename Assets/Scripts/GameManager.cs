using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//singleton
	public static GameManager Instance { get; private set; }
	public float GameBorderXvar { get; private set; }

	public event Action onGameStarted;
	public event Action onGameRestart;

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

	private GameObject lastObstacle;

	private float gameSpeed = 3.0f;

	private bool isGamePlaying;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		Player.Instance.onCollisionDetected += EndGame;
		Obstacle.onDestroyed += SpawnObstacles;

		//calculating height and witdh of screen and setting up bottom and top colliders
		var camera = Camera.main;
		float height = 2.0f * camera.orthographicSize;
		float width = height * camera.aspect;

		topCameraCollider.size = new Vector2(width, 1);
		topCameraCollider.transform.position = new Vector2(0, (height / 2) + 0.5f);

		bottomCameraCollider.size = new Vector2(width, 1);
		bottomCameraCollider.transform.position = new Vector2(0, -((height / 2) + 0.5f));

		GameBorderXvar = -(width / 2.0f) - 2.0f;

		obstaclesOnScreenCount = (int)(width / distanceBetweenObstacles + 2); //2 because we need to keep some additional ones for other ones to spawn off screen

		SpawnObstacles(obstaclesOnScreenCount);
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
		if (count == 1 && lastObstacle)
		{
			float xVar = lastObstacle.transform.position.x + distanceBetweenObstacles;
			float yVar = UnityEngine.Random.Range(-obstacleRandomRangeY, obstacleRandomRangeY);
			lastObstacle = Instantiate(obstaclePrefab, new Vector2(xVar, yVar), Quaternion.identity, obstacleParentTransform);
			return;
		}

		for (int i = 1; i < count + 1; i++)
		{
			float xVar = i * distanceBetweenObstacles;
			float yVar = UnityEngine.Random.Range(-obstacleRandomRangeY, obstacleRandomRangeY);
			lastObstacle = Instantiate(obstaclePrefab, new Vector2(xVar, yVar), Quaternion.identity, obstacleParentTransform);
		}
	}

	private void MoveBoard()
	{
		obstacleParentTransform.Translate(Vector2.left * Time.deltaTime * gameSpeed);
	}

	private void EndGame()
	{
		lastObstacle = null;
		isGamePlaying = false;
		RestartGame();
	}

	private void RestartGame()
	{
		onGameRestart?.Invoke();
		obstacleParentTransform.localPosition = Vector2.zero;
		SpawnObstacles(obstaclesOnScreenCount);
	}

	private void OnDestroy()
	{
		Player.Instance.onCollisionDetected -= EndGame;
		Obstacle.onDestroyed -= SpawnObstacles;
	}
}
