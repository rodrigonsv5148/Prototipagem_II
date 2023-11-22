using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int dodgeBase;
    public int damageBase;

    public int maxHP;
    public int atualHP;
    public int maxEnergy;
    public int atualEnergy;
    public int energyperturn;
    public int energizeActive;

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
