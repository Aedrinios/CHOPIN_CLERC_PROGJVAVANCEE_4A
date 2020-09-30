using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName  = "GameData", menuName = "Game Data", order = 0)]
public class GameData : ScriptableObject
{
    [SerializeField]
    private GameObject playerPrefab;
    public GameObject PlayerPrefab
    {
        get { return playerPrefab; }
    }

    [Space(10)]

    [SerializeField]
    private List<PlayerData> inputs = new List<PlayerData>();
    public List<PlayerData> Inputs
    {
        get { return inputs; }
    }
}
