using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float speed;
    public Slider healthBar;
    public int maxHP = 3;
    public GameObject hitParticle;
    public SpawnManager spawnManager;
    public bool isBoss = false;
    public int HP = 3;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        spawnManager = FindFirstObjectByType<SpawnManager>();
        anim = GetComponent<Animator>();
        maxHP = maxHP * DeadZone.instance.level; HP = maxHP;
    }

    void Update()
    {
        if (!isDead && !DeadZone.instance.isGameover)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            SoundManager.instance.AuidoStart(0);

            if (!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
            }

            HP--;

            Instantiate(hitParticle, other.gameObject.transform.position, Quaternion.identity);
            healthBar.value = (float)HP / maxHP;
            Destroy(other.transform.parent.gameObject);

            if (HP <= 0 && !isDead)
            {
                DeadZone.instance.AddScore(100);

                if (isBoss)
                {
                    DeadZone.instance.AddScore(900);
                }

                spawnManager.bossCount++;
                if (spawnManager.bossCount >= 10)
                {
                    spawnManager.SpawnBoss();
                    spawnManager.bossCount -= 10;
                    DeadZone.instance.LevelUp();
                }

                isDead = true;
                anim.SetTrigger("death");
                Destroy(GetComponent<Rigidbody>());
                GetComponent<CapsuleCollider>().enabled = false;
                healthBar.gameObject.SetActive(false);
                Destroy(gameObject, 2f);
            }
        }
    }
}
