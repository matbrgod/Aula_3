using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posToGo = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, posToGo, followSpeed);

    }
}