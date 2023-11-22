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

	private float multiplosAtaques = 0.0f;
	private float fatorDeQueda = 0.0f;

	void Start()
    {
        state = battleState.start;
		StartCoroutine(SetupBattle());
		//iniciar musica de fundo -------------------------------------------------------------------------------------------------------------------------------------------------------------
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
		//Selecionador de magias
		switch(attackUnion)
		{
			// Para todas as magias add efeito de som e animações-------------------------------------------------------------------------------------------------------------------
			case 1:
				magia = 9;
				break;
			case 2:
				magia = 12;
				break;
			case 4:
				magia = 6;
				break;
			case 8:
				magia = 10;
				break;
			//--------------------------------
			case 3:
				magia = 21;
				break;
			case 5:
				magia = 15;
				break;
			case 9:
				magia = 13;
				break;
			case 6:
				magia = 11;
				break;
			case 10:
				magia = 14;
				break;
			case 12:
				magia = 15;
				break;
            //------------------------------
            case 7:
				magia = 25;
				break;
			case 11:
				magia = 20;
				break;
			case 13:
				magia = 22;
				break;
			case 14:
				magia = 19;
				break;
			//----------------------------
			case 15:
				magia = 30;
				break;
		}

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
	}

	IEnumerator EnemyTurn() 
	{
		yield return new WaitForSeconds(1f);

		bool playerMorreu = teamUnit.takeDamage(enemyUnit.damageBase);
		print("besta Ataca");
		// Adicionar som de ataque ------------------------------------------------------------------------------------
        playerLife.setHP(teamUnit.atualHP);

        yield return new WaitForSeconds(1f);

		// Codigo de multiplos ataques
		multiplosAtaques = (Random.value * attackUnion) - fatorDeQueda;
		print(multiplosAtaques);
		
		fireButton.isOn = false;
		earthButton.isOn = false;
		airButton.isOn = false;
		waterButton.isOn = false;
		//Resetando botões para poder serem ativados no próximo turno
		attackUnion = 0;

		if (playerMorreu) 
		{
			state = battleState.lost;
			EndBattle();
            
        }
        else if(multiplosAtaques <= 4.5f && playerMorreu == false)
		{
            state = battleState.playerTurn;
			//EndBattle();
			fatorDeQueda = 0;
			print("vez do player");

        }else if(multiplosAtaques > 4.5f && playerMorreu == false)
		{
			state = battleState.enemyTurn;
			fatorDeQueda++;
			segundoAtaque();
		}
    }
	
	void segundoAtaque ()
	{
		StartCoroutine (EnemyTurn ());	
	}

	void EndBattle()
	{
		if (state == battleState.won) {
            //adicionar som de end game, dar load no menu--------------------------------------------------
            print("ganhei");
		} else 
		{
            //adicionar som de vitória, dar load na fase--------------------------------------------------
            //O que acontece se perder?
            print("no ceu tem pao?");
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

		//Em todos, botar som do elemento e mudar o sprite quando ativar, e som de magia se dissipando quando desativar------------------------------------------------------------------
		if (buttonFire == false) {
			mage1Location.position = (mage1Location.position + position);
			attackUnion++;
			buttonFire = true;
		} else 
		{
			mage1Location.position = (mage1Location.position - position);
			attackUnion--;
			buttonFire = false;
		}
	}

	public void OnEarthButton()
	{
		if (buttonEarth == false) {
			mage2Location.position = (mage2Location.position + position);
			attackUnion += 2;
			buttonEarth = true;
		} else 
		{
			mage2Location.position = (mage2Location.position - position);
			attackUnion -= 2;
			buttonEarth = false;
		}
	}

	public void OnAirButton()
	{
		if (buttonAir == false) {
			mage3Location.position = (mage3Location.position + position);
			attackUnion += 4;
			buttonAir = true;
		} else 
		{
			mage3Location.position = (mage3Location.position - position);
			attackUnion -= 4;
			buttonAir = false;
		}
	}

	public void OnWaterButton()
	{
		if (buttonWater == false) {
			mage4Location.position = (mage4Location.position + position);
			attackUnion += 8;
			buttonWater = true;
		} else 
		{
			mage4Location.position = (mage4Location.position - position);
			attackUnion -= 8;
			buttonWater = false;
		}
	}
}
