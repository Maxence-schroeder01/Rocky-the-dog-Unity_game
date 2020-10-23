using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<ennemy>();

        if (player)
        {

        }
        else
        {
            playerManger.boneCount += 1;
            Destroy(gameObject);
        }
    }
}


