using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]private string scene;
    [SerializeField]private GameObject menuP;
    [SerializeField]private GameObject menuI;

    public void loadScene() 
    {
        SceneManager.LoadScene(scene);
    }

    public void instructions()
    {
        menuP.SetActive(false);
        menuI.SetActive(true);
    }

    public void menuPrincipal()
    {
        menuP.SetActive(true);
        menuI.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene);
        }
        
    }
}
