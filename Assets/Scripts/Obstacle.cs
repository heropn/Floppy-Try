using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public event Action<int> onDestroyed;

	private float gameBorderX;

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
}
