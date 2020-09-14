using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private void Start()
	{
		Player.Instance.onTriggerDetected += ScoreAPoint;
	}

	private void ScoreAPoint()
	{

	}
}
