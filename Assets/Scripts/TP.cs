using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    [SerializeField]private string scene;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene);
        }
        
    }
}
