using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour
{
    public Slider Hp;

    public void SetHud(unit unit) 
    {
        Hp.maxValue = unit.maxHP;
        Hp.value = unit.atualHP;
    }
}
