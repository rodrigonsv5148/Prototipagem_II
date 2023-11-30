using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Adicionar chance do boss cancelar o uso de um botão.

public enum battleState { start, playerTurn, enemyTurn, won, lost };
public class BattleSystem : MonoBehaviour
{
    public string Scene;	
    public string enemythings;
    public bool stun = true;
    public float stunChance = 30;
    public bool supression = false;// só funciona com stun habilitado
    public float supressionChance = 15;
    public bool corrosion = true;
    public float corrosionChance = 25;
    private int turnsCorrosion = 0;
    public bool deathMark = true;
    private bool deathMarkOn = false;
    public float deathMarkChance = 9;
    private int turnsToDeath = -1;
    public bool beserker = true;
    public bool criticAttack = true;
    public float criticAttackChance = 30;
    private int baseDamage = 0;
    public int criticFactor = 2;

    public string playerthings;

    public float stunAllyBaseChance = 10;
    private bool stunned = false;
    public bool geloStuna = true;
    public bool terraStuna = true;
    public bool magmaStuna = true;
    public bool plantaStuna = true;
    public bool purpleVoid = true;
    public float purpleVoidChance = 10;
    public float recoveryChanceBase = 10;
    public int recoveryLifeBase = 10;
    public bool geloRecupera = true;
    public bool vaporRecupera = true;
    public bool plantaRecupera = true;
    public bool aguaRecupera = true;
    public bool cut = true;
    public int cutChance = 20;
    private int baseDamagebc = 0;
    public bool cutObsidian = true;
    public bool cutIce = true;
    public float corrosionChanceAlly = 20;
    private int corrosionTime = 0;
    public int timeOfCorrosion = 4;
    public int corrosionDamageBase = 7;
    public bool fireCorrosion = true;
    public bool magmaCorrosion = true;
    public bool vaporCorrosion = true;
    public bool vulcanicGasCorrosion = true;
    public bool acidRainCorrosion = true;
    public bool salitreCorrosion = true;


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
    public GameObject nomeDoAttack;
    private TMP_Text attackText;
    public GameObject danoDoAttack;
    private TMP_Text danoAttackText;
    public GameObject fase;
    private TMP_Text faseText;
    public GameObject informations;
    private TMP_Text informationsText;

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

