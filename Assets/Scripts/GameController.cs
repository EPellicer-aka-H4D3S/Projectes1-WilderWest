using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    private GameObject prevP1 = null;
    private GameObject prevP2;
    private int bisonCounter;
    public int bisonCurrent;
    public bool bandit = false;

    [Header("Prefabs")]
    public GameObject nuts;
    public GameObject platform;
    public GameObject nplatform;
    public GameObject bison;

    private GameObject[] CurrentPool;
    private int[] Chances;
    private GameObject[] BasicPool;
    private GameObject[] NutlessPool;
    public PlayerController playerController;

    void Start()
    {
        BasicPool = new GameObject[] {null,nuts,platform,nplatform};
        NutlessPool = new GameObject[] {null,null,platform,platform};
        Chances = new int[] {26, 51, 76};
        bisonCounter = UnityEngine.Random.Range(4, 8);
        bisonCurrent = bisonCounter;

        prevP2 = nuts.GetComponent<GameObject>();

        InvokeRepeating(nameof(PlatformSpawner), 4.0f, 1.6f);
        InvokeRepeating(nameof(EnemySpawner), 10.0f, 4.0f);
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
        int temp;
        if (rnd < Chances[0])
        {
            temp = 0;
            try
            {
                Instantiate(CurrentPool[0], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }
        else if (rnd < Chances[1])
        {
            temp = 1;
            try
            {
                Instantiate(CurrentPool[1], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }
        else if (rnd < Chances[2])
        {
            temp = 2;
            try
            {
                Instantiate(CurrentPool[2], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }
        else {
            temp = 3;
            try
            {
                Instantiate(CurrentPool[3], new Vector3(20, -1.5f, 0), Quaternion.identity);
            }
            catch (Exception) { }
        }

        //P2 Spawn Logic
        rnd = UnityEngine.Random.Range(0, 101);
        if (!((prevP1==null || prevP1.Equals(nuts)) && (prevP2 == null || prevP2.Equals(nuts))))
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
        prevP1 = CurrentPool[temp];
    }

    void EnemySpawner()
    {
        if (!bandit)
        {
            if (bisonCurrent > 0)
            {
                if (playerController.transform.position.y < -1.5f)
                {
                    bisonCurrent--;
                    Instantiate(bison, new Vector3(20, -4.5f, 0), Quaternion.identity);
                }
                else if (playerController.transform.position.y < 3.5f && !((prevP1 == null || prevP1.Equals(nuts))))
                {
                    bisonCurrent--;
                    Instantiate(bison, new Vector3(20, 0.5f, 0), Quaternion.identity);
                }
                else if(!(prevP2 == null || prevP2.Equals(nuts)))
                {
                    bisonCurrent--;
                    Instantiate(bison, new Vector3(20, 5.5f, 0), Quaternion.identity);
                }
            }
            else
            {
                bisonCounter = UnityEngine.Random.Range(4, 8);
                bisonCurrent = bisonCounter;
                Debug.Log("Bandit Spawn");
            }
        }
    }

    void Update()
    {
        
    }
}
