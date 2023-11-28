using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using TMPro;

//Adicionar chance do boss cancelar o uso de um botão.

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

    public int danoAtkFogo = 9;
    public int danoAtkTerra = 12;
    public int danoAtkAr = 6;
    public int danoAtkAgua = 10;
    public int danoAtkMagma = 21;
    public int danoAtkFumaca = 5;
    public int danoAtkVapor = 13;
    public int danoAtkAreia = 11;
    public int danoAtkPlanta = 14;
    public int danoAtkGelo = 15;
    public int danoAtkGasVucanico = 20;
    public int danoAtkObsidiana = 25;
    public int danoAtkChuvaAcida = 10;
    public int danoAtkSalitre = 19;
    public int danoAtkEter = 30;

    public AudioSource somFireAtk;
    public AudioSource somEarthAtk;
    public AudioSource somAirAtk;
    public AudioSource somWaterAtk;
    public AudioSource somMagmaAtk;
    public AudioSource somSmokeAtk;
    public AudioSource somVaporAtk;
    public AudioSource somSandAtk;
    public AudioSource somPlantAtk;
    public AudioSource somIceAtk;
    public AudioSource somVulcanicGasAtk;
    public AudioSource somObsidianAtk;
    public AudioSource somAcidRainAtk;
    public AudioSource somSalitreAtk;
    public AudioSource somEterAtk;
    public AudioSource battleMusic;
    public AudioSource enemyAttack;
    public AudioSource victorySound;
    public AudioSource loseSound;
    public AudioSource prepareMagicEffect;
    public AudioSource errorEffect;
    public AudioSource firePreparation;
    public AudioSource magicDesarm;
    public AudioSource earthPreparation;
    public AudioSource airPreparation;
    public AudioSource waterPreparation;
    bool interativo = true;
    public GameObject texto;
    private TMP_Text statusTeam;
    void Start()
    {
        state = battleState.start;
		StartCoroutine(SetupBattle());
        battleMusic.Play();
    }

	IEnumerator SetupBattle() 
    {
        GameObject fireMage = Instantiate(playerFirePrefab, mage1Location);
        //playerFireUnit = fireMage.GetComponent<Unit>();

        GameObject earthMage = Instantiate(playerEarthPrefab, mage2Location);
        //playerEarthUnit = earthMage.GetComponent<Unit>();

        GameObject airMage = Instantiate(playerAirPrefab, mage3Location);
        //playerAirUnit = airMage.GetComponent<Unit>();

        GameObject waterMage = Instantiate(playerWaterPrefab, mage4Location);
        //playerWaterUnit = waterMage.GetComponent<Unit>();

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
            // Para todas as magias, add animações, e arrumar tempos de duração (relação animação/som)
            case 1:
				magia = danoAtkFogo;
                Attack = Instantiate(fireAttackPrefab, spawnAttackLocation1);
                Animator animator = Attack.GetComponent<Animator>();
                animator.Play("FireAttack");
                tempoDeAnimacao = 2.0f;
                somFireAtk.Play();
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Fogo";
                break;

			case 2:
				magia = danoAtkTerra;
                Attack = Instantiate(EarthAttackPrefab, spawnAttackLocation1);
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somEarthAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Terra";
                break;

			case 4:
                magia = danoAtkAr;
                Attack = Instantiate(AirAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somAirAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Ar";
                break;
			case 8:
                
                magia = danoAtkAgua;
                Attack = Instantiate(waterAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somWaterAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Agua";
                break;
			//--------------------------------
			case 3:
				magia = danoAtkMagma;
                Attack = Instantiate(magmaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somMagmaAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Magma";
                break;
			case 5:
				magia = danoAtkFumaca;
                Attack = Instantiate(smokeAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somSmokeAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Fumaça";
                break;
			case 9:
				magia = danoAtkVapor;
                Attack = Instantiate(vaporAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somVaporAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Vapor";
                break;
			case 6:
				magia = danoAtkAreia;
                Attack = Instantiate(areiaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somSandAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Areia";
                break;
			case 10:
				magia = danoAtkPlanta;
                Attack = Instantiate(plantaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somPlantAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Planta";
                break;
			case 12:
				magia = danoAtkGelo;
                Attack = Instantiate(geloAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somIceAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Gelo";
                break;
            //------------------------------
            case 7:
				magia = danoAtkGasVucanico;
                Attack = Instantiate(gasVulcanicoAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somVulcanicGasAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Gás Vulcânico (Que é toxico)";
                break;
			case 11:
				magia = danoAtkObsidiana;
                Attack = Instantiate(obsidianaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somObsidianAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Obsidiana";
                break;
			case 13:
				magia = danoAtkChuvaAcida;
                Attack = Instantiate(chuvaAcidaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somAcidRainAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Chuva ácida";
                break;
			case 14:
				magia = danoAtkSalitre;
                Attack = Instantiate(salitreAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somSalitreAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoDoAttack = magia.ToString();
                nomeDoAttack = "Salitre";
                break;
			//----------------------------
			case 15:
				magia = danoAtkEter;
                Attack = Instantiate(eterAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somEterAtk.Play();
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
        statusTeam = texto.GetComponent<TMP_Text>();
        if(interativo == true)
        {
            fireButton.interactable = true;
            earthButton.interactable = true;
            airButton.interactable = true;
            waterButton.interactable = true;
        }
        
        fase = "Enemy Turn";
        yield return new WaitForSeconds(1f);

        bool playerMorreu = teamUnit.takeDamage(enemyUnit.damageBase);
		print("besta Ataca");
        enemyAttack.Play();
        playerLife.setHP(teamUnit.atualHP);
        if(Random.value > 0.7)
        {
            float desabilitar;
            desabilitar = Random.value;
            if(desabilitar < 0.25)
            {
                fireButton.interactable = false;
                if(interativo == true)
                {
                    statusTeam.text = "status: Fire warrior stunned";
                }else
                {
                    statusTeam.text = "status: Fire warrior supressed";
                    yield return new WaitForSeconds(3f);
                    statusTeam.text = "status: Curse of suppression";
                }
            }
            else if (desabilitar >= 0.25 && desabilitar < 0.5)
            {
                earthButton.interactable = false;
                if(interativo == true)
                {
                    statusTeam.text = "status: Earth warrior stunned";
                }else
                {
                    statusTeam.text = "status: Earth warrior supressed";
                    yield return new WaitForSeconds(3f);
                    statusTeam.text = "status: Curse of suppression";
                }

            }else if (desabilitar >= 0.5 && desabilitar < 0.75)
            {
                airButton.interactable = false;
                if(interativo == true)
                {
                    statusTeam.text = "status: Air warrior stunned";
                }else
                {
                    statusTeam.text = "status: Air warrior supressed";
                    yield return new WaitForSeconds(3f);
                    statusTeam.text = "status: Curse of suppression";
                }

            }else if (desabilitar >= 0.75 && desabilitar < 1)
            {
                waterButton.interactable = false;
                if(interativo == true)
                {
                    statusTeam.text = "status: Water warrior stunned";
                }else
                {
                    statusTeam.text = "status: Air warrior supressed";
                    yield return new WaitForSeconds(3f);
                    statusTeam.text = "status: Curse of suppression";
                }
            }
        }else
        {
            statusTeam.text = "Status: Normal";
        }

        if(Random.value > 0.85)
        {
            interativo = false;
            statusTeam.text = "status: Curse of suppression";
        }
        yield return new WaitForSeconds(2f);// regular baseado no tempo do som do ataque
		
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
            victorySound.Play();
            yield return new WaitForSeconds(1f);
            Destroy(enemy);
            yield return new WaitForSeconds(1f);
            winText.SetActive (true);// Podemos trocar o texto por uma imagem;
            yield return new WaitForSeconds(3f);//Ajustar os 3 yield ao tempo ao som de vitória
            SceneManager.LoadScene("Menu");
            print("ganhei");
		} else //O que acontece se perder?
		{
            loseSound.Play();
            yield return new WaitForSeconds(1f);
            Destroy(fireMage);
            Destroy(earthMage);
            Destroy(airMage);
            Destroy(waterMage);
            yield return new WaitForSeconds(1f);
            looseText.SetActive (true);// Podemos trocar o texto por uma imagem;
            yield return new WaitForSeconds(3f);//Ajustar os 3 yield ao tempo ao som de vitória
            SceneManager.LoadScene("Menu");
            print("no ceu tem pao?");
		}
	}

	void PlayerTurn (){}

	public void OnAttackButton ()
	{
        StartCoroutine(attackButton());
	}
    IEnumerator attackButton ()
    {
        if(attackUnion > 0) 
        {
            prepareMagicEffect.Play();
            yield return new WaitForSeconds(3f);//Ajustar ao tempo ao som do efeito
            if (state != battleState.playerTurn)
            {
                //return; ---------------------------------------- não sei se vai fazer falta
            }
            StartCoroutine(PlayerAttack());
        }
        else 
        {
            errorEffect.Play();
            yield return new WaitForSeconds(3f);//Ajustar ao tempo ao som do efeito
            nomeDoAttack = "Escolha elementos";
        }
    }
	public void OnFireButton()
	{
		//Em todos, botar som do elemento e mudar o sprite quando ativar, e som de magia se dissipando quando desativar------------------------------------------------------------------
		if (buttonFire == false) {
            firePreparation.Play();
			mage1Location.position = (mage1Location.position + position);
			attackUnion++;
			buttonFire = true;
		} else 
		{
            magicDesarm.Play();
			mage1Location.position = (mage1Location.position - position);
			attackUnion--;
			buttonFire = false;
		}
	}
	public void OnEarthButton()
	{
		if (buttonEarth == false) {
            earthPreparation.Play();
			mage2Location.position = (mage2Location.position + position);
			attackUnion += 2;
			buttonEarth = true;
		} else 
		{
            magicDesarm.Play();
			mage2Location.position = (mage2Location.position - position);
			attackUnion -= 2;
			buttonEarth = false;
		}
	}
	public void OnAirButton()
	{
		if (buttonAir == false) {
            airPreparation.Play();
			mage3Location.position = (mage3Location.position + position);
			attackUnion += 4;
			buttonAir = true;
		} else 
		{
            magicDesarm.Play();
			mage3Location.position = (mage3Location.position - position);
			attackUnion -= 4;
			buttonAir = false;
		}
	}
	public void OnWaterButton()
	{
		if (buttonWater == false) {
            waterPreparation.Play();
			mage4Location.position = (mage4Location.position + position);
			attackUnion += 8;
			buttonWater = true;
		} else 
		{
            magicDesarm.Play();
			mage4Location.position = (mage4Location.position - position);
			attackUnion -= 8;
			buttonWater = false;
		}
	}
}
