using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 pos;
    public float MaxPos = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSideMove();
    }

    void PlayerSideMove()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            pos.x -= 5.0f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            pos.x += 5.0f;
        }

        pos.x = Mathf.Clamp(pos.x, -MaxPos, MaxPos);

        transform.position = pos;
    }

    public Vector3 GetPlayerPos()
    {
        return pos;
    }
}
