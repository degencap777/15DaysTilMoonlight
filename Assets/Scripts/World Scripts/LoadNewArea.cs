﻿using System.Collections;
using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public string levelToLoad;
    public string exitPoint;
    private PlayerController thePlayer;
    private PlayerHealthManager playerHealth;
    private PlayerStaminaManager playerStamina;
    private PlayerRangedAttack playerRangedAttack;
    private PlayerStats playerStatsScript;
    private PlayerStartPoint startPoint;
    public static int counter;
    public string curLvl;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();
        playerStamina = FindObjectOfType<PlayerStaminaManager>();
        playerRangedAttack = FindObjectOfType<PlayerRangedAttack>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
        startPoint = FindObjectOfType<PlayerStartPoint>();
        curLvl = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Snowy A Exits
        if (other.gameObject.name == "Player" && curLvl == "SnowyA" && this.gameObject.name == "SnowyBEntry")
        {
            SetAllForLvl();
            //PlayerPrefs.SetInt("Global Music Tracker", 0);
            SceneManager.LoadScene("SnowyB", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_B_A_Entrance");
        }
        else if (other.gameObject.name == "Player" && curLvl == "SnowyA" && this.gameObject.name == "SnowyCEntry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("SnowyC", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_C_A_Entrance");
        }
        // Snowy B Exits
        else if (other.gameObject.name == "Player" && curLvl == "SnowyB" && this.gameObject.name == "SnowyAEntry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("SnowyA", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_A_B_Entrance");
        }
        else if (other.gameObject.name == "Player" && curLvl == "SnowyB" && this.gameObject.name == "SnowyDEntry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("SnowyD_Crossroads", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_D_B_Entrance");
        }
        // Snowy C Exits
        else if (other.gameObject.name == "Player" && curLvl == "SnowyC" && this.gameObject.name == "SnowyAEntry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("SnowyA", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_A_C_Entrance");
        }
        // Snowy D Exits
        else if (other.gameObject.name == "Player" && curLvl == "SnowyD_Crossroads" && this.gameObject.name == "SnowyBEntry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("SnowyB", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_B_D_Entrance");
        }
        // Snowy Crossroads Exits
        else if (other.gameObject.name == "Player" && curLvl == "SnowyD_Crossroads" && this.gameObject.name == "Snowy_Graveyard_Entry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("Snowy_Graveyard", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_Graveyard_Crossroads");
        }
        // Snowy Graveyard Exits
        else if (other.gameObject.name == "Player" && curLvl == "Snowy_Graveyard" && this.gameObject.name == "Snowy_Crossroads_Entry")
        {
            SetAllForLvl();
            Debug.Log("hmmm");
            SceneManager.LoadScene("SnowyD_Crossroads", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_D_Graveyard_Entry");
        }
    }
    public void SetAllForLvl()
    {
        PlayerPrefs.SetInt("Global Player Current Health", playerHealth.playerCurrentHealth);
        PlayerPrefs.SetInt("Global Player Max Health", playerHealth.playerMaxHealth);
        PlayerPrefs.SetInt("Global Player Current Stamina", playerStamina.playerCurrentStamina);
        PlayerPrefs.SetInt("Global Player Max Stamina", playerStamina.playerMaxStamina);
        PlayerPrefs.SetInt("Global Player Current Xp", playerStatsScript.currentExp);
        PlayerPrefs.SetInt("Global Player Level", playerStatsScript.currentLevel);
        PlayerPrefs.SetInt("Global Player Points To Spend", playerStatsScript.pointsToSpend);
        PlayerPrefs.SetInt("Global Player Vitality", playerStatsScript.vitality);
        PlayerPrefs.SetInt("Global Player Strength", playerStatsScript.strength);
        PlayerPrefs.SetInt("Global Player Dexterity", playerStatsScript.dexterity);
        PlayerPrefs.SetInt("Global Player Intelligence", playerStatsScript.intelligence);
        PlayerPrefs.SetInt("Global Player Dagger Count", playerRangedAttack.daggerCount);
        PlayerPrefs.SetFloat("Global Player Last Move X", thePlayer.lastMove.x);
        PlayerPrefs.SetFloat("Global Player Last Move Y", thePlayer.lastMove.y);
        PlayerPrefs.SetString("Global Player Cur Lvl", curLvl);
        if (thePlayer.lockOn)
        {
            PlayerPrefs.SetInt("Global Player Lock On", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Global Player Lock On", 0);
        }
    }

}
