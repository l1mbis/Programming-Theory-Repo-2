using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {

    public float speed;
    
    void Update() {
        if(!GameManager.Instance.gameOver)
            transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
