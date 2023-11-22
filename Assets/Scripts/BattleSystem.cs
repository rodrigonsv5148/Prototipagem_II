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
    public UILife enemyLife;

    public bool buttonFire = false;
	public bool buttonEarth = false;
	public bool buttonAir = false;
	public bool buttonWater = false;
    
	private int attackUnion = 0;

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
		enemyLife.SetHud(enemyUnit);

		yield return new WaitForSeconds (1f);

		state = battleState.playerTurn;
		PlayerTurn();
    }

	IEnumerator PlayerAttack()
	{

		bool morreu = enemyUnit.takeDamage (magia);

		yield return new WaitForSeconds (1f);

        /*setar o HP do inimigo*/
		enemyLife.SetHP(enemyUnit.atualHP);

        if (morreu)
		{
			state = battleState.won;
			EndBattle ();
		}else
		{
			state = battleState.enemyTurn;
			StartCoroutine (EnemyTurn());
		}

		//resetar as variáveis e os botões da UI
	}

	IEnumerator EnemyTurn() 
	{
		yield return new WaitForSeconds(1f);

		bool playerMorreu = teamUnit.takeDamage(enemyUnit.damageBase);

        playerLife.setHP(teamUnit.atualHP);

        yield return new WaitForSeconds(1f);

		//aqui embaixo q eu posso botar para gerar um numero aleatório baseado em quantos focaram a magia para realizar o ataque

		if (playerMorreu) 
		{
			state = battleState.lost;
			EndBattle();

        }
		else 
		{
            state = battleState.playerTurn;
			EndBattle();
        }

    }

	void EndBattle()
	{
		if (state == battleState.won) {
			//O que acontece se ganhar?
			print ("ganhei");
		} else 
		{
			print ("no ceu tem pao?");
		}
	}

	void PlayerTurn ()
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
