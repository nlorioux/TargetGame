using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class Balle : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 mousePositionStart;
    private Vector3 mousePositionEnd;
    private Vector3 strength = new Vector3(0f, 0f, 0f);
    private Vector3 balleStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        mousePositionStart = new Vector3(0f, 0f, 0f);
        mousePositionEnd = new Vector3(0f, 0f, 0f);
        rb = GetComponent<Rigidbody>();
        balleStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetMouseButtonUp(0)){
            mousePositionEnd = Input.mousePosition;
            strength.x = -(float)0.015*(mousePositionStart.y - mousePositionEnd.y);
            strength.y = (float)0.015 * (mousePositionStart.y - mousePositionEnd.y);
            strength.z = (float)0.015 * (mousePositionStart.x - mousePositionEnd.x);
            mousePositionStart = new Vector3(0f, 0f, 0f);
            rb.useGravity = true;
            rb.AddForce(strength, ForceMode.Impulse);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            mousePositionStart = Input.mousePosition;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        transform.position = balleStartPosition;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }
    /*
    void Explode()
    {
        var exp = ps.GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(GameObject.Find("Capsule"), exp.duration);
    }*/


}
