using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {

    public float speed;

    private float posX = -14.5f;
    
    void Update() {
        if(!GameManager.Instance.gameOver)
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        
        if(transform.position.x < posX)
            Destroy(gameObject);
    }
}
