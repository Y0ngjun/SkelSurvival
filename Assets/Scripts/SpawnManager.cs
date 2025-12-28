using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Monster monsterPrefab;
    public Monster bossPrefab;
    public Item itemPrefab;
    public float spawnPointZ;
    public int bossCount = 0;

    void Start()
    {
        StartCoroutine(Spwan_Coroutine());
        StartCoroutine(Spwan_item());
    }

    IEnumerator Spwan_Coroutine()
    {
        float xPos = Random.Range(-3f, 3f);
        Instantiate(monsterPrefab, new Vector3(xPos, 0.5f, spawnPointZ), Quaternion.Euler(0, 180f, 0));
        yield return new WaitForSeconds(Random.Range(3f - 0.5f * DeadZone.instance.level, 5f - 0.5f * DeadZone.instance.level));
        StartCoroutine(Spwan_Coroutine());
    }

    IEnumerator Spwan_item()
    {
        Instantiate(itemPrefab, new Vector3(0, 0, spawnPointZ), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        StartCoroutine(Spwan_item());
    }

    public void SpawnBoss()
    {
        Monster boss = Instantiate(bossPrefab, new Vector3(0.0f, 0.5f, spawnPointZ), Quaternion.Euler(0, 180f, 0))
            .GetComponent<Monster>();
        boss.isBoss = true;
        boss.maxHP = 100 * DeadZone.instance.level;
        boss.HP = boss.maxHP;
    }
}
