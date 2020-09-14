using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] 
	private float jumpForce = 20.0f;

	public static Player Instance { get; private set; }
	public event Action onCollisionDetected;
	public event Action onTriggerDetected;

	private Rigidbody2D rigidBody;

	private bool isPlaying;


	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		Instance = this;
	}

	private void Start()
	{
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
		GameManager.Instance.onGameStarted += StartGame;
		GameManager.Instance.onGameRestart += RestetGame;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isPlaying)
		{
			Jump();
		}
	}

	private void StartGame()
	{
		rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		isPlaying = true;
		Jump();
	}

	private void RestetGame()
	{
		isPlaying = false;
		transform.localPosition = Vector2.zero;
	}

	private void Jump()
	{
		rigidBody.velocity = Vector2.up * jumpForce;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		onCollisionDetected?.Invoke();
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
		Debug.Log("COLLISION");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		onTriggerDetected?.Invoke();
		Debug.Log("TRIGGERED");
	}

	private void OnDestroy()
	{
		GameManager.Instance.onGameStarted -= StartGame;
		GameManager.Instance.onGameRestart += RestetGame;
	}
}
