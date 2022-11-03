using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Mur : MonoBehaviour
{
    public GameObject redTargetObject;
    public GameObject purpleTargetObject;
    public GameObject goldTargetObject;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Target").Length == 0)
        {
            redTargetObject.transform.position = new Vector3(-1.3f, 3f, 0f);
            Instantiate(redTargetObject);
            //instantiateTargetOnWall("red");
            //instantiateTargetOnWall("purple");
            //instantiateTargetOnWall("gold");
        }
    }

   

    // Update is called once per frame
    void Update()
    {
    }
}
