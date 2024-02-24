using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    public Transform playerSpawnTransform;

    public List<PlayerController> players;
    public List<AIController> AIplayers;

    // Method called when first created
    private void Awake()
    {
        // if another GameManager, rmeove
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnPlayer();
        GetAIControllers();
    }



    // Spawns Player
    public void SpawnPlayer()
    {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, Quaternion.identity) as GameObject;
    
        Controller newPlayerController = newPlayerObj.GetComponent<Controller>();

        Pawn newPlayerPawn = newPawnObj.GetComponent<Pawn>();

        newPlayerController.pawn = newPlayerPawn;

        // Noise Maker
        newPlayerPawn.AddComponent<NoiseMaker>();
        newPlayerPawn.noiseMaker = newPlayerPawn.GetComponent<NoiseMaker>();
        newPlayerController.pawn = newPlayerPawn;
    }

    public void GetAIControllers()
    {
        //    Pawn[] allTanks = FindObjectsByType<Pawn>(FindObjectsSortMode.None);
        AIplayers = (FindObjectsByType<AIController>(FindObjectsSortMode.None)).ToList();

    }


}
