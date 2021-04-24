using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
	private Button exitButton;

	private void Awake()
	{
		exitButton = GetComponent<Button>();
	}

	private void Start()
	{
		exitButton.onClick.AddListener(Application.Quit);
	}
}
