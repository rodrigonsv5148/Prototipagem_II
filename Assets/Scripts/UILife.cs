using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UILife : MonoBehaviour
{
    public Slider Hp;
	public Text tipoDeAttack;
    public Text danoDeAttack;
    public Text fase;
    public GameObject battleSystem;

    public void Update()
    {
        if (battleSystem != null) 
        {
            BattleSystem script = battleSystem.GetComponent<BattleSystem>();
            string nomeDoAttack = script.nomeDoAttack;
            tipoDeAttack.text = nomeDoAttack;
            string danoDoAttack = script.danoDoAttack;
            danoDeAttack.text = danoDoAttack;
            string faseDeBatalha = script.fase;
            fase.text = faseDeBatalha;
        }
        
    }
    public void SetHud(Unit unit) 
    {
		Hp.maxValue = unit.maxHP;
		Hp.value = unit.atualHP;
    }

	public void setHP(int hp)
	{
		Hp.value = hp;
	}
}
