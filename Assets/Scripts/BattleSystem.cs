using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.UI;

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

	public Toggle fireButton;
	public Toggle earthButton;
	public Toggle airButton;
	public Toggle waterButton;

	Vector3 position = new Vector3 (0f, 0.8f, 0f);

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

		enemyLife.setHP (enemyUnit.atualHP);

		if (morreu) {
			state = battleState.won;
			EndBattle ();
		} else {
			state = battleState.enemyTurn;
			StartCoroutine (EnemyTurn ());
		}
			
		//Resetando botões para poder serem ativados no próximo turno
		attackUnion = 0;
		fireButton.isOn = false;
		earthButton.isOn = false;
		airButton.isOn = false;
		waterButton.isOn = false;

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
			mage1Location.position = (mage1Location.position + position);
			attackUnion = attackUnion + 1;

			buttonFire = true;
		} else 
		{
			mage1Location.position = (mage1Location.position - position);
			attackUnion = attackUnion - 1;
			buttonFire = false;

		}
		print (attackUnion);
	}

	public void OnEarthButton()
	{
		if (buttonEarth == false) {
			mage2Location.position = (mage2Location.position + position);
			attackUnion = attackUnion + 2;
			buttonEarth = true;
		} else 
		{
			mage2Location.position = (mage2Location.position - position);
			attackUnion = attackUnion - 2;
			buttonEarth = false;
		}
	}

	public void OnAirButton()
	{
		if (buttonAir == false) {
			mage3Location.position = (mage3Location.position + position);
			attackUnion = attackUnion + 4;
			buttonAir = true;
		} else 
		{
			mage3Location.position = (mage3Location.position - position);
			attackUnion = attackUnion - 4;
			buttonAir = false;
		}
	}
	public void OnWaterButton()
	{
		if (buttonWater == false) {
			mage4Location.position = (mage4Location.position + position);
			attackUnion = attackUnion + 8;
			buttonWater = true;
		} else 
		{
			mage4Location.position = (mage4Location.position - position);
			attackUnion = attackUnion - 8;
			buttonWater = false;
		}
	}
}
