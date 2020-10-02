using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { 
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private int playerCount;
    [SerializeField]
    private List<Transform> spawnPositionList = new List<Transform>();

    private List<GameObject> players = new List<GameObject>();
    private Transform gameplayTransform;
    private Transform pauseTransform;

    private void Start()
    {
        Time.timeScale = 1;
        gameplayTransform = GameObject.Find("Gameplay").transform;
        StartCoroutine(InitializeGame(2));

    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseTransform.GetChild(0).gameObject.activeSelf)
            {
                pauseTransform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseTransform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }

    }

    private IEnumerator InitializeGame(int id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        for(int i = 0; i < playerCount; i++)
        {
            GameObject newPlayer = Instantiate(gameData.PlayerPrefab, spawnPositionList[i].position, Quaternion.identity, gameplayTransform);
            newPlayer.GetComponent<PlayerDataScript>().SetPlayerData(gameData.PlayerData[i], spawnPositionList[i].position,  "Player" + (i + 1), (i + 1));
            players.Add(newPlayer);
        }

        pauseTransform = GameObject.Find("PausePanel").transform;
    }



    public void GameOver(GameObject playerToLose)
    {
        players.Remove(playerToLose);
        if (players.Count <= 1)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Transform victoryScreen = GameObject.Find("VictoryPanel").transform;
        victoryScreen.GetChild(0).gameObject.SetActive(true);
        victoryScreen.GetChild(1).gameObject.SetActive(true);
        victoryScreen.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = players[0].GetComponent<PlayerDataScript>().PlayerName + " wins !";
    }
}
