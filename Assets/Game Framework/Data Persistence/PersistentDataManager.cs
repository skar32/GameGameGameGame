﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentDataManager : MonoBehaviour
{
    public List<MinigameScriptableObject> scriptableObjectMinigames;

    public PlayerSettings debug_playerSettings;
    public MinigameList debug_minigameMasterList;
    public Run debug_run;



    public static PersistentDataManager INSTANCE;
    public static PlayerSettings playerSettings;
    public static MinigameList minigameMasterList;
    public static Run run;

    
    
    
    // MonoBehaviour Methods

    private void Awake() {
        if (minigameMasterList == null) LoadMinigameMasterList();
        if (playerSettings == null) LoadPlayerSettings();
        if (run == null) {

            //Adds ONLY current scene game to list -- for testing
            Minigame m = IsInMinigame();
            if (m != null) {
                //creates run with only this game
                CreateNewRun(new MinigameList(m));
            }
        }


        debug_playerSettings = playerSettings;
        debug_minigameMasterList = minigameMasterList;
    }

    private void Start() {
        INSTANCE = this;
            
    }

    //Public Methods

    public void SavePlayerSettings() {
        if (playerSettings != null) {
            FileSaveUtil.SaveData<PlayerSettings>("playerSettings", playerSettings);
        }
    }

    public void SaveMinigameMasterList() {
        if (minigameMasterList != null) {
            FileSaveUtil.SaveData<MinigameList>("minigameMasterList", minigameMasterList);
        }
    }

    public void CreateNewRun(MinigameList runList) {
        run = new Run(runList);
        debug_run = run;
    }


    // Private Methods

    private void LoadPlayerSettings() {
        playerSettings = FileSaveUtil.LoadData<PlayerSettings>("playerSettings");
        if (playerSettings == null) {
            playerSettings = new PlayerSettings();
        }
    }

    private void LoadMinigameMasterList() {
        minigameMasterList = FileSaveUtil.LoadData<MinigameList>("minigameMasterList");
        if (minigameMasterList == null) {
            minigameMasterList = new MinigameList(scriptableObjectMinigames);
        }
    }

    private Minigame IsInMinigame() {
        return minigameMasterList.minigames.Find(item => item.SceneNumber == SceneManager.GetActiveScene().buildIndex);
    }

}