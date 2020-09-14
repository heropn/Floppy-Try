using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private void Start()
	{
		GameManager.instance.onGameRestart += DestroySelf;
	}

	private void Update()
	{
		if (transform.position.x < GameManager.instance.GameBorderXvar)
		{
			GameManager.instance.SpawnObstacles(1);
			Destroy(gameObject);
		}
	}

	private void DestroySelf()
	{
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		GameManager.instance.onGameRestart -= DestroySelf;
	}
}
