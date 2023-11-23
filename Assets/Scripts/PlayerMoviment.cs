using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 5f; // Velocidade de movimento do jogador

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obt�m entrada do teclado para movimento
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcula a dire��o do movimento
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f) * moveSpeed * Time.deltaTime;

        // Atualiza a posi��o do jogador
        transform.position += movement;
    }
}
