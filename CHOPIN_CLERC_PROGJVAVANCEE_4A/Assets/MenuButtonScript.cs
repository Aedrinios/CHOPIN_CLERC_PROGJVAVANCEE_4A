using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}