using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public void Recommencer_Click()
    {
        SceneManager.LoadScene("Scene1");
    }
}

