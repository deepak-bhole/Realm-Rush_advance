using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower TowerPrefab;
    [SerializeField] bool isPlacable;
    public bool IsPlacable { get{ return isPlacable; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();


    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isPlacable)
            {
                gridManager.blockNode(coordinates);
            }
        }
    }
    void OnMouseDown() 
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockpath(coordinates))
        {   
            bool isSuccessfull = TowerPrefab.CreateTower(TowerPrefab, transform.position);
             
            if(isSuccessfull)
            {
                gridManager.blockNode(coordinates);
                pathFinder.NotifyReceivers();
            } 
        }
        
    }
}