    private GameObject fakeAttack;

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
    private int danoBaseEter = 30;

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
        attackText = nomeDoAttack.GetComponent<TMP_Text>();
        danoAttackText = danoDoAttack.GetComponent<TMP_Text>();
        faseText = fase.GetComponent<TMP_Text>();
        informationsText = informations.GetComponent<TMP_Text>();
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
        faseText.text = "Player Turn";
        PlayerTurn();
    }

	IEnumerator PlayerAttack()
	{
        bool morreu;

        if (turnsToDeath != 0)
        {
            turnsToDeath--;
        }
        GameObject Attack = fakeAttack;

        //Selecionador de magias
        switch (attackUnion)
		{
            // Para todas as magias, add animações, e arrumar tempos de duração (relação animação/som)
            case 1:
                if(fireCorrosion)
                {
                    if (Random.value < (corrosionChanceAlly-5)/100)
                    {
                        corrosionTime = timeOfCorrosion;
                        corrosionDamageBase += 3;
                    } 
                }
				magia = danoAtkFogo;
                Attack = Instantiate(fireAttackPrefab, spawnAttackLocation1);
                Animator animator = Attack.GetComponent<Animator>();
                animator.Play("FireAttack");
                tempoDeAnimacao = 2.0f;
                somFireAtk.Play();
                danoAttackText.text = magia.ToString();
                attackText.text = "Fogo";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;

			case 2:
				magia = danoAtkTerra;
                Attack = Instantiate(EarthAttackPrefab, spawnAttackLocation1);
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                if(terraStuna)
                {
                    if (Random.value > (100 - stunAllyBaseChance)/100)
                    {
                        stunned = true;
                        informationsText.text = "Enemy Stunned";
                    }
                }
                somEarthAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Terra";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;

			case 4:
                magia = danoAtkAr;
                Attack = Instantiate(AirAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somAirAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Ar";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 8:
                
                magia = danoAtkAgua;
                Attack = Instantiate(waterAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                if (aguaRecupera)
                {
                    if(Random.value > (100 - recoveryChanceBase)/100)
                    {
                        informationsText.text = "Recovery";
                        teamUnit.heal(recoveryLifeBase);
                    }
                }
                somWaterAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Agua";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			//--------------------------------
			case 3:
                if(magmaCorrosion)
                {
                    
                    if (Random.value < (corrosionChanceAlly+5)/100)
                    {
                        corrosionTime = timeOfCorrosion - 2;
                        corrosionDamageBase += 6;
                    }
                    
                }                
				magia = danoAtkMagma;
                Attack = Instantiate(magmaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somMagmaAtk.Play();
                if(magmaStuna)
                {
                    if (Random.value > (100 - stunAllyBaseChance * 2)/100)
                    {
                        stunned = true;
                        informationsText.text = "Enemy Stunned";
                    }
                }
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Magma";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 5:
				magia = danoAtkFumaca;
                Attack = Instantiate(smokeAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somSmokeAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Fumaça";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 9:
                if(vaporCorrosion)
                {
                    if (Random.value < (corrosionChanceAlly+5)/100)
                    {
                        corrosionTime = timeOfCorrosion - 2;
                        corrosionDamageBase += 4;
                    }
                }    
                if(vaporRecupera)
                {
                    if(Random.value > (100 - recoveryChanceBase/1.5)/100)
                    {
                        informationsText.text = "Recovery";
                        teamUnit.heal(recoveryLifeBase * 3);
                    }
                }
				magia = danoAtkVapor;
                Attack = Instantiate(vaporAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somVaporAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Vapor";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 6:
				magia = danoAtkAreia;
                Attack = Instantiate(areiaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somSandAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Areia";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 10:
				magia = danoAtkPlanta;
                Attack = Instantiate(plantaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somPlantAtk.Play();
                if (plantaRecupera)
                {
                    if(Random.value > (100 - recoveryChanceBase)/100)
                    {
                        informationsText.text = "Recovery";
                        teamUnit.heal(recoveryLifeBase/2);
                    }
                }
                if(plantaStuna)
                {
                    if (Random.value > (100 - stunAllyBaseChance * 3)/100)
                    {
                        stunned = true;
                        informationsText.text = "Enemy Stunned";
                    }
                }
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Planta";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 12:
				magia = danoAtkGelo;
                Attack = Instantiate(geloAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somIceAtk.Play();
                if (cut)
                {
                    if (cutIce)
                    {
                        if (Random.value < cutChance/100)
                        {
                            baseDamagebc = danoAtkGelo * 2;
                            magia = baseDamagebc;
                            informationsText.text = "enemy cut";
                        }
                    }
                }
                if(geloRecupera)
                {
                    if(Random.value > (100 - recoveryChanceBase/2)/100)
                    {
                        informationsText.text = "Recovery";
                        teamUnit.heal(recoveryLifeBase/2);
                    }
                }
                if(geloStuna)
                {
                    if (Random.value > (100 - stunAllyBaseChance)/100)
                    {
                        stunned = true;
                        informationsText.text = "Enemy Stunned";
                    }
                }
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Gelo";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
            //------------------------------
            case 7:
                if(vulcanicGasCorrosion)
                {
                    if (Random.value < (corrosionChanceAlly)/100)
                    {
                        corrosionTime = timeOfCorrosion;
                        corrosionDamageBase += 2;
                    } 
                }
				magia = danoAtkGasVucanico;
                Attack = Instantiate(gasVulcanicoAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somVulcanicGasAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Gás Vulcânico (Que é toxico)";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 11:
				magia = danoAtkObsidiana;
                if (cut)
                {
                    if (cutObsidian)
                    {
                        if (Random.value < cutChance/100)
                        {
                            baseDamagebc = danoAtkObsidiana * 3;
                            magia = baseDamagebc;
                            informationsText.text = "enemy cut";
                        }
                    }
                }
                Attack = Instantiate(obsidianaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somObsidianAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Obsidiana";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 13:
                if(acidRainCorrosion)
                {
                    if (Random.value < (corrosionChanceAlly)/100)
                    {
                        corrosionTime = timeOfCorrosion;
                        corrosionDamageBase += 13;
                    } 
                }
				magia = danoAtkChuvaAcida;
                Attack = Instantiate(chuvaAcidaAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somAcidRainAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Chuva ácida";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			case 14:
                if(salitreCorrosion)
                {
                    if (Random.value < (corrosionChanceAlly)/100)
                    {
                        corrosionTime = timeOfCorrosion;
                        corrosionDamageBase += 5;
                    } 
                }
				magia = danoAtkSalitre;
                Attack = Instantiate(salitreAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somSalitreAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Salitre";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
			//----------------------------
			case 15:
                if(purpleVoid)
                {
                    if (Random.value > (100 - purpleVoidChance)/100)
                    {
                        danoBaseEter = danoAtkEter;
                        danoAtkEter = 99999;
                        informationsText.text = "Purple Void";
                    }
                }
				magia = danoAtkEter;
                Attack = Instantiate(eterAttackPrefab, spawnAttackLocation1);//Talvez configurar dependendo de onde venha o ataque, se ele cair no inimigo
                //Animator animator = Attack.GetComponent<Animator>();
                //animator.Play("FireAttack");
                somEterAtk.Play();
                tempoDeAnimacao = 2.0f;// Configurar quando botar animação
                danoAttackText.text = magia.ToString();
                attackText.text = "Éter";
                yield return new WaitForSeconds (2.0f);
                danoAttackText.text = "";
                attackText.text = "";
                break;
		}

        if (corrosionTime != 0)
        {
            magia += corrosionDamageBase;
            corrosionTime--;
            morreu = enemyUnit.takeDamage (magia);

        }else
        {
            morreu = enemyUnit.takeDamage (magia);
        }
		

		yield return new WaitForSeconds (tempoDeAnimacao + 0.5f);

        Destroy(Attack);
		
		enemyLife.setHP (enemyUnit.atualHP);

		if (morreu) {
            informationsText.text = "";
			state = battleState.won;
            StartCoroutine (EndBattle());
		} else if (morreu == false && stunned == false){
            informationsText.text = "";
			danoAtkEter = danoBaseEter;
            state = battleState.enemyTurn;
			StartCoroutine (EnemyTurn ());
		}else if (morreu == false && stunned == true)
        {
            informationsText.text = "";
            stunned = false;
            state = battleState.playerTurn;
            StartCoroutine (PlayerAttack());
            
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
        
        faseText.text = "Enemy Turn";
        yield return new WaitForSeconds(1f);
        if(criticAttack == true)
        {
            baseDamage = enemyUnit.damageBase;
            if(Random.value > (100 - criticAttackChance)/100)
            {
                enemyUnit.damageBase = enemyUnit.damageBase * criticFactor;
            }
        }
        bool playerMorreu = teamUnit.takeDamage(enemyUnit.damageBase);
		print("besta Ataca");
        enemyAttack.Play();
        playerLife.setHP(teamUnit.atualHP);
        
        if(criticAttack == true)
        {
            enemyUnit.damageBase = baseDamage;
        }
        if(stun == true)
        {
            if(Random.value > ( 100 - stunChance)/100 )
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
            }
        }
        if(corrosion == true)
        {
            if(Random.value > (100 - corrosionChance)/100)
            {
                turnsCorrosion = 4; // quantidade de turnos da corrosão
            }
        }
        if(supression == true)
        {
            if(Random.value > (100 - supressionChance)/100)
            {
                interativo = false;
                statusTeam.text = "status: Curse of suppression";
            }
        }
        if(deathMark == true)
        {
            if(Random.value > (100 - deathMarkChance)/100 )
            {
                turnsToDeath = 5;
                deathMark = false;
                deathMarkOn = true;
            }
        }
        if (deathMarkOn == true)
        {
            if(turnsToDeath == 0)
            {
                playerMorreu = teamUnit.takeDamage(9999);
            }
        }
        /*else
        {
            statusTeam.text = "Status: Normal";
        }*/

        
        
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

        if (turnsCorrosion != 0) // efeito da corrosão
        {
            turnsCorrosion--;
            playerMorreu = teamUnit.takeDamage(5);
        }

		if (playerMorreu) 
		{
			state = battleState.lost;
            StartCoroutine (EndBattle());
            
        }
        else if(multiplosAtaques <= 4.1f && playerMorreu == false)
		{
            faseText.text = "Player Turn";
            state = battleState.playerTurn;
			fatorDeQueda = 0;

        }else if(multiplosAtaques > 4.1f && playerMorreu == false)
		{
            if(beserker == false)
            {
                fatorDeQueda++;
            }
			state = battleState.enemyTurn;
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
            SceneManager.LoadScene(Scene);
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
            SceneManager.LoadScene(Scene);
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
            attackText.text = "Escolha elementos";
            yield return new WaitForSeconds (2.0f);
            attackText.text = "";
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
