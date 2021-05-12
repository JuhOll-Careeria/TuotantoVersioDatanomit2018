using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(Player.position.x + offset.x, Player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}
