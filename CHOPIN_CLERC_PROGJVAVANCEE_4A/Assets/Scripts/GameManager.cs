using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerController> players = new List<PlayerController>();

    private void Awake()
    {
        StartCoroutine(LoadScene(2));
    }

    private IEnumerator LoadScene(int id)
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

        StartGame();
    }

    private void StartGame()
    {
        foreach(PlayerController player in players)
        {
            player.InitializeUI();
        }
    }
}
