using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] 
	private float jumpForce = 20.0f;

	public event Action onCollisionDetected;
	public event Action onTriggerDetected;

	private Rigidbody2D rigidBody;

	private AudioSource audioSource;

	private bool isPlaying;

	[SerializeField]
	private AudioClip deathSound;
	[SerializeField]
	private AudioClip jumpSound;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
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
		audioSource.PlayOneShot(jumpSound);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		audioSource.PlayOneShot(deathSound);
		onCollisionDetected?.Invoke();
		rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		onTriggerDetected?.Invoke();
	}

	private void OnDestroy()
	{
		GameManager.Instance.onGameStarted -= StartGame;
		GameManager.Instance.onGameRestart += RestetGame;
	}
}
