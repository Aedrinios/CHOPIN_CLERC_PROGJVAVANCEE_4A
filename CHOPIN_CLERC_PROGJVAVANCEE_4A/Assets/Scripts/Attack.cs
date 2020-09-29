using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private int _attack = 6;
    private int _defense = 6;
    private int _maximumHealth = 100;
    private int _maxMP = 100;
    private int _luck = 6;
    private int _int = 6;
    private int hitRange = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Attacks()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 origin = transform.position;


     if (Physics.Raycast(origin, forward, out hit))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                Debug.Log("Ennemy is getting hit");
            }
        }
    }
}
