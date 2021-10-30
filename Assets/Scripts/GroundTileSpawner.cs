using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundTileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnedGroundTiles = new GameObject[14];
    [SerializeField] private CreatableObjects[] creatableObjects;

    [SerializeField] private float startSpawnPosX = -0.715f;
    [SerializeField] private float startSpawnPosY = -1.2f;

    [SerializeField] private int tileNum;
    [SerializeField] private int score;

    [SerializeField] private Text scoreText;
    [SerializeField] private Transform border;

    [SerializeField] private GameObject[] groundTiles;

   private void Start()
    {
        FirstGroundTileSpawn();
        UpdateScore();
    }
    private void FirstGroundTileSpawn() 
    {
        for (int i = 0; i < spawnedGroundTiles.Length; i++) 
        {
            int rand = DifficultyChanger();
            if (i == 0) rand = 0;
            CreateTile(rand, i);
        }
    }

    public void GroundTileSpawn(int i) 
    {
        int rand = DifficultyChanger();
        Destroy(spawnedGroundTiles[i]);
        if (rand != 10) CalculateChanceOfCreatingObject();
        CreateTile(rand, i);
        ChangeBorderPosition();
        score++;
        UpdateScore();
    }

    private void CreateTile(int _rand, int _i) 
    {
        spawnedGroundTiles[_i] = Instantiate(groundTiles[_rand], new Vector3(startSpawnPosX + tileNum * 0.17f, startSpawnPosY + tileNum * 0.08f, 0), Quaternion.identity);
        spawnedGroundTiles[_i].GetComponent<GroundTile>().SetId(_i);
        spawnedGroundTiles[_i].GetComponent<GroundTile>().SetTileSpawner(gameObject);
        tileNum++;
    }

    private void ChangeBorderPosition()
    {
        border.transform.position = new Vector3(startSpawnPosX + score * 0.17f, startSpawnPosY + score * 0.08f, 0);
    }

    public void UpdateScore()
    {
       scoreText.text = "Score: " + score.ToString();
    }

    private int DifficultyChanger()
    {
        int percentRand = Random.Range(0, 100);
        if (percentRand < 10) 
        {
            return 10;
        }
        else
        {
            int rand = Random.Range(0, 10);
            return rand;
        }
    }

    private void CalculateChanceOfCreatingObject() 
    {
        bool isCreated = false;
        for(int i = 0; i < creatableObjects.Length; i++) 
        {
            if (isCreated == true) break;
            int percentRand = Random.Range(0, 100);
            if (creatableObjects[i].chanceOfCreatingObject > percentRand)
            {
                CreateObjectOnTile(i);
                isCreated = true;
            }
        }
    }
    private void CreateObjectOnTile(int i) 
    {
        var go = Instantiate(creatableObjects[i].obj, new Vector3(startSpawnPosX + tileNum * 0.17f, startSpawnPosY + 0.88f + tileNum * 0.08f, 0), Quaternion.identity);
    }
    
    [System.Serializable]
    private struct CreatableObjects
    {
        public GameObject obj;
        public int chanceOfCreatingObject;
    }
}


