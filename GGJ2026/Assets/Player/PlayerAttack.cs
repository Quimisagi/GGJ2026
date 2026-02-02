using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMove player;
    public PlayerTransformation playerTransformation;
    private Vector3 pos;
    private Quaternion quaternion;
    public Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            pos = player.GetPlayerPos();
            quaternion = transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            pos = player.GetPlayerPos();
            quaternion = transform.rotation;
        }

        if (playerTransformation.GetTransfomaitonFlag())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SoundManager.Instance.PlaySound("Bullet");
                Instantiate(bullet, pos, quaternion);
            }
        }
    }
}
