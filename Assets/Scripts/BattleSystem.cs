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

    void Start()
    {
        state = battleState.start;
        SetupBattle();
    }

    void SetupBattle() 
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
    }

}
