using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnType;
    public float spawnCooldown;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            GameObject newObj = Instantiate(spawnType, transform);
            newObj.transform.SetParent(null);
            timer = spawnCooldown;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
