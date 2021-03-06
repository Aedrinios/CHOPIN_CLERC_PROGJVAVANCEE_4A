﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour
{
    [SerializeField]
    private float maxDamageReceived;
    [SerializeField]
    private int lifeRemaining;
    public int LifeRemaining {  get { return lifeRemaining; } }

    private float currentDamageReceived;
    public float CurrentDamageReceived { get { return currentDamageReceived; } }
    

    public delegate void OnPlayerTakeDamage(BallControllerScript ballHit);
    public OnPlayerTakeDamage onPlayerTakeDamage;

    public delegate void OnPlayerLoseLife();
    public OnPlayerLoseLife onPlayerLoseLife;

    public delegate void OnPlayerDie();
    public OnPlayerDie onPlayerDie;

    public void LoseOneLife()
    {
        if (lifeRemaining >= 1)
        {
            lifeRemaining--;
            currentDamageReceived = 0;
        }

        if (lifeRemaining == 0)
        {
            onPlayerLoseLife -= LoseOneLife;
            onPlayerDie?.Invoke();

        }
    }

    public void TakeDamage(BallControllerScript ballHit)
    {
        currentDamageReceived += Mathf.Clamp(ballHit.Speed, 0, maxDamageReceived);
        
    }

    public IEnumerator UnlistenTakeDamage(float timer)
    {
        while (timer > 0)
        {
            onPlayerTakeDamage -= TakeDamage;
            timer -= Time.deltaTime;
            yield return null;
        }
        onPlayerTakeDamage += TakeDamage;
    }
    public void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameOver(gameObject);
    }

    public void RaiseOnPlayerDamageEvent(BallControllerScript ballHit)
    {
        onPlayerTakeDamage?.Invoke(ballHit);
    }

    private void OnEnable()
    {
        onPlayerLoseLife += LoseOneLife;
        
        onPlayerTakeDamage += TakeDamage;
        
        onPlayerDie += Die;
    }

    private void OnDisable()
    {
        onPlayerLoseLife -= LoseOneLife;
        onPlayerTakeDamage -= TakeDamage;
       
        onPlayerDie -= Die;
    }
}
