﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    NinjaController ninja; // this is my enemy
    Vector2 toEnemy;
    float enemyDistance;

    float maxEnemyDistance = 8;
    float minEnemyDistance = 3;

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

    void Start() { }

    void SetVelocityX(float x)
    {
        Vector2 velocity = rigidBody.velocity;
        velocity.x = x;
        rigidBody.velocity = velocity;
    }

    void Update()
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
    {
        SetVelocityX(0);

        animator.SetInteger("anim", 0);

        if (enemyDistance > maxEnemyDistance)
        {
            _currentState = RabbitState.RunToward;
        }
        else if (enemyDistance < minEnemyDistance)
        {
            _currentState = RabbitState.RunAway;
        }
    }

    private void UpdateRunToward()
    {
        SetVelocityX(runningSpeed * -1);

        animator.SetInteger("anim", 1);

        if (enemyDistance < maxEnemyDistance)
        {
            _currentState = RabbitState.Idle;
        }
    }

    private void UpdateRunAway()
    {
        SetVelocityX(runningSpeed);

        animator.SetInteger("anim", 2);

        if (enemyDistance > minEnemyDistance)
        {
            _currentState = RabbitState.Idle;
        }
    }
}
