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
    
    public float runningSpeed = 2.0f;

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
	}
}
