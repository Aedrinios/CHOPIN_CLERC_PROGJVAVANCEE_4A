using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private RaycastHit hit;
    public Transform holdPoint;
    private bool grabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        holdPoint = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Grab"))
        {
            Debug.Log("Attack");
            Attacks();
        }
    }
    void Attacks()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 origin = transform.position;


        if (Physics.Raycast(origin, forward, out hit))
        {
            if (hit.transform.gameObject.tag == "Ball")
            {
               // hit.transform.gameObject.SetActive(false); // Test pour désactiver la boule quand ca touche
                CmdGrabAndThrow();
            }


        }
    }
    public void CmdGrabAndThrow()

    {
        bool grabbed = false;
        if (!grabbed)
        {



            if (hit.collider != null)
            {
                grabbed = true;
            }

            else
            {
                grabbed = false;

                hit.collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(transform.localScale.x, 1f, 0.0f);

            }

        }
        if (grabbed)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                grabbed = false;
                hit.collider.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(transform.localScale.x * 5, 1f, 0f);
            }
            else
            {
                hit.collider.gameObject.transform.position = this.transform.position;
            }
        }
    }
}

