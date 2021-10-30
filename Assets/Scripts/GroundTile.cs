using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private GameObject groundTileSpawner;
    private int id;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayArea"))
        {
           groundTileSpawner.GetComponent<GroundTileSpawner>().GroundTileSpawn(id);
        }
    }

    public void SetTileSpawner(GameObject _groundTileSpawner) 
    {
        groundTileSpawner = _groundTileSpawner;
    }

    public void SetId(int _id) 
    {
        id = _id;
    }
}
