using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private GameObject prevP1;
    private GameObject prevP2;
    private GameObject[] CurrentPool;
    private int enemyCounter;
    private bool banditActive = false;
    private bool cactusSpawn = true;
    private int score = 0;
    private int totalScore = 0;

    [SerializeField] private GameData d;
    
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI textFinalScore;
    [SerializeField] private TextMeshProUGUI textCurrentScore;
    [SerializeField] private ParticleSystem damageParticle;
    private DeathMenu deathMenu;
    private AudioManager audioManager;

    void Start()
    {
        enemyCounter = UnityEngine.Random.Range(5, 8);

        deathMenu = gameObject.GetComponent<DeathMenu>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        prevP1 = d.empty;
        prevP2 = d.nuts;

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
        if (rnd < d.Chances[0])
        {
            temp = 0;
            Instantiate(CurrentPool[0], new Vector3(d.spawningLatitude, d.firstPlatformHeight, 0), Quaternion.identity);
        }
        else if (rnd < d.Chances[1])
        {
            temp = 1;
            Instantiate(CurrentPool[1], new Vector3(d.spawningLatitude, d.firstPlatformHeight, 0), Quaternion.identity);
        }
        else if (rnd < d.Chances[2])
        {
            temp = 2;
            Instantiate(CurrentPool[2], new Vector3(d.spawningLatitude, d.firstPlatformHeight, 0), Quaternion.identity);
        }
        else {
            temp = 3;
            Instantiate(CurrentPool[3], new Vector3(d.spawningLatitude, d.firstPlatformHeight, 0), Quaternion.identity);
        }

        //P2 Spawn Logic
        rnd = UnityEngine.Random.Range(1, 101);
        if (prevP1.Equals(d.platform) || prevP1.Equals(d.nplatform) || prevP2.Equals(d.platform) || prevP2.Equals(d.nplatform))
        {
            if (rnd < d.Chances[0])
            {
                Instantiate(CurrentPool[0], new Vector3(d.spawningLatitude, d.secondPlatformHeight, 0), Quaternion.identity);
                temp = temp + 00;
            }
            else if (rnd < d.Chances[1])
            {
                Instantiate(CurrentPool[1], new Vector3(d.spawningLatitude, d.secondPlatformHeight, 0), Quaternion.identity);
                temp = temp + 10;
            }
            else if (rnd < d.Chances[2])
            {
                Instantiate(CurrentPool[2], new Vector3(d.spawningLatitude, d.secondPlatformHeight, 0), Quaternion.identity);
                temp = temp + 20;
            }
            else
            {
                Instantiate(CurrentPool[3], new Vector3(d.spawningLatitude, d.secondPlatformHeight, 0), Quaternion.identity);
                temp = temp + 30;
            }
        }

        return temp;
    }

    void EnemySpawner()
    {
        if (!banditActive)
        {
            if (enemyCounter > 0)
            {
                //Spawn enemy
                GameObject enemy;
                if (UnityEngine.Random.Range(1,3)==1)
                {
                    enemy  = d.bison;
                }
                else
                {
                    enemy = d.bulture;
                }

                if (playerController.transform.position.y > 5f && (prevP2.Equals(d.platform) || prevP2.Equals(d.nplatform)))
                {
                    enemyCounter--;
                    Instantiate(enemy, new Vector3(d.spawningLatitude, d.enemyUp, 0), Quaternion.identity);
                }
                else if (playerController.transform.position.y > 0f && (prevP1.Equals(d.platform) || prevP1.Equals(d.nplatform)))
                {
                    enemyCounter--;
                    Instantiate(enemy, new Vector3(d.spawningLatitude, d.enemyMiddle, 0), Quaternion.identity);
                }
                else
                {
                    enemyCounter--;
                    Instantiate(enemy, new Vector3(d.spawningLatitude, d.enemyDown, 0), Quaternion.identity);
                }
            }
            else
            {
                //Spawn bandit
                enemyCounter = UnityEngine.Random.Range(5, 8);
                banditActive = true;
                if (playerController.transform.position.y > 5f && (prevP2.Equals(d.platform) || prevP2.Equals(d.nplatform)))
                {
                    Instantiate(d.bandit, new Vector3(d.spawningLatitude, d.enemyUp, 0), Quaternion.identity);
                }
                else if (playerController.transform.position.y > 0f && (prevP1.Equals(d.platform) || prevP1.Equals(d.nplatform)))
                {
                    Instantiate(d.bandit, new Vector3(d.spawningLatitude, d.enemyMiddle, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(d.bandit, new Vector3(d.spawningLatitude, d.enemyDown, 0), Quaternion.identity);
                }
            }
        }
    }

    void CactusSpawner()
    {
        if (cactusSpawn)
        {
            int height = -5;
            int horzPos;
            
            int hRnd = UnityEngine.Random.Range(-1,2);
            horzPos = d.spawningLatitude + hRnd * 2;
            
            if (prevP1.Equals(d.platform) && prevP2.Equals(d.platform))
            {
                int vRnd = UnityEngine.Random.Range(1, 4);
                
                switch (vRnd)
                {
                    case 1:
                        height = -5;
                        break;
                    case 2:
                        height = 0;
                        break;
                    case 3:
                        height = 5;
                        break;
                }

                Instantiate(d.cactus, new Vector3(horzPos, height, 0), Quaternion.identity);
            }

            else if (prevP1.Equals(d.platform))
            {
                int vRnd = UnityEngine.Random.Range(1, 3);
                
                switch (vRnd)
                {
                    case 1:
                        height = -5;
                        break;
                    case 2:
                        height = 0;
                        break;
                }

                Instantiate(d.cactus, new Vector3(horzPos, height, 0), Quaternion.identity);
            }

            else if (prevP2.Equals(d.platform))
            {
                int vRnd = UnityEngine.Random.Range(1, 3);
                
                switch (vRnd)
                {
                    case 1:
                        height = 5;
                        break;
                    case 2:
                        height = -5;
                        break;
                }

                Instantiate(d.cactus, new Vector3(horzPos, height, 0), Quaternion.identity);
            }

            else
            {
                Instantiate(d.cactus, new Vector3(horzPos, height, 0), Quaternion.identity);
            }
        }
        cactusSpawn = !cactusSpawn;
    }

    public void KillPlayer()
    {
        if (score >= 0)
        {
            score -= Mathf.RoundToInt(d.hitPenaltyBase * Time.timeScale);
            damageParticle.Play();
            audioManager.playEffect(audioManager.hit);
            
        }
        if (score < 0)
        {
            Time.timeScale = 0.0f;
            audioManager.playEffect(audioManager.death);
            deathMenu.ToggleDeathUI();
            textFinalScore.text = totalScore.ToString();
        }
        if (score <= 0)
        {
            scoreDisplay.text = "x0";
        }
        else scoreDisplay.text = "x" + score.ToString();
    }

    public void Accelerate()
    {
        banditActive = false;
        if (Time.timeScale < d.timeScalingMax)
        {
            Time.timeScale = Time.timeScale + d.timeScaling;
        }
    }

    public void UpdateScore()
    {
        score++;
        totalScore++;
        textCurrentScore.text = totalScore.ToString();
        scoreDisplay.text = "x" + score.ToString();
    }

    void Update()
    {
        //Bandit pool swap
        if (banditActive)
        {
            CurrentPool = d.NutlessPool;
        }
        else
        {
            CurrentPool = d.BasicPool;
        }
    }
}
