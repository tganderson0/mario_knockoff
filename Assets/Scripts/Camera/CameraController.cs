using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float toleranceX = 0.0f;
    public float toleranceY = 0.0f;
    public float offsetY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = gameObject.transform.position - player.transform.position;
        Rigidbody2D playerRbd = player.GetComponent<Rigidbody2D>();
        
        //
        if (Mathf.Abs(distance.x) > toleranceX)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), Mathf.Abs(playerRbd.velocity.x) * Time.deltaTime);
        }
        if (Mathf.Abs(distance.y - offsetY) > toleranceY)
        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y + offsetY, transform.position.z), Mathf.Abs(playerRbd.velocity.y) * Time.deltaTime);
        }
    }
}
