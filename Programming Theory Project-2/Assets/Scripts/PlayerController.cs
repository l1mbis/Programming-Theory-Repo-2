using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody playerRb;

    [SerializeField] private float forceToAdd;
    
    private void Start() {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            jump();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Fence") || other.gameObject.CompareTag("Ground")) {
            if(!GameManager.Instance.gameOver)
                jump();
            
            GameManager.Instance.gameOver = true;
        }
    }

    private void jump() {
        playerRb.velocity = transform.position;
        playerRb.AddForce(Vector3.up * forceToAdd, ForceMode.Impulse);
    }
}
