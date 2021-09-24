using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject score;
    
    public bool gameOver = true;

    private void Start() {
        Instance = this;
    }

    public void StartGame() {
        score.SetActive(true);
        GameObject.Find("Menu").SetActive(false);
        
        gameOver = false;
        
        player.GetComponent<Rigidbody>().useGravity = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
