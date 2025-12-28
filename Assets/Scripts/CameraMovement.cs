using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public Transform character;

    void Update()
    {
        float LerpX = Mathf.Lerp(transform.position.x, character.position.x, speed * Time.deltaTime);
        transform.position = new Vector3(LerpX, transform.position.y, transform.position.z);
    }
}