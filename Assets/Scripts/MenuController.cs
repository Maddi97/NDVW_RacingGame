using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    public void ButtonStart(){
        SceneManager.LoadScene(2);
    }
    public void ButtonDemo(){
        SceneManager.LoadScene(1);
    }
    public void ButtonQuit(){
        Application.Quit();
        System.Diagnostics.Debug.WriteLine("This is a log");
    } 
}
