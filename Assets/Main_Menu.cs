using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void Playgame() 
    {
        SceneManager.LoadScene(1);
    }

    public void QutiGame()
    {
        Application.Quit();
    }
}
