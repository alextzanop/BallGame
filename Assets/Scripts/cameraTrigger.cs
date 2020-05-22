using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************
*
* UNUSED SCRIPT. IGNORE
*
*********************************************************************/

public class cameraTrigger : MonoBehaviour
{

    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            //cameraFollow.rotateCam(cam, new Vector3(25.5f, 90, 0));
            //cam.GetComponent<cameraFollow>().transOffset = new Vector3(-10, 5, 0);
            
        }
    }


}
