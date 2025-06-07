using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameObject prevP1;
    private GameObject prevP2;
    private int enemyCounter;
    private int enemyCurrent;
    private bool banditActive = false;
    private bool cactusSpawn = true;
    private int score = 0;

    [Header("Prefabs")]
    public GameObject empty;
    public GameObject nuts;
    public GameObject platform;
    public GameObject nplatform;
    public GameObject bison;
    public GameObject bulture;
    public GameObject bandit;
    public GameObject cactus;

    private GameObject[] CurrentPool;
    [SerializeField] private int[] Chances = new int[] { 26, 51, 76 };
    private GameObject[] BasicPool;
    private GameObject[] NutlessPool;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    private DeathMenu deathMenu;
    private AudioManager audioManager;

    void Start()
    {
        BasicPool = new GameObject[] {empty,nuts,platform,nplatform};
        NutlessPool = new GameObject[] {empty,empty,platform,platform};
        enemyCounter = UnityEngine.Random.Range(5, 8);
        enemyCurrent = enemyCounter;

        deathMenu = gameObject.GetComponent<DeathMenu>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        prevP1 = empty.GetComponent<GameObject>();
        prevP2 = nuts.GetComponent<GameObject>();

        InvokeRepeating(nameof(EnviromentSpawner), 4.0f, 1.0f);
        InvokeRepeating(nameof(EnemySpawner), 10.0f, 4.0f);
    }
    
    void EnviromentSpawner()
    {
        int temp = PlatformSpawner();
        prevP2 = CurrentPool[temp / 10];
        prevP1 = CurrentPool[temp % 10];
        CactusSpawner();
    }

    int PlatformSpawner()
    {
        //P1 Spawn Logic
        int rnd = UnityEngine.Random.Range(1, 101);
        int temp;
        if (rnd < Chances[0])
        {
            temp = 0;
            Instantiate(CurrentPool[0], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }
        else if (rnd < Chances[1])
        {
            temp = 1;
            Instantiate(CurrentPool[1], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }
        else if (rnd < Chances[2])
        {
            temp = 2;
            Instantiate(CurrentPool[2], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }
        else {
            temp = 3;
            Instantiate(CurrentPool[3], new Vector3(20, -1.5f, 0), Quaternion.identity);
        }

        //P2 Spawn Logic
        rnd = UnityEngine.Random.Range(1, 101);
        if (prevP1.Equals(platform) || prevP1.Equals(nplatform) || prevP2.Equals(platform) || prevP2.Equals(nplatform))
        {
            if (rnd < Chances[0])
            {
                Instantiate(CurrentPool[0], new Vector3(20, 3.5f, 0), Quaternion.identity);
                temp = temp + 00;
            }
            else if (rnd < Chances[1])
            {
                Instantiate(CurrentPool[1], new Vector3(20, 3.5f, 0), Quaternion.identity);
                temp = temp + 10;
            }
            else if (rnd < Chances[2])
            {
                Instantiate(CurrentPool[2], new Vector3(20, 3.5f, 0), Quaternion.identity);
                temp = temp + 20;
            }
            else
            {
                Instantiate(CurrentPool[3], new Vector3(20, 3.5f, 0), Quaternion.identity);
                temp = temp + 30;
            }
        }

        return temp;
    }

    void EnemySpawner()
    {
        if (!banditActive)
        {
            if (enemyCurrent > 0)
            {
                //Spawn enemy
                GameObject enemy;
                if (UnityEngine.Random.Range(1,3)==1)
                {
                    enemy  = bison;
                }
                else
                {
                    enemy = bulture;
                }

                if (playerController.transform.position.y > 5f && (prevP2.Equals(platform) || prevP2.Equals(nplatform)))
                {
                    enemyCurrent--;
                    Instantiate(enemy, new Vector3(20, 5.5f, 0), Quaternion.identity);
                }
                else if (playerController.transform.position.y > 0f && (prevP1.Equals(platform) || prevP1.Equals(nplatform)))
                {
                    enemyCurrent--;
                    Instantiate(enemy, new Vector3(20, 0.5f, 0), Quaternion.identity);
                }
                else
                {
                    enemyCurrent--;
                    Instantiate(enemy, new Vector3(20, -4.5f, 0), Quaternion.identity);
                }
            }
            else
            {
                //Spawn bandit
                enemyCounter = UnityEngine.Random.Range(4, 8);
                enemyCurrent = enemyCounter;
                banditActive = true;
                if (playerController.transform.position.y > 5f && (prevP2.Equals(platform) || prevP2.Equals(nplatform)))
                {
                    Instantiate(bandit, new Vector3(20, 5.5f, 0), Quaternion.identity);
                }
                else if (playerController.transform.position.y > 0f && (prevP1.Equals(platform) || prevP1.Equals(nplatform)))
                {
                    Instantiate(bandit, new Vector3(20, 0.5f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(bandit, new Vector3(20, -4.5f, 0), Quaternion.identity);
                }
            }
        }
    }

    void CactusSpawner()
    {
        if (cactusSpawn)
        {
            if (prevP1.Equals(platform) && prevP2.Equals(platform))
            {
                int rnd = UnityEngine.Random.Range(1, 10);
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

            else if (prevP1.Equals(platform))
            {
                int rnd = UnityEngine.Random.Range(1, 7);
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

            else if (prevP2.Equals(platform))
            {
                int rnd = UnityEngine.Random.Range(1, 7);
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
                int rnd = UnityEngine.Random.Range(1, 4);
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
        }
        cactusSpawn = !cactusSpawn;
    }

    public void KillPlayer()
    {
        if (score >= 0)
        {
            score = score - 30;
            audioManager.playEffect(audioManager.hit);
            scoreDisplay.text = "x" + score.ToString();
        }
        if (score < 0)
        {
            Time.timeScale = 0.0f;
            audioManager.playEffect(audioManager.death);
            deathMenu.ToggleDeathUI();
        }
    }

    public void Accelerate()
    {
        banditActive = false;
        if (Time.timeScale < 3.0f)
        {
            Time.timeScale = Time.timeScale + 0.15f;
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreDisplay.text = "x" + score.ToString();
    }

    void Update()
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
    }
}
