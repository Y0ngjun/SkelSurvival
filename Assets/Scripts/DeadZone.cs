using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
{
    public static DeadZone instance;
    public Slider HPUI;
    public GameObject gameoverUI;
    public int playerHP = 10;
    public bool isGameover = false;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI levelUI;
    public int level = 1;

    private int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            playerHP--;
            if(other.GetComponent<Monster>().isBoss)
            {
                playerHP -= 2;
            }

            HPUI.value = (float)playerHP / 10;

            if(playerHP <= 0)
            {
                gameoverUI.SetActive(true);
                isGameover = true;
            }
        }

        Destroy(other.gameObject);
    }

    public void AddScore(int add)
    {
        score += add;

        scoreUI.text = "score: " + score;
    }

    public void LevelUp()
    {
        level += 1;

        if(level > 5)
        {
            level = 5;
        }

        levelUI.text = "level: " + level;
    }
}
