using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int damageBase;
    public int maxHP;
    public int atualHP;
    
	public bool takeDamage (int dmg)
	{
		atualHP -= dmg;

		if (atualHP <= 0) {
			return true;
		} else {
			return false;
		}
	}

}
