using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;

    [Header("Offsets")]
    public float maxX;          // Posição max até onde vai a fase
    public float offSetX;       // Posição em X
    public float offSetY;     // Posição em Y 

    [Header("Locks")]
    [HideInInspector] public bool lockY = false;

    private void LateUpdate()
    {
        if (transform.position.x < maxX)
        {
            if (lockY)
                transform.position = new Vector3(player.position.x + offSetX, offSetY, -10);
            else
                transform.position = new Vector3(player.position.x + offSetX, player.position.y + offSetY, -10);
        }

        // Se quiser mover em y é só adicionar um offSet em y
    }
}