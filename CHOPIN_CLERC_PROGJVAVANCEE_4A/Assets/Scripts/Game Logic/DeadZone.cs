using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OUCH : " + collision.gameObject.name);
        if ((playerMask.value & (1 << collision.gameObject.layer)) > 0)
            collision.gameObject.GetComponent<PlayerLifeSystem>().onPlayerLoseLife?.Invoke();
    }
}
