using UnityEngine;
using TMPro;

public enum ItemState { ATK_count, ATK_speed };

public class Item : MonoBehaviour
{
    public GameObject[] Cubes;
    public Material[] materials;
    public TextMeshProUGUI[] texts;
    public float speed;

    private ItemState[] items = new ItemState[2];

    void Start()
    {
        items[0] = (ItemState)Random.Range(0, 2);
        items[1] = items[0] == ItemState.ATK_speed ?
            ItemState.ATK_count : ItemState.ATK_speed;

        for (int i = 0; i < Cubes.Length; i++)
        {
            Cubes[i].GetComponent<Renderer>().material = materials[(int)items[i]];
            Cubes[i].tag = items[i].ToString();
            texts[i].text = ((int)items[i] == 0) ? "ATTACK\nCOUNT" : "ATTACK\nSPEED";
        }
    }

    void Update()
    {
        if (DeadZone.instance.isGameover)
        {
            return;
        }

        transform.position -= transform.forward * speed * Time.deltaTime;
    }
}
