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

    private void Awake()
    {
        gameplayTransform = GameObject.Find("Gameplay").transform;
        StartCoroutine(InitializeGame(2));
    }

    private IEnumerator InitializeGame(int id)
    {
        float chrono = 0;

        while (chrono <= 0.25f)
        {
            chrono += Time.deltaTime;
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        for(int i = 0; i < playerCount; i++)
        {
            GameObject newPlayer = Instantiate(gameData.PlayerPrefab, spawnPositionList[i].position, Quaternion.identity, gameplayTransform);
            newPlayer.GetComponent<PlayerControllerScript>().InitializePlayer(gameData.Inputs[i], "Player" + (i + 1), "Player" + (i + 1)    );
            newPlayer.GetComponent<DeadZone>().ZoneStart = spawnPositionList[i];
            newPlayer.transform.GetChild(0).GetComponent<MeshRenderer>().material = gameData.Inputs[i].playerMaterial;
            players.Add(newPlayer);
        }
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
        victoryScreen.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = players[0].GetComponent<PlayerControllerScript>().PlayerName + " wins !";
        
        
    }
}
