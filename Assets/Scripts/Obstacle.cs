using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public static event Action<int> onDestroyed;
	private float gameBorderX;

	private void Start()
	{
		GameManager.Instance.onGameRestart += DestroySelf;
	}

	private void Update()
	{
		if (transform.position.x < gameBorderX)
		{
			onDestroyed?.Invoke(1);
			DestroySelf();
		}
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}

	public void SetUpDestroyLocation(float gameBorderX)
	{
		this.gameBorderX = gameBorderX;
	}

	private void OnDestroy()
	{
		GameManager.Instance.onGameRestart -= DestroySelf;
	}
}
