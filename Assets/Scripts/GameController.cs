using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    private GameObject prevP1 = null;
    private GameObject prevP2;
    private bool bandit = false;

    public GameObject nuts;
    public GameObject platform;
    public GameObject nplatform;

    private GameObject[] CurrentPool;
    private int[] Chances;
    private GameObject[] BasicPool;
    private GameObject[] NutlessPool;

    void Start()
    {
        BasicPool = new GameObject[] {null,nuts,platform,nplatform};
        NutlessPool = new GameObject[] {null,null,platform,platform};
        Chances = new int[] {26, 51, 76};
        prevP2 = nuts.GetComponent<GameObject>();
        InvokeRepeating(nameof(PlatformSpawner), 4.0f, 4.0f);
        InvokeRepeating(nameof(EnemySpawner), 4.0f, 4.0f);
    }
    
    void PlatformSpawner()
    {
        //Bandit pool swap
        if (bandit)
        {
            CurrentPool = NutlessPool;
        }
        else
        {
            CurrentPool = BasicPool;
        }

        //P1 Spawn Logic
        int rnd = UnityEngine.Random.Range(0, 101);
        if (rnd < Chances[0])
        {
            prevP1 = CurrentPool[0];
            try
            {
                Instantiate(CurrentPool[0], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }
        else if (rnd < Chances[1])
        {
            prevP1 = CurrentPool[1];
            try
            {
                Instantiate(CurrentPool[1], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }
        else if (rnd < Chances[2])
        {
            prevP1 = CurrentPool[2];
            try
            {
                Instantiate(CurrentPool[2], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }
        else {
            prevP1 = CurrentPool[3];
            try
            {
                Instantiate(CurrentPool[3], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }

        //P2 Spawn Logic
        rnd = UnityEngine.Random.Range(0, 101);
        if (prevP1.Equals(platform)|| prevP1.Equals(nplatform) || prevP2.Equals(platform) || prevP2.Equals(nplatform))
        {
            if (rnd < Chances[0])
            {
                prevP2 = CurrentPool[0];
                try {
                    Instantiate(CurrentPool[0], new Vector3(20, 3.5f, 0), Quaternion.identity);
                } catch (Exception) { }
            }
            else if (rnd < Chances[1])
            {
                prevP2 = CurrentPool[1];
                try
                {
                    Instantiate(CurrentPool[1], new Vector3(20, 3.5f, 0), Quaternion.identity);
                }
                catch (Exception) { }
            }
            else if (rnd < Chances[2])
            {
                prevP2 = CurrentPool[2];
                try
                {
                    Instantiate(CurrentPool[2], new Vector3(20, 3.5f, 0), Quaternion.identity);
                }
                catch (Exception) { }
            }
            else
            {
                prevP2 = CurrentPool[3];
                try
                {
                    Instantiate(CurrentPool[4], new Vector3(20, 3.5f, 0), Quaternion.identity);
                }
                catch (Exception) { }
            }
        }
    }

    void EnemySpawner()
    {

    }

    void Update()
    {
        
    }
}
