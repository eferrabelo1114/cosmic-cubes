using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameManager gameManager;
    public LayerMask collisionLayer;
    public float delay = .3f;
    public bool isSliding = false;
    public PlayerIndexController faces;
    public Transform movePoint;
    public Animator anim;

    void Start(){
        faces = gameObject.GetComponent<PlayerIndexController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = gameManager.currentPlayerSpawnpoint;

        movePoint.parent = null;
    }
}
