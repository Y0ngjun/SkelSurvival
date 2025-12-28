using UnityEngine;

public class Rainbow : MonoBehaviour
{
    public float speed = 1f;
    private Renderer[] rends;
    private float hue = 0f;

    void Start()
    {
        rends = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        hue += Time.deltaTime * speed;
        if (hue > 1f) hue = 0f;

        Color c = Color.HSVToRGB(hue, 1f, 1f);

        foreach (Renderer r in rends)
        {
            r.material.color = c;
        }
    }
}
