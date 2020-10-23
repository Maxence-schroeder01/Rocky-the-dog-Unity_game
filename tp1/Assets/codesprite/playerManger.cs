using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManger : MonoBehaviour
{
    public static int boneCount = 0;

    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        text.text = boneCount.ToString();
    }
}
