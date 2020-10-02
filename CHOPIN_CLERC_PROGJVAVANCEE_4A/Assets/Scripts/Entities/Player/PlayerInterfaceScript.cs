using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterfaceScript : MonoBehaviour
{
    private PlayerDataScript playerData;
    private GameObject playerUI;
    private TMPro.TextMeshProUGUI playerNameText;
    private TMPro.TextMeshProUGUI playerDamageText;
    private TMPro.TextMeshProUGUI playerLifeText;

    private void Start()
    {
        playerData = GetComponent<PlayerDataScript>();
        playerUI = GameObject.Find("Player" + playerData.PlayerIndex + "UI");
        playerNameText = playerUI.transform.Find("Player" + playerData.PlayerIndex + "Name").GetComponent<TMPro.TextMeshProUGUI>() ;
        playerDamageText = playerUI.transform.Find("Player" + playerData.PlayerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>();
        playerDamageText.text = "0 %";
        playerLifeText = playerUI.transform.Find("Player" + playerData.PlayerIndex + "Life").GetComponent<TMPro.TextMeshProUGUI>();
        playerLifeText.text = "Life : " + GetComponent<PlayerLifeSystem>().LifeRemaining;
    }

    public void RefreshDamageUI(BallControllerScript ballHit)
    {
        playerDamageText.text = GetComponent<PlayerLifeSystem>().CurrentDamageReceived + " %";
    }

    public void RefreshLifeUI()
    {
        playerLifeText.text = "Life : " + GetComponent<PlayerLifeSystem>().LifeRemaining;
    }

    private void OnEnable()
    {
        GetComponent<PlayerLifeSystem>().onPlayerTakeDamage += RefreshDamageUI;
        GetComponent<PlayerLifeSystem>().onPlayerLoseLife += RefreshLifeUI;
    }  
    private void OnDisable()
    {
        GetComponent<PlayerLifeSystem>().onPlayerTakeDamage -= RefreshDamageUI;
        GetComponent<PlayerLifeSystem>().onPlayerLoseLife -= RefreshLifeUI;

    }
}
