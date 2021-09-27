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
        if (Input.GetButtonDown("Jump") && !GameManager.Instance.gameOver) {
            jump();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if ((other.gameObject.CompareTag("Fence") || other.gameObject.CompareTag("Ground")) && !GameManager.Instance.gameOver) {
            if(!GameManager.Instance.gameOver)
                jump();

            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Normal_Fence") && !GameManager.Instance.gameOver) {
            if(other.transform.parent.gameObject.name == "Fence(Clone)")
                GameManager.Instance.updateScore(1);
            else
                GameManager.Instance.updateScore(3);
        }
    }

    private void jump() {
        playerRb.velocity = transform.position;
        playerRb.AddForce(Vector3.up * forceToAdd, ForceMode.Impulse);
    }
}
