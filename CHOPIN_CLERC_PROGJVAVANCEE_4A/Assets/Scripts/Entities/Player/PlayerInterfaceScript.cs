using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterfaceScript : MonoBehaviour
{
    private PlayerDataScript playerData;
    private GameObject playerUI;
    private TMPro.TextMeshProUGUI playerNameText;
    private TMPro.TextMeshProUGUI playerDamageText;

    private void Start()
    {
        playerData = GetComponent<PlayerDataScript>();
        playerUI = GameObject.Find("Player" + playerData.PlayerIndex + "UI");
        playerNameText = playerUI.transform.Find("Player" + playerData.PlayerIndex + "Name").GetComponent<TMPro.TextMeshProUGUI>() ;
        playerDamageText = playerUI.transform.Find("Player" + playerData.PlayerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>();
        playerDamageText.text = "0 %";
    }

    public void RefreshDamageUI(BallControllerScript ballHit)
    {
        Debug.Log("Order :" + this.name);
        playerDamageText.text = ballHit.Speed + " %";   
    }

    private void OnEnable()
    {
        GetComponent<PlayerLifeSystem>().onPlayerTakeDamage += RefreshDamageUI;
    }  
    private void OnDisable()
    {
        GetComponent<PlayerLifeSystem>().onPlayerTakeDamage -= RefreshDamageUI;
    }
}
