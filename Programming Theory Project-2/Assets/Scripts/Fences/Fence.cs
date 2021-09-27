using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {

    public float speed;

    private float outOfBounds = -14.5f;
    
    void Update() {
        if(!GameManager.Instance.gameOver)
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        
        if(transform.position.x < outOfBounds)
            Destroy(gameObject);
    }

    public virtual int ScoreToAdd() {
        int score=0;
        return score;
    }
}
