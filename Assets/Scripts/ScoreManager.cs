using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private TextMeshProUGUI textUI;
	private int scorePoints;

	private void Awake()
	{
		textUI = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		Player.Instance.onTriggerDetected += ScoreAPoint;
		GameManager.Instance.onGameRestart += RestartPoints;
		scorePoints = 0;
		textUI.text = scorePoints.ToString();
	}

	private void ScoreAPoint()
	{
		textUI.text = (++scorePoints).ToString();
	}

	private void RestartPoints()
	{
		scorePoints = 0;
		textUI.text = scorePoints.ToString();
	}

	private void OnDestroy()
	{
		Player.Instance.onTriggerDetected -= ScoreAPoint;
		GameManager.Instance.onGameRestart -= RestartPoints;
	}
}
