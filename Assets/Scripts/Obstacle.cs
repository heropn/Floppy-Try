using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public static event Action<int> onDestroyed;
	private void Start()
	{
		GameManager.Instance.onGameRestart += DestroySelf;
	}

	private void Update()
	{
		if (transform.position.x < GameManager.Instance.gameBorderXvar)
		{
			onDestroyed?.Invoke(1);
			DestroySelf();
		}
	}

	private void DestroySelf()
	{
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		GameManager.Instance.onGameRestart -= DestroySelf;
	}
}
