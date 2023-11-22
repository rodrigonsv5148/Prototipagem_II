using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public enum battleState { start, playerTurn, enemyTurn, won, lost };
public class BattleSystem : MonoBehaviour
{
    public GameObject playerFirePrefab;
    public GameObject playerEarthPrefab;
    public GameObject playerAirPrefab;
    public GameObject playerWaterPrefab;
    public GameObject enemyPrefab;
    public GameObject team;

    public Transform mage1Location;
    public Transform mage2Location;
    public Transform mage3Location;
    public Transform mage4Location;
    public Transform enemyLocation;

    public battleState state;

    Unit playerFireUnit;
    Unit playerEarthUnit;
    Unit playerAirUnit;
    Unit playerWaterUnit;
    Unit enemyUnit;
    Unit teamUnit;

    public UILife playerLife;

	public bool buttonFire = false;
	public bool buttonEarth = false;
	public bool buttonAir = false;
	public bool buttonWater = false;
    
	public int attackUnion = 0;

	public int magia = 10;

	void Start()
    {
        state = battleState.start;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle() 
    {
        GameObject fireMage = Instantiate(playerFirePrefab, mage1Location);
        playerFireUnit = fireMage.GetComponent<Unit>();

        GameObject earthMage = Instantiate(playerEarthPrefab, mage2Location);
        playerEarthUnit = earthMage.GetComponent<Unit>();

        GameObject airMage = Instantiate(playerAirPrefab, mage3Location);
        playerAirUnit = airMage.GetComponent<Unit>();

        GameObject waterMage = Instantiate(playerWaterPrefab, mage4Location);
        playerWaterUnit = waterMage.GetComponent<Unit>();

        GameObject enemy = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemy.GetComponent<Unit>();

        teamUnit = team.GetComponent<Unit>();

        playerLife.SetHud(teamUnit);

		yield return new WaitForSeconds (1f);

		state = battleState.playerTurn;
		playerTurn ();

    }

	IEnumerator PlayerAttack()
	{

		bool morreu = enemyUnit.takeDamage(magia)

		yield return new WaitForSeconds (1f);

		if(morreu)
		{
			
		}else
		{
			
		}

		//resetar as variáveis e os botões da UI
	}

	void playerTurn ()
	{
		
	}

	public void OnAttackButton ()
	{
		if (state != battleState.playerTurn) 
		{
			return;
		}

		StartCoroutine (PlayerAttack ());
	}

	public void OnFireButton()
	{
		if (buttonFire == false) {
			attackUnion = attackUnion + 1;
			buttonFire = true;
		} else 
		{
			attackUnion = attackUnion - 1;
			buttonFire = false;

		}
		print (attackUnion);
	}

	public void OnEarthButton()
	{
		if (buttonEarth == false) {
			attackUnion = attackUnion + 2;
			buttonEarth = true;
		} else 
		{
			attackUnion = attackUnion - 2;
			buttonEarth = false;
		}
	}

	public void OnAirButton()
	{
		if (buttonAir == false) {
			attackUnion = attackUnion + 4;
			buttonAir = true;
		} else 
		{
			attackUnion = attackUnion - 4;
			buttonAir = false;
		}
	}
	public void OnWaterButton()
	{
		if (buttonWater == false) {
			attackUnion = attackUnion + 8;
			buttonWater = true;
		} else 
		{
			attackUnion = attackUnion - 8;
			buttonWater = false;
		}
	}
}
