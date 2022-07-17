using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private Transform player;

    private Vector3 adjustCameraX = new Vector3(0.2f, 0, 0);
    private Vector3 adjustCameraY = new Vector3(0, 0.2f, 0);
    private int speed = 9;
    private Vector3 targPos = new Vector3(0, 0, 0);

    private int maxX = 550;
    private int minX = 250;

    private int maxY = 412;
    private int minY = 187;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Dice").transform;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 currentPos = gameObject.transform.position;
        Vector3 screenPos = cam.WorldToScreenPoint(player.position);

        if (targPos != new Vector3(0, 0, 0)) {
            gameObject.transform.position = Vector3.Lerp(currentPos, targPos, speed * Time.deltaTime);
        }

        if (screenPos.x <= minX) {
            targPos = currentPos -= adjustCameraX;
        } else if(screenPos.x >= maxX) {
            targPos = currentPos += adjustCameraX;
        }

        if (screenPos.y <= minY) {
            targPos = currentPos -= adjustCameraY;
        } else if(screenPos.y >= maxY) {
            targPos = currentPos += adjustCameraY;
        }
    }
}
