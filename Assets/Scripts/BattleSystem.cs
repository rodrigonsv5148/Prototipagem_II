using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;


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

	public Transform spawnAttackLocation1;
    public Transform spawnAttackLocation2;
    public string nomeDoAttack;
    public string danoDoAttack;
    public string fase = "";

    public GameObject fireAttackPrefab;
    public GameObject EarthAttackPrefab;
    public GameObject AirAttackPrefab;
    public GameObject waterAttackPrefab;
    public GameObject magmaAttackPrefab;
    public GameObject smokeAttackPrefab;
    public GameObject vaporAttackPrefab;
    public GameObject areiaAttackPrefab;
    public GameObject plantaAttackPrefab;
    public GameObject geloAttackPrefab;
    public GameObject gasVulcanicoAttackPrefab;
    public GameObject obsidianaAttackPrefab;
    public GameObject chuvaAcidaAttackPrefab;
    public GameObject salitreAttackPrefab;
    public GameObject eterAttackPrefab;

    public GameObject fakeAttack;

    public GameObject enemy;
    public GameObject fireMage;
    public GameObject earthMage;
    public GameObject airMage;
    public GameObject waterMage;
    public GameObject winText;
    public GameObject looseText;

    public float tempoDeAnimacao = 1.0f;
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

        enemy = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemy.GetComponent<Unit>();

        teamUnit = team.GetComponent<Unit>();

        playerLife.SetHud(teamUnit);
		enemyLife.SetHud(enemyUnit);

		yield return new WaitForSeconds (1f);

		state = battleState.playerTurn;
        fase = "Player Turn";
        PlayerTurn();
    }

	IEnumerator PlayerAttack()
	{
        GameObject Attack = fakeAttack;

        //Selecionador de magias
        switch (attackUnion)
		{
            // Para todas as magias, add efeito de som e animações, e arrumar tempos de duração-------------------------------------------------------------------------------------------------------
            case 1:
				magia = 9;
                Attack = Instantiate(fireAttackPrefab, spawnAttackLocation1);
                Animator animator = Attack.GetComponent<Animator>();
                animator.Play("FireAttack");
                tempoDeAnimacao = 2.0f;
                //Som da animação (Tem que ser menor ou igual o tempo de animação)
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Fogo";
                break;

			case 2:
				magia = 12;
                Attack = Instantiate(EarthAttackPrefab, spawnAttackLocation1);
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Terra";
                break;

			case 4:
                magia = 6;
                Attack = Instantiate(AirAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Ar";
                break;
			case 8:
                
                magia = 10;
                Attack = Instantiate(waterAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Agua";
                break;
			//--------------------------------
			case 3:
				magia = 21;
                Attack = Instantiate(magmaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Magma";
                break;
			case 5:
				magia = 3;
                Attack = Instantiate(smokeAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Fumaça";
                break;
			case 9:
				magia = 13;
                Attack = Instantiate(vaporAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Vapor";
                break;
			case 6:
				magia = 11;
                Attack = Instantiate(areiaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Areia";
                break;
			case 10:
				magia = 14;
                Attack = Instantiate(plantaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Planta";
                break;
			case 12:
				magia = 15;
                Attack = Instantiate(geloAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Gelo";
                break;
            //------------------------------
            case 7:
				magia = 20;
                Attack = Instantiate(gasVulcanicoAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Gás Vulcânico (Que é toxico)";
                break;
			case 11:
				magia = 25;
                Attack = Instantiate(obsidianaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)
                
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Obsidiana";
                break;
			case 13:
				magia = 10;
                Attack = Instantiate(chuvaAcidaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)
                
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Chuva ácida";
                break;
			case 14:
				magia = 19;
                Attack = Instantiate(salitreAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)

                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Salitre";
                break;
			//----------------------------
			case 15:
				magia = 30;
                Attack = Instantiate(eterAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                //Som da animação (Tem que ser menor ou igual o tempo de animação)
                
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Éter";
                break;
		}

		bool morreu = enemyUnit.takeDamage (magia);

		yield return new WaitForSeconds (tempoDeAnimacao + 0.5f);

        Destroy(Attack);
		
		enemyLife.setHP (enemyUnit.atualHP);

		if (morreu) {
			state = battleState.won;
            StartCoroutine (EndBattle());
		} else {
			state = battleState.enemyTurn;
			StartCoroutine (EnemyTurn ());
		}
	}

	IEnumerator EnemyTurn() 
	{
        fase = "Enemy Turn";
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
            StartCoroutine (EndBattle());
            
        }
        else if(multiplosAtaques <= 4.5f && playerMorreu == false)
		{
            fase = "Player Turn";
            state = battleState.playerTurn;
			fatorDeQueda = 0;

        }else if(multiplosAtaques > 4.5f && playerMorreu == false)
		{
			state = battleState.enemyTurn;
			fatorDeQueda++;
			StartCoroutine (EnemyTurn ());
		}
    }

	IEnumerator EndBattle()
	{
        // O que acontece se vencer?
		if (state == battleState.won) {
            //adicionar som de vitória--------------------------------------------------
            yield return new WaitForSeconds(1f);
            Destroy(enemy);
            yield return new WaitForSeconds(1f);
            winText.SetActive (true);
            yield return new WaitForSeconds(3f); // Tempo da musica, tem que ver como vai ser
            SceneManager.LoadScene("Menu");
            print("ganhei");
		} else //O que acontece se perder?
		{
            //adicionar som de end game--------------------------------------------------
            yield return new WaitForSeconds(1f);
            Destroy(fireMage);
            Destroy(earthMage);
            Destroy(airMage);
            Destroy(waterMage);
            yield return new WaitForSeconds(1f);
            looseText.SetActive (true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Menu");
            print("no ceu tem pao?");
		}
	}

	void PlayerTurn ()
	{
		
	}

	public void OnAttackButton ()
	{
        if(attackUnion > 0) 
        {
            if (state != battleState.playerTurn)
            {
                return;
            }

            StartCoroutine(PlayerAttack());
        }
        else 
        {
            nomeDoAttack = "Escolha elementos";
        }
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
