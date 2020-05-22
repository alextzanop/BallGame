using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformRotate : MonoBehaviour
{

    public float rotSpeed; //rotation speed
    public string rotAxis; //axis on which the platform will rotate
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//rotate platform on given axis
        if(rotAxis == "x") 
            this.transform.Rotate(new Vector3(rotSpeed, 0, 0));
        if(rotAxis == "y") 
            this.transform.Rotate(new Vector3(0, rotSpeed, 0));
        if(rotAxis == "z") 
            this.transform.Rotate(new Vector3(0, 0, rotSpeed));
    }
}
