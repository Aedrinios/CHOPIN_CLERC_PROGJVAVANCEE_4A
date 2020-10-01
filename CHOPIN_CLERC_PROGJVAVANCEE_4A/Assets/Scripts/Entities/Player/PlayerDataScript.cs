using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    private string playerName;
    public string PlayerName { get { return playerName; } }

    private int playerIndex;
    public int PlayerIndex { get { return playerIndex; } }

    private Vector3 playerSpawner;
    public Vector3 PlayerSpawner { get { return playerSpawner; } }


    private string horizontalAxis, verticalAxis;
    public string HorizontalAxis { get { return horizontalAxis; } }
    public string VerticalAxis { get { return verticalAxis; } }

    private KeyCode jumpInput, hitBallInput;
    public KeyCode JumpInput { get { return jumpInput; } }
    public KeyCode HitBallInput { get { return hitBallInput; } }

    private bool isControlledByPlayer;
    public bool IsControlledByPlayer
    {
        get { return isControlledByPlayer; }
    }


    public void SetPlayerData(PlayerDataObject data, Vector3 spawner, string name, int index)
    {
        playerName = name;
        playerIndex = index;
        playerSpawner = spawner;
        horizontalAxis = data.horizontalAxis;
        verticalAxis = data.verticalAxis;
        jumpInput = data.jumpInput;
        hitBallInput = data.hitBallInput;
        isControlledByPlayer = data.isPlayer;
    }
}
