using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	private TextMeshProUGUI textUI;

	private void Awake()
	{
		textUI = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		textUI.text = "0";
	}

	public void UpdateScore(int score)
	{
		textUI.text = (score).ToString();
	}
}
