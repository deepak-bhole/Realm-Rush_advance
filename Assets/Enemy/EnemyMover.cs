using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    
    [SerializeField] [Range(0f, 5f)] float Speed = 1f;
    List<Node> path = new List<Node>();
    
    Enemy enemy;

    GridManager gridManager;
    PathFinder pathFinder;
  
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Debug.Log("RECALCULATEPATH");
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathFinder.startcoordinates;
        }
        else 
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }
    void ReturnToStart()
    {
        transform.position = gridManager.GetPostionFromCoordinates(pathFinder.startcoordinates);
    }

    void FinishPath()
    {
        Debug.Log("FINISHPATH");
        gameObject.SetActive(false);
        enemy.StealGold();
    }

    IEnumerator FollowPath()
    {
        Debug.Log("FOLLOWPATH");
        for(int i = 1; i < path.Count; i++) 
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPostionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent<1f)
                {
                    travelPercent += Time.deltaTime * Speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
        }
        FinishPath();  
    }

}


//yield return new WaitForSeconds(waitTime);