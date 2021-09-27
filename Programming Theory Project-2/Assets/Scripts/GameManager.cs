using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject score;

    [SerializeField] private Vector3 spawnPos;

    [SerializeField] private GameObject[] fances;

    [SerializeField] private GameObject afterLose;

    public Text bestScore;

    private float secondsToWait = 0;
    
    public bool gameOver = true;

    private int points = 0;

    private int maxScore = 0;

    private void Start() {
        Instance = this;
        
        LoadData();
        
        bestScore.text = $"Best score: {maxScore}";
    }

    IEnumerator SpawnRandomFence() {
        yield return new WaitForSeconds(secondsToWait);
        if (!gameOver) {

            if (secondsToWait == 0)
                secondsToWait++;

            int chance = Random.Range(0, 50);

            spawnPos.y = Random.Range(-2, 1.6f);
            if (chance < 45)
                Instantiate(fances[0], spawnPos, fances[0].transform.rotation);
            else
                Instantiate(fances[1], spawnPos, fances[1].transform.rotation);
            StartCoroutine(SpawnRandomFence());
        }
    }

    public void GameOver() {
        gameOver = true;
        GameObject newRecord = afterLose.transform.GetChild(0).gameObject;
        
        afterLose.SetActive(true);
        score.SetActive(false);

        if (points > maxScore) {
            maxScore = points;

            SaveData();

            newRecord.GetComponent<Text>().text = $"New record: {maxScore}";
        }
        else {
            newRecord.GetComponent<Text>().color = Color.yellow;
            newRecord.GetComponent<Text>().text = $"Your score: {points}";
        }
    }

    public void updateScore(int pointsToAdd) {
        points += pointsToAdd;
        score.GetComponent<Text>().text = $"{points}";
    }

    #region Data

    [System.Serializable]

    class DataToSave {
        public int maxScore;
    }

    public void SaveData() {
        DataToSave data = new DataToSave();
        data.maxScore = maxScore;

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData() {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            DataToSave data = JsonUtility.FromJson<DataToSave>(json);

            maxScore = data.maxScore;
        }
    }

    #endregion
    
    #region Menu

    [SerializeField] private GameObject menu;

    public void StartGame() {
        score.SetActive(true);
        menu.SetActive(false);
        
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

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }
    
    #endregion
}
