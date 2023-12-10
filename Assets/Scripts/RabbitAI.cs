using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    NinjaController ninja; // this is my enemy
    Vector2 toEnemy;
    float enemyDistance;
    
    public float runningSpeed = 2.0f;

    [SerializeField]
    private RabbitState _currentState;

    private enum RabbitState
    {
        Idle,
        RunToward,
        RunAway
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ninja = FindObjectOfType<NinjaController>();
    }
    
    void Start ()
    {
	}

    void SetVelocityX(float x)
    {
        Vector2 velocity = rigidBody.velocity;
        velocity.x = x;
        rigidBody.velocity = velocity;
    }

	void Update ()
    {
        toEnemy = ninja.transform.position - transform.position;
        enemyDistance = Mathf.Abs(toEnemy.x);
        UIManager.Instance.SetDistance(enemyDistance);

        //TODO: Implement Movement via SetVelocityX

        if (_currentState == RabbitState.Idle)
            UpdateIdle();
        else if (_currentState == RabbitState.RunToward)
            UpdateRunToward();
        else if (_currentState == RabbitState.RunAway)
            UpdateRunAway();


	}

    private void UpdateIdle()
    {}

    private void UpdateRunToward()
    {
        // In the RunToward-State the Rabbit should move toward the player
        // Note: You can use the Method SetVelocityX(…) and the variable runningSpeed to set the 
        // movement-Speed of the Rabbit.

        // I guess that instruction means to do this? 
        SetVelocityX(runningSpeed);
    }

    private void UpdateRunAway()
    {}


}
