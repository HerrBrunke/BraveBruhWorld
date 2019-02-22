using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float jumpForceMultiplier = .05f;
    float startingJumpForce = 6f;
    float maxJumpForce = 12f;
    public float jumpCounter = 0;
    public float maxJumpCounter = 4;
    bool rightPressed;
    bool leftPressed;
    bool isRunning;
    public float jumpForce;
    public float playerSpeed = 3f;
    public float runSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //move ;)
        Move();


        //shoot/use
        if (Input.GetKey(KeyCode.RightShift))
        {
            Debug.Log("Bang!");
        }
        //switching weapon(inventory slot)
    }

    //CollisionDetection 
    //1 : jumping
    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag == "Ground")
        {
            jumpCounter = 0;
        }
    }

    //Methods
    void Move()
    {
        #region

        //isrunning (shift)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            if (isRunning)
            {
                transform.position += Vector3.right * runSpeed * Time.deltaTime;
            }
            else if (!isRunning)
            {
                transform.position += Vector3.right * playerSpeed * Time.deltaTime;
                rightPressed = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightPressed = false;
        }
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            if (isRunning)
            {
                transform.position += Vector3.left * runSpeed * Time.deltaTime;
            }
            else if (!isRunning)
            {
                transform.position += Vector3.left * playerSpeed * Time.deltaTime;
                leftPressed = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            leftPressed = false;
        }
        //jump
        //load jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpForce = startingJumpForce;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpForce < maxJumpForce)
            {
                jumpForce += jumpForceMultiplier;
                Debug.LogFormat("jumpForce:{0} | maxJF:{1} | jFmulti:{2}", jumpForce, maxJumpForce, jumpForceMultiplier);
            }
        }

        #endregion
        #region
        //if rightPressed jump right, left left, nothing up
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpCounter++;
            Debug.LogFormat("jumpForce:{0} | JumpCounter:{1} | maxJumCounter:{2} | rPressed:{3} | lPressed:{4}", jumpForce, jumpCounter, maxJumpCounter, rightPressed, leftPressed);
            if (rightPressed && jumpCounter < maxJumpCounter)
            {
                Debug.Log("RechtsSprung");
                rb.AddForce(new Vector3(1, jumpForce, 0), ForceMode.Impulse);
            }
            else if (leftPressed && jumpCounter < maxJumpCounter)
            {
                Debug.Log("LinksSprung");
                rb.AddForce(new Vector3(-1, jumpForce, 0), ForceMode.Impulse);
            }
            else if (jumpCounter < maxJumpCounter)
            {
                Debug.Log("Sprung");
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }

        }
        #endregion
    }//ENDofMOVE

    //END of CLASS
}

