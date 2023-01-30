using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> cells = new List<GameObject>();
    [SerializeField] private List<GameObject> boss = new List<GameObject>();

    private int stage = 1;
    private float timer = 100.0f;
    private bool isSpawnBoss = false;

    public void InitSpawner(int stage)
    {
        StopAllCoroutines();
        this.stage = stage;
        timer = this.stage == 1 ? 80.0f : 120.0f;
        isSpawnBoss = false;

        StartCoroutine("SpawnPatternStage" + this.stage.ToString());
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        GameManager.Instance.uiManager.SetTimerText(timer);

        if (timer <= 0)
        {
            timer = 0;
            if (isSpawnBoss == false)
            {
                BossSpawn();
            }
        }
    }

    private void BossSpawn()
    {
        isSpawnBoss = true;

        Instantiate(boss[stage - 1], new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);

        StopAllCoroutines();
    }

    private IEnumerator SpawnPatternStage1()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(nameof(Bacteria));
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(nameof(SpawnWhiteBloodCell));
            StartCoroutine(nameof(CancerCell));
            StartCoroutine(nameof(Bacteria));
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(nameof(SpawnWhiteBloodCell));
            StartCoroutine(nameof(Virus));
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(nameof(SpawnBloodCell));
            StartCoroutine(nameof(Virus));
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(nameof(SpawnWhiteBloodCell));
            StartCoroutine(nameof(Bacteria));
            StartCoroutine(nameof(Virus));
        }
    }

    private IEnumerator SpawnPatternStage2()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(nameof(Bacteria));
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(nameof(SpawnBloodCell));
            StartCoroutine(nameof(CancerCell));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(nameof(SpawnWhiteBloodCell));
            StartCoroutine(nameof(SuperBacteria));
            StartCoroutine(nameof(Virus));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(nameof(SpawnBloodCell));
            StartCoroutine(nameof(Virus));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(nameof(SpawnWhiteBloodCell));
            StartCoroutine(nameof(CancerCell));
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(nameof(SpawnBloodCell));
            StartCoroutine(nameof(Bacteria));
            StartCoroutine(nameof(SuperVirus));
            yield return new WaitForSeconds(3.5f);
            StartCoroutine(nameof(Virus));
            StartCoroutine(nameof(TumorCell));
        }
    }

    private IEnumerator Bacteria()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4.0f, 4.0f), 6.0f, 0.0f);

        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemies[0], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

    }

    private IEnumerator CancerCell()
    {
        Vector3 spawnPos = new Vector3(0, 6.0f, 0.0f);

        Instantiate(enemies[1], spawnPos, Quaternion.identity);

        yield break;
    }
    private IEnumerator SuperBacteria()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4.0f, 4.0f), 6.0f, 0.0f);

        Instantiate(enemies[2], spawnPos, Quaternion.identity);

        yield break;
    }
    private IEnumerator SuperVirus()
    {
        Vector3 spawnPos = new Vector3(Random.Range(2.0f, 4.0f), 6.0f, 0.0f);

        Instantiate(enemies[3], spawnPos, Quaternion.identity);

        yield break;
    }
    private IEnumerator Virus()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 fixedSpawnPos = new Vector3(1.5f * Random.Range(-2, 3), 6.0f, 0.0f);
            Instantiate(enemies[5], fixedSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
        yield break;
    }
    private IEnumerator TumorCell()
    {
        Vector3 spawnPos = new Vector3(Random.Range(4.0f, 2.0f), 6.0f, 0.0f);

        for (int i = -1; i <= 1; i += 2)
        {
            Vector3 fixedSpawnPos = new Vector3(spawnPos.x * i, 6.0f, 0.0f);
            Instantiate(enemies[4], fixedSpawnPos, Quaternion.identity);
        }
        yield break;
    }

    private IEnumerator SpawnBloodCell()
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-3.25f, 3.25f), 6.0f, 0.0f);

            Instantiate(cells[0], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(0.65f);
        }
    }

    private IEnumerator SpawnWhiteBloodCell()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-3.25f, 3.25f), 6.0f, 0.0f);

        Instantiate(cells[1], spawnPos, Quaternion.identity);

        yield break;
    }
}