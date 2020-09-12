using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] public static float gameSpeed = 3.0f;

	[SerializeField] private GameObject topCameraCollider;
	[SerializeField] private GameObject bottomCameraCollider;

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

		//calculating height and witdh of screen and setting up bottom and top colliders
		Camera camera = Camera.main;
		float height = 2.0f * camera.orthographicSize;
		float width = height * camera.aspect;

		topCameraCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, 1);
		topCameraCollider.transform.position = new Vector2(0, (height / 2) + 0.5f);

		bottomCameraCollider.GetComponent<BoxCollider2D>().size = new Vector2(width, 1);
		bottomCameraCollider.transform.position = new Vector2(0, -((height / 2) + 0.5f));

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
