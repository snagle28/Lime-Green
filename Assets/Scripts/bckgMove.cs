using NUnit.Framework;
using UnityEngine;
//code only lines if i scale the viewport properly??
public class bckgMove : MonoBehaviour
{
    //connect to the global trigger so we can access the camReady variable (naming outdated, should be bckgReady)
    public float speed = .05f;
    
    private bool _AllAligned;

    private SpriteRenderer myRenderer;
    private float myWidth;
    bool moved = false;
    private float startingPos;
    private int sceneNumber;

    
    // public List<GameObject> scene1Trigger = new List<GameObject>();
    // public List<GameObject> scene2Trigger = new List<GameObject>();
    //variables from original global trigger script
    public GameObject bigMask;
    public GameObject midMask;
    public GameObject lilMask;

    public bool camReady = false;
    private int timer = 0;
    private bool _check1Align;
    private bool _check2Align;
    private bool _check3Align;
    public bool allAligned;

    void Start()
    {
        print("Access a bubble by pressing 1,2, or 3. Then use WASD to move. ");
        myRenderer = GetComponent<SpriteRenderer>();
        //convert width into an integer because the bck can't move by, say, .5 of a pixel
        myWidth = (myRenderer.bounds.size.x);

        //store a static location variable 
        startingPos = transform.position.x;

        //scene number determines which scene should be on the screen
        sceneNumber = 1;
    }

    void Update()
    {
        //
        print(timer);
        //myRenderer.sprite.bounds.size.x = myWidth;
        //check if ready to move:
        //global trigger 
        _check1Align = bigMask.GetComponent<BigMaskMove>().bigMaskAligned;
        _check2Align = midMask.GetComponent<MidMaskMove>().midMaskAligned;
        _check3Align = lilMask.GetComponent<LilMaskMove>().lilMaskAligned;
        
        if ((_check2Align == true && _check3Align == true && _check1Align == true) || (Input.GetKey(KeyCode.N)))
        {
            print(allAligned);
            allAligned = true;
            timer++;
        }
        
        if (timer >= 500)
        {
            camReady = true;
        }
        
        //now move
        
        //if you mix with vector 2, it snaps it forward/backward and messes with layering!!
        Vector3 currentPos = transform.position;

        if (camReady)
        {
            MoveBkg();
        }
        else //added this in last second to try and reset the variable properly, check on this later
        {
            //print("moved is set to false");
            moved = false;
        }
        

    }

    void MoveBkg()
    {
        
        //increase the scene number one time when the function first runs
        //sceneNumber++;
        Vector3 currentPos = transform.position;

        if (!moved)
        {
            float target = startingPos - sceneNumber * myWidth;
    
            if (transform.position.x > target)
            {
                currentPos.x -= speed * Time.deltaTime;
            }
            else
            {
                ResetGlobalTrigger();
                moved = false;
                sceneNumber++;
            }
        }
        transform.position = currentPos;
    }
    
    void ResetGlobalTrigger()
    {
        camReady = false;
        allAligned = false;
        timer = 0;
        moved = false;
    }
   
}
