using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject score;

    [SerializeField] private Vector3 spawnPos;

    [SerializeField] private GameObject[] fances;

    private float secondsToWait = 0;
    
    public bool gameOver = true;

    private void Start() {
        Instance = this;
    }

    IEnumerator SpawnRandomFence() {
        yield return new WaitForSeconds(secondsToWait);

        if (secondsToWait == 0)
            secondsToWait++;

        int chance = Random.Range(0, 50);

        spawnPos.y = Random.Range(-2, 1.6f);
        if (chance < 45)
            Instantiate(fances[0], spawnPos, fances[0].transform.rotation);
        else
            Instantiate(fances[1], spawnPos, fances[1].transform.rotation);
        if(!gameOver)
            StartCoroutine(SpawnRandomFence());
    }
    
    #region Menu

    public void StartGame() {
        score.SetActive(true);
        GameObject.Find("Menu").SetActive(false);
        
        gameOver = false;
        
        StartCoroutine(SpawnRandomFence());

        player.GetComponent<Rigidbody>().useGravity = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
    }
    
    #endregion
}
