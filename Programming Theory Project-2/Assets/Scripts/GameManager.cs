using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; set; }

	public GameObject player;
	
	public bool gameOver = true;

	[SerializeField] private GameObject menuButtons;
	[SerializeField] private Text scoreText;

	[SerializeField] private List<GameObject> fences = new List<GameObject>();
	[SerializeField] private Vector3 spawnPos;

	private void Start() {
		Instance = this;

		StartCoroutine(SpawnFence());
	}

	public void StartGame() {
		menuButtons.gameObject.SetActive(false);
		scoreText.gameObject.SetActive(true);
		
		player.GetComponent<Rigidbody>().useGravity = true;
		gameOver = false;
	}

	public void QuitGame() {
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}

	public void GameOver() {
		gameOver = true;
		
	}

	IEnumerator SpawnFence() {
		yield return new WaitForSeconds(2);

		int rand = Random.Range(0, 50);
		if (rand < 40)
			Instantiate(fences[0], spawnPos, fences[0].transform.rotation);
		else
			Instantiate(fences[1], spawnPos, fences[1].transform.rotation);
			
		StartCoroutine(SpawnFence());
	}
}
