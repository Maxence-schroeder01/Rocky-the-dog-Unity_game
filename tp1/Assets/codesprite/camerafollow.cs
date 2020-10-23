using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public Transform perso;
    public Vector3 offset;
    private void FixedUpdate()
    {
        transform.position = perso.position + offset;
    }

}
