﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float jumpForce = 20.0f;

	public static Player instance;
	public event Action onCollisionDetected;

	private Rigidbody2D rigidBody;

	private bool isPlaying;


	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		instance = this;
	}

	private void Start()
	{
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
		GameManager.instance.onGameStarted += StartGame;
		GameManager.instance.onGameRestart += RestetGame;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isPlaying)
			Jump();
	}

	private void StartGame()
	{
		rigidBody.constraints = RigidbodyConstraints2D.None;
		rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		isPlaying = true;
		Jump();
	}

	private void RestetGame()
	{
		isPlaying = false;
	}

	private void Jump()
	{
		rigidBody.velocity = Vector2.up * jumpForce;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		onCollisionDetected.Invoke();
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
		Debug.Log("COLLISION");
	}

	public void ResetPlayerPosition()
	{
		transform.localPosition = Vector2.zero;
	}

	public void FollowCamera(float xValue)
	{
		transform.localPosition = new Vector2(xValue, transform.localPosition.y);
	}

	private void OnDestroy()
	{
		GameManager.instance.onGameStarted -= StartGame;
		GameManager.instance.onGameRestart += RestetGame;
	}
}
