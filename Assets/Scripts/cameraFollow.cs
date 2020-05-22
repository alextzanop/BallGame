using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public GameObject player;
    public Vector3 transOffset;
    public Vector3 rotOffset;

    

    // Start is called before the first frame update
    void Start()
    {
        float rot = Mathf.Lerp(this.transform.rotation.x, rotOffset.x, Time.deltaTime * 0.5f);
        this.transform.rotation = Quaternion.Euler(rot*100, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {


        if (player)
        {
            //follow player
            this.transform.position = player.transform.position + transOffset;
            
        }

    }

	//rotates the camera 
    public static void rotateCam(GameObject cam, Vector3 offset, bool rev=false)
    {
        float rotx = 0, roty = 0, rotz = 0;

        if (!rev)
        {
           rotx = Mathf.Lerp(cam.transform.rotation.x, offset.x, Time.deltaTime * 0.5f);
           roty = Mathf.Lerp(cam.transform.rotation.y, offset.y, Time.deltaTime * 0.5f);
           rotz = Mathf.Lerp(cam.transform.rotation.z, offset.z, Time.deltaTime * 0.5f);
        }
        else
        {
            rotx = Mathf.InverseLerp(cam.transform.rotation.x, offset.x, Time.deltaTime * 0.5f);
            roty = Mathf.InverseLerp(cam.transform.rotation.y, offset.y, Time.deltaTime * 0.5f);
            rotz = Mathf.InverseLerp(cam.transform.rotation.z, offset.z, Time.deltaTime * 0.5f);

        }
        cam.transform.rotation = Quaternion.Euler(rotx*100, roty*100, rotz*100);
        
    }

  
}


