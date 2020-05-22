using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class platformMove : MonoBehaviour
{
    
    public float HighBound; //top bound
    public float LowBound; //bot bound
    public string action; //axis to move
    public float speed; //movement speed
    public float direction; //1 for up, right or front, -1 for down, left or back

    public Dropdown actions;


    private Vector3 startPos; //initial position
    private Vector3 newPos; //position to go to


    // Start is called before the first frame update
    void Start()
    {
        this.startPos = this.transform.position;
      
       
    }

    // Update is called once per frame
    void Update()
    {

		//move on an axis based on action given
        if (action == "moveX")
        {
            if (this.transform.position.x <= startPos.x - LowBound)//reached bound, change direction
                direction = 1; 
            else if (this.transform.position.x >= startPos.x + HighBound) //reached bound, change direction
                direction = -1;
			//set new Position based on direction and axis
            newPos = new Vector3(this.transform.position.x + speed * direction, this.transform.position.y, this.transform.position.z);
        }
        else if(action == "moveY")
        {
            if (this.transform.position.y <= startPos.y - LowBound)//reached bound, change direction
                direction = 1;
            else if (this.transform.position.y >= startPos.y + HighBound)//reached bound, change direction
                direction = -1;
			//set new Position based on direction and axis
            newPos = new Vector3(this.transform.position.x, this.transform.position.y + speed * direction , this.transform.position.z);
        }
        else if(action == "moveZ")
        {
            if (this.transform.position.z <= startPos.z - LowBound)//reached bound, change direction
                direction = 1;
            else if (this.transform.position.z >= startPos.z + HighBound)//reached bound, change direction
                direction = -1;
			//set new Position based on direction and axis
            newPos = new Vector3(this.transform.position.x, this.transform.position.y , this.transform.position.z + speed * direction);
        }

		//move
        this.transform.position = newPos;


    }
}

