using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public Player()
    {
        health = 1.0f;
        xSpeed = 0.0f;
        ySpeed = 0.0f;
        xSpeedMax = 20.0f;
        xDashMultiplier = 2.0f;
        jumpHeight = 500f;
        fastFallSpeed = -35.0f;
        grounded = false;
        flipped = false;
        running = false;
        hasDoubleJump = true;
        fastFalling = false;
    }

    //Getters

    public float getHealth()
    {
        return health;
    }
    public float getXPos()
    {
        return xPos;
    }
    public float getYPos()
    {
        return yPos;
    }
    public float getOldXPos()
    {
        return oldXPos;
    }
    public float getOldYPos()
    {
        return oldYPos;
    }
    public float getXSpeed()
    {
        return xSpeed;
    }
    public float getYSpeed()
    {
        return ySpeed;
    }
     public float getXSpeedMax()
    {
         return xSpeedMax;
    }
    public float getXDashMultiplier()
    {
        return xDashMultiplier;
    }
    public float getJumpHeight()
    {
        return jumpHeight;
    }
    public float getFastFallSpeed()
    {
        return fastFallSpeed;
    }
    public float getRespawnXPos()
    {
        return respawnXPos;
    }
    public float getRespawnYPos()
    {
        return respawnYPos;
    }
    public bool getGrounded()
    {
        return grounded;
    }
    public bool getFlipped()
    {
        return flipped;
    }
    public bool getRunning()
    {
        return running;
    }
    public bool getHasDoubleJump()
    {
        return hasDoubleJump;
    }
    public bool getFastFalling()
    {
        return fastFalling;
    }

    //Setters

    public void setHealth(float theHealth)
    {
        health = theHealth;
    }
    public void setXPos(float theXPos)
    {
        xPos = theXPos;
    }
    public void setYPos(float theYPos)
    {
        yPos = theYPos;
    }
    public void setOldXPos(float theOldXPos)
    {
        oldXPos = theOldXPos;
    }
    public void setOldYPos(float theOldYPos)
    {
        oldXPos = theOldYPos;
    }
    public void setXSpeed(float theXSpeed)
    {
        xSpeed = theXSpeed;
    }
    public void setYSpeed(float theYSpeed)
    {
        ySpeed = theYSpeed;
    }
    public void setXSpeedMax(float theXSpeedMax)
    {
        xSpeedMax = theXSpeedMax;
    }
    public void setXDashMultiplier(float theXDashMultiplier)
    {
        xDashMultiplier = theXDashMultiplier;
    }
    public void setJumpHeight(float theJumpHeight)
    {
        jumpHeight = theJumpHeight;
    }
    public void setFastFallSpeed(float theFastFallSpeed)
    {
        fastFallSpeed = theFastFallSpeed;
    }
    public void setRespawnXPos(float theRespawnXPos)
    {
        respawnXPos = theRespawnXPos;
    }
    public void setRespawnYPos(float theRespawnYPos)
    {
        respawnYPos = theRespawnYPos;
    }
    public void setGrounded(bool theGrounded)
    {
        grounded = theGrounded;
    }
    public void setFlipped(bool theFlipped)
    {
        flipped = theFlipped;
    }
    public void setRunning(bool theRunning)
    {
        running = theRunning;
    }
    public void setHasDoubleJump(bool theHasDoubleJump)
    {
        hasDoubleJump = theHasDoubleJump;
    }
    public void setFastFalling(bool theFastFalling)
    {
        fastFalling = theFastFalling;
    }

    private float health;
    private float xPos;
    private float yPos;
    private float oldXPos;
    private float oldYPos;
    private float xSpeed;
    private float ySpeed;
    private float xSpeedMax;
    private float xDashMultiplier;
    private float jumpHeight;
    private float fastFallSpeed;
    private float respawnXPos;
    private float respawnYPos;
    private bool grounded;
    private bool flipped;
    private bool running;
    private bool hasDoubleJump;
    private bool fastFalling;


    private Animator anim;
    private Rigidbody2D playerRigidbody;

    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    // Use this for initialization
    void Start () {

        playerRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Initial Respawn Point

        setRespawnXPos( transform.position.x );
        setRespawnYPos( transform.position.y );

        // Find Initial X & Y Position

        setOldXPos(getXPos());
        setOldYPos(getYPos());

    }


	
	// Update is called once per frame
	void Update () {

        //Respawn

        if (getHealth() == 0 || Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector2(getRespawnXPos(), getRespawnYPos());
            playerRigidbody.velocity = new Vector2(0, 0);

            setYPos(getRespawnYPos());
            setHealth(1.0f);
        }

        //// Movement

        float horizontal = Input.GetAxis("Horizontal");

        setXSpeed( horizontal*getXSpeedMax() );

        // Sprint Mechanic 

        if (Input.GetKeyDown(KeyCode.Z) && getRunning() == true)
        {
            setXSpeed(getXSpeed() * getXDashMultiplier());

            setXSpeedMax(getXSpeed());

            if (getXSpeed() < 0)
            {
                setXSpeedMax(getXSpeedMax() * -1);
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            setXSpeedMax(20.0f);
        }

        // Jump Mechanic

        if (Input.GetKeyDown(KeyCode.UpArrow) && getGrounded() == true )
        {
            Jump(getGrounded(), getJumpHeight(), getXSpeed());
        }

        // When Grounded Set...

        if (getGrounded() == true)
        {
            setHasDoubleJump(true);
            setFastFalling(false);
        }

        // Double Jump Mechanic

        if (Input.GetKeyDown(KeyCode.UpArrow) && getHasDoubleJump() == true )
        {
            Jump(getGrounded(), getJumpHeight(), getXSpeed());
            setHasDoubleJump(false);
            setFastFalling(false);
        }

        // Fast Fall Mechanic

        if (Input.GetKeyDown(KeyCode.DownArrow) && getGrounded() == false && getFastFalling() == false)
        {
            playerRigidbody.velocity = new Vector2(0, getFastFallSpeed());
            setFastFalling(true);
        }

        // Max Speed

        if (getXSpeed() > getXSpeedMax())
        {
            setXSpeed( getXSpeedMax() );
        }

        if (getXSpeed() < -getXSpeedMax() )
        {
            setXSpeed( -getXSpeedMax() );
        }      

        setYSpeed(playerRigidbody.velocity.y);

        //Move Functions

        Movement( getXSpeed(), getFastFalling() );

        isFlipped(horizontal, getFlipped() );

        setXPos(transform.position.x);
        setYPos(transform.position.y);

        //Check Grounded, Animation Business

        setGrounded(Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround));

        if (getXSpeed() == 0)
        {
            setRunning(false);
        }
        else
        {
            setRunning(true);
        }

        anim.SetBool("Grounded", getGrounded());
        anim.SetBool("Running", getRunning());

        //Find X & Y Position

        setOldXPos(getXPos());
        setOldYPos(getYPos());

        // Fall Off Map

        if (getYPos() < -43.0f)
        {
            setHealth(0.0f);
        }

    }


    //Movement Function

    private void Movement(float getXSpeed, bool getFastFalling)
    {
        if (getFastFalling == false)
        {
            playerRigidbody.velocity = new Vector2(getXSpeed, playerRigidbody.velocity.y);
        }
    }
    
    // Jumps

    private void Jump(bool getGrounded, float getJumpHeight, float getXSpeed)
    {
        if (getGrounded == true)
        //Single Jump
        {
            playerRigidbody.AddForce(new Vector2(0, getJumpHeight));
        }       
        else
        //Double Jump
        {
            playerRigidbody.velocity = new Vector2(getXSpeed, 0);
            playerRigidbody.AddForce(new Vector2(0, getJumpHeight * 2));
        }

    }

    //Flipping Function 

    private void isFlipped(float getXSpeed, bool getFlipped)
    {

        if (getXSpeed > 0 && getFlipped == true || getXSpeed < 0 && getFlipped == false)
        {
            setFlipped( !getFlipped );


            Vector2 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
  
    }
}
