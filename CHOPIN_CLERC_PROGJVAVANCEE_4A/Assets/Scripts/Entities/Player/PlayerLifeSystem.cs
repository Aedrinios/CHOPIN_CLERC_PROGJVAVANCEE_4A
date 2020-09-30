using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour
{
    [SerializeField]
    private int lifeRemaining;

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
            lifeRemaining--;

        if (lifeRemaining == 0)
        {
            onPlayerLoseLife -= LoseOneLife;
            onPlayerDie?.Invoke();

        }
        //  GameManager.Instance.GameOver(gameObject);

    }

    public void TakeDamage(BallControllerScript ballHit)
    {
        Debug.Log("Order :" + this.name);
        currentDamageReceived += ballHit.Speed;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void RaiseOnPlayerDamageEvent(BallControllerScript ballHit)
    {
        onPlayerTakeDamage?.Invoke(ballHit);
    }

    private void OnEnable()
    {
        onPlayerTakeDamage += TakeDamage;
        onPlayerLoseLife += LoseOneLife;
        onPlayerDie += Die;
    }

    private void OnDisable()
    {
        onPlayerTakeDamage -= TakeDamage;
        onPlayerLoseLife -= LoseOneLife;
        onPlayerDie -= Die;
    }
}
