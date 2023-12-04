using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkill : MonoBehaviour
{
    float larguraTela;
    public bool cima = false;
    public bool baixo = false;
    public bool esquerda = false;
    public bool direita = false;

    SpriteRenderer grafico;

    void Start()
    {
        grafico = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (cima) 
        {
            float offset = Time.time * 0.2f;

            grafico = GetComponent<SpriteRenderer>();

            grafico.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
        else if (baixo)
        {
            float offset = Time.time * 0.2f;

            grafico = GetComponent<SpriteRenderer>();

            grafico.material.SetTextureOffset("_MainTex", new Vector2(0, -1 * offset));
        }
        else if (esquerda) 
        {
            float offset = Time.time * 0.2f;

            grafico = GetComponent<SpriteRenderer>();

            grafico.material.SetTextureOffset("_MainTex", new Vector2(-1 * offset, 0));
        }
        else if (direita) 
        {
            float offset = Time.time * 0.2f;

            grafico = GetComponent<SpriteRenderer>();

            grafico.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}
