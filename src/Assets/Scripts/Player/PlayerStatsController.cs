using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameManager gameManager;
    public LayerMask collisionLayer;
    public int delay = 1;
    public bool isSliding = false;
    public PlayerIndexController faces;
    public Transform movePoint;

    void Start(){
        faces = gameObject.GetComponent<PlayerIndexController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.currentPlayerSpawnpoint;

        movePoint.parent = null;
    }
}
