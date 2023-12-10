using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour {
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    public State state;
    public Direction direction;

    float inputHAxis;
    float runningSpeed = 3.0f;

    public enum State
    {
        Idle,
        Running,
        Jumping,
    }

    public enum Direction
    {
        Left,
        Right
    }

    private void Awake()
    {
		rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Idle;
        SetDirection(Direction.Right);
    }

    // Use this for initialization
    void Start ()
    {

	}
	
    // set direction and make sure the sprite faces the correct way
    void SetDirection(Direction dir)
    {
        direction = dir;

        float scaleX = transform.localScale.x;
        if (dir == Direction.Left)
        {
            scaleX = Mathf.Abs(scaleX) * -1;
        }
        else
        {
            scaleX = Mathf.Abs(scaleX);
        }
        transform.localScale = new Vector2(scaleX, transform.localScale.y);
    }

    void IdleState()
    {
        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);

        float haxis = Input.GetAxis("Horizontal");
		if (haxis != 0)
        {
            state = State.Running;
        }
    }

    void RunningState()
    {
        Vector2 velocity = rigidBody.velocity;
        if (inputHAxis == 0)
        {
            state = State.Idle;
            velocity.x = 0;
        }
        else if (inputHAxis < 0)
        {
            SetDirection(Direction.Left);
            velocity.x = -runningSpeed;
        }
        else if (inputHAxis > 0)
        {
            SetDirection(Direction.Right);
            velocity.x = runningSpeed;
        }
        rigidBody.velocity = velocity;
    }

    void JumpingState()
    {
        Vector2 velocity = rigidBody.velocity;
        if (inputHAxis < 0)
        {
            SetDirection(Direction.Left);
            velocity.x = -runningSpeed;
        }
        else if (inputHAxis > 0)
        {
            SetDirection(Direction.Right);
            velocity.x = runningSpeed;
        }
        rigidBody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            state = State.Idle;
        }
    }

    void Update ()
    {
        inputHAxis = 0;
        if (Input.GetKey("left")) inputHAxis = -1;
        if (Input.GetKey("right")) inputHAxis = 1;
        if (state == State.Idle || state == State.Running)
        {
            if (Input.GetKeyDown("space"))
            {
                Vector2 velocity = rigidBody.velocity;
                velocity.y = 7;
                rigidBody.velocity = velocity;
                state = State.Jumping;
            }
        }

        if (state == State.Idle)
        {
            IdleState();
        }
        else if (state == State.Running)
        {
            RunningState();
        }
        else if (state == State.Jumping)
        {
            JumpingState();
        }
	}
}
