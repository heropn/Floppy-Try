using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Player player;

	private float gameSpeed;
	private bool isPlaying;

	private void Start()
	{
		gameSpeed = GameManager.instance.gameSpeed;
		GameManager.instance.onGameStarted += StartGame;
		GameManager.instance.onGameRestart += RestartGame;
	}

	private void Update()
	{
		if (isPlaying)
		{
			transform.Translate(Vector2.right * Time.deltaTime * gameSpeed);
			player.FollowCamera(transform.localPosition.x);
		}
	}

	private void StartGame()
	{
		isPlaying = true;
	}

	private void RestartGame()
	{
		isPlaying = false;
		player.ResetPlayerPosition();
		transform.localPosition = new Vector3(0, 0, -10);
	}

	private void OnDestroy()
	{
		GameManager.instance.onGameStarted -= StartGame;
		GameManager.instance.onGameRestart -= RestartGame;
	}
}
