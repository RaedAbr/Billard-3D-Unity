using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameController gameController = gameManager.GetComponent<GameController>();

        gameController.BallInHole(col.gameObject);
    }
}
