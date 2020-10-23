using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Recommencer_Click()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void quit()
    {
        Application.Quit();
    }
    public void credit()
    {
        SceneManager.LoadScene("Credit");
    }
    public void retour()
    {
        SceneManager.LoadScene("Main");
    }


}
