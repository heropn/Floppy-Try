using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	private TextMeshProUGUI textUI;
	private int scorePoints;

	private void Awake()
	{
		textUI = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		scorePoints = 0;
		textUI.text = scorePoints.ToString();
	}

	public void ScorePoint()
	{
		textUI.text = (++scorePoints).ToString();
	}

	public void RestartPoints()
	{
		scorePoints = 0;
		textUI.text = scorePoints.ToString();
	}
}
