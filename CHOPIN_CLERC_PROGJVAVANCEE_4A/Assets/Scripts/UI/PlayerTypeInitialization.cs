using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTypeInitialization : MonoBehaviour
{
    [SerializeField]
    private PlayerDataObject playerData;
    private Toggle togglePlayerInitialization;

    private void Start()
    {
        togglePlayerInitialization = GetComponent<Toggle>();
    }

    public void SetPlayerType()
    {
        playerData.isPlayer = togglePlayerInitialization.isOn;
    }
}
