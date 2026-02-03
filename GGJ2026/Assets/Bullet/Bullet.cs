using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 pos;
    public float speed = 1.0f;
    public float time = 1.0f;
    public float removeTime = 20.0f;
    private float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
       pos = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        time += elapsedTime;

        BulletMove();

        if(time >= removeTime)
        {
            BulletRemove();
        }

    }

    void BulletMove()
    {
        pos.z += speed * elapsedTime;
        transform.position = pos;
    }

    void BulletRemove()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
