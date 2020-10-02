using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour
{
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
        currentDamageReceived += ballHit.Speed;
        
    }

    public IEnumerator UnlistenTakeDamage(float timer)
    {
        Debug.Log("unlisten");
        while (timer > 0)
        {
            Debug.Log("while : " + timer);
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
