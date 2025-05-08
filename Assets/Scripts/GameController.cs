using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    private GameObject prevP1 = null;
    private GameObject prevP2 = null;
    private bool bandit = false;

    public GameObject nuts;
    public GameObject platform;
    public GameObject nplatform;

    private GameObject[] CurrentPool;
    private int[] chances;
    private GameObject[] BasicPool;
    private GameObject[] NutlessPool;

    void Start()
    {
        BasicPool = new GameObject[] {null,nuts,platform,nplatform};
        NutlessPool = new GameObject[] {null,null,platform,platform};
        chances = new int[] {26, 51, 76};
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
        int rnd = Random.Range(0, 101);
        if (rnd < chances[1])
        {
            prevP1 = CurrentPool[1];
            Instantiate(CurrentPool[1], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }
        else if (rnd < chances[2])
        {
            prevP1 = CurrentPool[2];
            Instantiate(CurrentPool[2], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }
        else if (rnd < chances[3])
        {
            prevP1 = CurrentPool[3];
            Instantiate(CurrentPool[3], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }
        else {
            prevP1 = CurrentPool[4];
            Instantiate(CurrentPool[4], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }

        //P2 Spawn Logic
        rnd = Random.Range(0, 101);
        if (prevP1.Equals(platform)|| prevP1.Equals(nplatform) || prevP2.Equals(platform) || prevP2.Equals(nplatform))
        {
            if (rnd < chances[1])
            {
                prevP2 = CurrentPool[1];
                Instantiate(CurrentPool[1], new Vector3(20, 3.5f, 0), Quaternion.identity);
            }
            else if (rnd < chances[2])
            {
                prevP2 = CurrentPool[2];
                Instantiate(CurrentPool[2], new Vector3(20, 3.5f, 0), Quaternion.identity);
            }
            else if (rnd < chances[3])
            {
                prevP2 = CurrentPool[3];
                Instantiate(CurrentPool[3], new Vector3(20, 3.5f, 0), Quaternion.identity);
            }
            else
            {
                prevP2 = CurrentPool[4];
                Instantiate(CurrentPool[4], new Vector3(20, 3.5f, 0), Quaternion.identity);
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
