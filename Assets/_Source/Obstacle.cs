using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public event Action<Obstacle> onPassedBorder;

	private float gameBorderX;

	private void Update()
	{
		if (transform.position.x < gameBorderX)
		{
			onPassedBorder?.Invoke(this);
		}
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	public void Initialize(float gameBorderX)
	{
		this.gameBorderX = gameBorderX;
	}
}
