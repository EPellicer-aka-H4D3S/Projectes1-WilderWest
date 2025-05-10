using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameObject prevP1 = null;
    private GameObject prevP2;
    private int bisonCounter;
    private int bisonCurrent;
    private bool banditActive = false;
    private bool cactusSpawn = true;
    private int score = 0;


    [Header("Prefabs")]
    public GameObject nuts;
    public GameObject platform;
    public GameObject nplatform;
    public GameObject bison;
    public GameObject bandit;
    public GameObject cactus;

    private GameObject[] CurrentPool;
    private int[] Chances;
    private GameObject[] BasicPool;
    private GameObject[] NutlessPool;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI scoreDisplay;

    void Start()
    {
        BasicPool = new GameObject[] {null,nuts,platform,nplatform};
        NutlessPool = new GameObject[] {null,null,platform,platform};
        Chances = new int[] {26, 51, 76};
        bisonCounter = UnityEngine.Random.Range(4, 8);
        bisonCurrent = bisonCounter;

        prevP2 = nuts.GetComponent<GameObject>();

        InvokeRepeating(nameof(PlatformSpawner), 4.0f, 1.0f);
        InvokeRepeating(nameof(EnemySpawner), 10.0f, 4.0f);
        InvokeRepeating(nameof(CactusSpawner), 4.0f, 1.0f);
    }
    
    void PlatformSpawner()
    {
        //Bandit pool swap
        if (banditActive)
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
        if (!banditActive)
        {
            if (bisonCurrent > 0)
            {
                if (playerController.transform.position.y > 5f && !(prevP2 == null || prevP2.Equals(nuts)))
                {
                    bisonCurrent--;
                    Instantiate(bison, new Vector3(20, 5.5f, 0), Quaternion.identity);
                }
                else if (playerController.transform.position.y > 0f && !((prevP1 == null || prevP1.Equals(nuts))))
                {
                    bisonCurrent--;
                    Instantiate(bison, new Vector3(20, 0.5f, 0), Quaternion.identity);
                }
                else
                {
                    bisonCurrent--;
                    Instantiate(bison, new Vector3(20, -4.5f, 0), Quaternion.identity);
                }
            }
            else
            {
                bisonCounter = UnityEngine.Random.Range(4, 8);
                bisonCurrent = bisonCounter;
                banditActive = true;
                if (playerController.transform.position.y > 5f && !(prevP2 == null || prevP2.Equals(nuts)))
                {
                    bisonCurrent--;
                    Instantiate(bandit, new Vector3(20, 5.5f, 0), Quaternion.identity);
                }
                else if (playerController.transform.position.y > 0f && !((prevP1 == null || prevP1.Equals(nuts))))
                {
                    bisonCurrent--;
                    Instantiate(bandit, new Vector3(20, 0.5f, 0), Quaternion.identity);
                }
                else
                {
                    bisonCurrent--;
                    Instantiate(bandit, new Vector3(20, -4.5f, 0), Quaternion.identity);
                }
            }
        }
    }

    void CactusSpawner()
    {
        if (cactusSpawn)
        {
            cactusSpawn = !cactusSpawn;
            if (!(prevP1 == null || prevP1.Equals(nuts) || prevP1.Equals(nplatform)) && !(prevP2 == null || prevP2.Equals(nuts) || prevP2.Equals(nplatform)))
            {
                int rnd = UnityEngine.Random.Range(0, 10);
                switch (rnd)
                {
                    case 1:
                        Instantiate(cactus, new Vector3(18, 5f, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(cactus, new Vector3(20, 5f, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(cactus, new Vector3(22, 5f, 0), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(cactus, new Vector3(18, 0f, 0), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(cactus, new Vector3(20, 0f, 0), Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(cactus, new Vector3(22, 0f, 0), Quaternion.identity);
                        break;
                    case 7:
                        Instantiate(cactus, new Vector3(18, -5f, 0), Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(cactus, new Vector3(20, -5f, 0), Quaternion.identity);
                        break;
                    case 9:
                        Instantiate(cactus, new Vector3(22, -5f, 0), Quaternion.identity);
                        break;
                }
            }

            else if (!((prevP1 == null || prevP1.Equals(nuts) || prevP1.Equals(nplatform))))
            {
                int rnd = UnityEngine.Random.Range(0, 7);
                switch (rnd)
                {
                    case 1:
                        Instantiate(cactus, new Vector3(18, 0f, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(cactus, new Vector3(20, 0f, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(cactus, new Vector3(22, 0f, 0), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(cactus, new Vector3(18, -5f, 0), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(cactus, new Vector3(20, -5f, 0), Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(cactus, new Vector3(22, -5f, 0), Quaternion.identity);
                        break;
                }
            }
            else if (!((prevP2 == null || prevP2.Equals(nuts) || prevP2.Equals(nplatform))))
            {
                int rnd = UnityEngine.Random.Range(0, 7);
                switch (rnd)
                {
                    case 1:
                        Instantiate(cactus, new Vector3(18, 5f, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(cactus, new Vector3(20, 5f, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(cactus, new Vector3(22, 5f, 0), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(cactus, new Vector3(18, -5f, 0), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(cactus, new Vector3(20, -5f, 0), Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(cactus, new Vector3(22, -5f, 0), Quaternion.identity);
                        break;
                }
            }
            else
            {
                int rnd = UnityEngine.Random.Range(0, 4);
                switch (rnd)
                {
                    case 1:
                        Instantiate(cactus, new Vector3(18, -5f, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(cactus, new Vector3(20, -5f, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(cactus, new Vector3(22, -5f, 0), Quaternion.identity);
                        break;
                }
            }
        }else cactusSpawn = !cactusSpawn;
    }

    public void KillPlayer()
    {
        if (score > 0)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            score = score - 30;
        }
    }

    public void Accelerate()
    {
        banditActive = false;
        if (Time.timeScale < 3.0f)
        {
            Time.timeScale = Time.timeScale + 0.2f;
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreDisplay.text = "x" + score.ToString();
    }

    void Update()
    {

    }
}
