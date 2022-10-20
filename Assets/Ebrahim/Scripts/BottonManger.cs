using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BottonManger : MonoBehaviour
{
    // Start is called before the first frame update

    private void StartGame() 
    {
        SceneManager.LoadScene(1);
    }
    private void Exit()
    {
        Application.Quit();
    }
    
}
