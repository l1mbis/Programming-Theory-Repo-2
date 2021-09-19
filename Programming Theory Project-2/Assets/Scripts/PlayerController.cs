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
            playerRb.velocity = transform.position;
            playerRb.AddRelativeForce(Vector3.up * forceToAdd, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("France") || other.gameObject.CompareTag("Ground")) {
            GameManager.Instance.gameOver = true;
        }
    }
}
