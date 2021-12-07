using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndMenu : MonoBehaviour
{
    public void ButtonMenu(){
        SceneManager.LoadScene(0);
    }
    public void ButtonStartAgain(){
        SceneManager.LoadScene(2);
    }
}