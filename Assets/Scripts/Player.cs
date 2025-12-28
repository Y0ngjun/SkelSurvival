using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    public float bullet_speed;
    public GameObject bulletPrefab;
    public ParticleSystem Levelup_Particle;
    public int bullet_count = 0;

    private Vector3 StartPos;
    private Vector3 EndPos;
    private Rigidbody rb;
    private Animator anim;
    private float itemTime = 1f;
    private float lastItemTime;

    private float[] PosX01 = { 0.0f };
    private float[] PosX02 = { -0.15f, 0.15f };
    private float[] PosX03 = { -0.15f, 0.0f, 0.15f };
    private float[] PosX04 = { -0.3f, -0.15f, 0.15f, 0.3f };
    private float[] PosX05 = { -0.3f, -0.15f, 0.0f, 0.15f, 0.3f };

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        StartCoroutine(Bullet_Coroutine());
        lastItemTime = Time.time - itemTime;
    }

    void Update()
    {
        if (DeadZone.instance.isGameover)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartPos = EndPos = Vector3.zero;
            rb.velocity = Vector3.zero;

            AnimatorChange("Idle");
        }

        if (Input.GetMouseButton(0))
        {
            EndPos = Input.mousePosition;
            Vector3 Distance = EndPos - StartPos;
            int value = (int)Mathf.Sign(Distance.x);

            if (Vector3.Distance(StartPos, EndPos) > 0.5f)
            {
                if (value == 1)
                {
                    StartPos = new Vector3(EndPos.x - 2.0f, StartPos.y, StartPos.z);
                    rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

                    AnimatorChange("Run");
                }
                else if (value == -1)
                {
                    StartPos = new Vector3(EndPos.x + 2.0f, StartPos.y, StartPos.z);
                    rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);

                    AnimatorChange("Run");
                }
            }
        }
    }

    void LevelUp(GameObject other)
    {
        Destroy(other);
        Levelup_Particle.Play();
        SoundManager.instance.AuidoStart(2);
    }

    void AnimatorChange(string temp)
    {
        if (temp == "Shoot")
        {
            anim.SetTrigger("Shoot");

            return;
        }

        anim.SetBool("Run", false);
        anim.SetBool("Idle", false);

        anim.SetBool(temp, true);
    }

    void Bullet_Make()
    {
        AnimatorChange("Shoot");
        SoundManager.instance.AuidoStart(1);
        float[] bullets = PosX(bullet_count);

        for (int i = 0; i < bullets.Length; i++)
        {
            GameObject go = Instantiate(bulletPrefab,
            new Vector3(transform.position.x + PosX(bullet_count)[i],
            transform.position.y + 0.5f,
            transform.position.z + 1.0f),
            Quaternion.identity);

            Destroy(go, 3.0f);
        }
    }

    private IEnumerator Bullet_Coroutine()
    {
        if (!DeadZone.instance.isGameover)
        {
            Bullet_Make();
            yield return new WaitForSeconds(bullet_speed);

            StartCoroutine(Bullet_Coroutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("ATK_speed") && lastItemTime + itemTime < Time.time)
        {
            lastItemTime = Time.time;

            LevelUp(other.gameObject);
            bullet_speed -= 0.2f;

            if (bullet_speed <= 0.2f)
            {
                bullet_speed = 0.2f;
            }
        }
        else if (other.gameObject.tag == ("ATK_count") && lastItemTime + itemTime < Time.time)
        {
            lastItemTime = Time.time;

            LevelUp(other.gameObject);
            bullet_count++;

            if (bullet_count >= 4)
            {
                bullet_count = 4;
            }
        }
    }

    private float[] PosX(int count)
    {
        switch (count)
        {
            case 0: return PosX01;
            case 1: return PosX02;
            case 2: return PosX03;
            case 3: return PosX04;
            case 4: return PosX05;
            default: return null;
        }
    }
}
