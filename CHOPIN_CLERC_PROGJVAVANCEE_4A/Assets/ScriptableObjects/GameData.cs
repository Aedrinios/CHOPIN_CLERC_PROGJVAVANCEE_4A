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
    private List<PlayerDataObject> playerData = new List<PlayerDataObject>();
    public List<PlayerDataObject> PlayerData
    {
        get { return playerData; }
    }
}
