using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballMovement : MonoBehaviour
{
    public GameObject cam;
    public GameObject canvasHealth;
    public GameObject block; //confetti block
    public float moveSpeed;
    public float jumpForce; 
    public float windUpTime; //windup time for jump
    public float confetti; //how many confetti to generate

    //Explosion on the end 
    public float ExplosionForce = 100.0f;
    public float ExplosionRadius = 5.0f;
    public float upwardsMod = 0.0f;



    private Vector3 startPos; //spawnpoint
    private float startTime; //time when initiated jump
    private Rigidbody rb; //ball's rigidbody
    private Vector3 movement;
    private int jumps; //jumps available for double jump etc
    private int lives; 
    private bool end;
    private bool camChange; //becomes true when reching a point on the map to change the camera orientation
    private GameObject tr1 = null; //first trigger point
    private GameObject tr2 = null; //second trigger point
    private bool checkpoint = false; //true if has reached checkpoint, false otherwise
    private bool infLives = false; //for testing purposes

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        jumps = 2;
        lives = 3;
        end = false;
        rb = this.GetComponent<Rigidbody>();
        camChange = false; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!infLives)
            updateLives();

        if (!camChange)
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        else//changes camera orientation
            movement = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Z)) //for testing purposes
        {
            infLives = !infLives;
        }

        if (Input.GetButtonDown("Jump"))
        {
           
            startTime = Time.time;
        }


        if (Input.GetButtonUp("Jump") && jumps>0 && !end)
        {
            soundPlayer.PlaySound("jump");
            movement += new Vector3(0, jumpForce, 0);
            if (Time.time - startTime < windUpTime)
                movement += new Vector3(0, (Time.time - startTime)*jumpForce, 0); //jump depending on space press time

            jumps--;
        }

        if(!end)
            rb.AddForce(movement * moveSpeed);
        
        if (this.transform.position.y < -5)
        {
            resetPlayer();
        }


        //confetti explosion at the endpoint
        foreach(Collider col in Physics.OverlapSphere(this.transform.position, ExplosionRadius))
        {
            if (col.gameObject.tag == "confetti" && col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, this.transform.position, ExplosionRadius, upwardsMod, ForceMode.Force);
            }
        }

    }


    void OnCollisionEnter(Collision other)
    {
        jumps = 2;
        //reset ball
        if (other.gameObject.tag == "ground" && !end)
        {
            lives--;
            if (lives != 0)
            {
                
                resetPlayer();
               
            }
            
            if (camChange)
            {
                
                cam.transform.rotation = Quaternion.Euler(25.5f, 0, 0);
                cam.GetComponent<cameraFollow>().transOffset = new Vector3(0, 5, -10);
                camChange = false;
            }

            soundPlayer.PlaySound("death");

        }
        
         //reached checkpoint/ max lifebar(one time use)
         else if (other.gameObject.tag == "checkPoint")
        {
            
            if (!checkpoint)
            {

                if (lives < 3)
                    lives = 3;
                startPos = other.gameObject.transform.position + new Vector3(0, 1, 0); //set new spawnpoint
                checkpoint = true;
            }
        }

    }

	//reduce jumps when off ground
    void OnCollisionExit(Collision other)
    {
        if(jumps == 2) {
           
            jumps = 1;
        }
            

    }

   
    void OnTriggerEnter(Collider other)
    {
		//initiate victory explosion
        if (other.gameObject.tag == "win" && !end)
        {
            for (int i = 0; i < confetti; i++)
            {
                GameObject temp = (GameObject)Instantiate(block, other.transform.position, Quaternion.identity);
                Material tempMat = (Material)Instantiate(Resources.Load("confettiMat"), other.transform.position, Quaternion.identity);
                temp.GetComponent<Renderer>().material = tempMat;
                temp.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
            }
            end = true;
            soundPlayer.PlaySound("win");
            Destroy(this.gameObject); //destroy ball

        }

		//first trigger point to change camera 
        else if (other.gameObject.tag == "trigger1")
        {
            cameraFollow.rotateCam(cam, new Vector3(25.5f, 90, 0));
            cam.GetComponent<cameraFollow>().transOffset = new Vector3(-10, 5, 0);
            this.camChange = true;
            tr1 = other.gameObject;
            other.gameObject.SetActive(false);

        }
 
		//second trigger point to change camera
        else if (other.gameObject.tag == "trigger2")
        {
            cam.transform.rotation = Quaternion.Euler(25.5f, 0, 0);
            cam.GetComponent<cameraFollow>().transOffset = new Vector3(0, 5, -10);
            this.camChange = false;
            tr2 = other.gameObject;
            other.gameObject.SetActive(false);
        }

        
       


    }

    //set player back to spawn point
    void resetPlayer()
    {
        this.transform.position = startPos;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        rb.transform.rotation = new Quaternion(0, 0, 0, 0);
        if (tr1 && !checkpoint)
            tr1.SetActive(true);
        if (tr2)
            tr2.SetActive(true);
    }

    //update health bar
    void updateLives()
    {
        if (lives == 3)
        {
            canvasHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 100);
            canvasHealth.GetComponent<RawImage>().uvRect = new Rect(0, 0, 3, 1);
        }

        if (lives == 2)
        {
            canvasHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            canvasHealth.GetComponent<RawImage>().uvRect = new Rect(0, 0, 2, 1);
        }
        else if (lives == 1)
        {
            canvasHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 100);
            canvasHealth.GetComponent<RawImage>().uvRect = new Rect(0, 0, 1, 1);
        }

        else if (lives == 0)
        {
            end = true;
            canvasHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 100);
            canvasHealth.GetComponent<RawImage>().uvRect = new Rect(0, 0, 0, 1);
        }
    }
}
