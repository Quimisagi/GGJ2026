using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariEnemyMove : MonoBehaviour
{
    Vector3 pos;
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var elapsedTime = Time.deltaTime;
        
        pos.z -= speed * elapsedTime;

        transform.position = pos;
    }
}
