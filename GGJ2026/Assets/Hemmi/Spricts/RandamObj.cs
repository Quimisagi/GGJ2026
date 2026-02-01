using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandamObj : MonoBehaviour
{
    Vector3 m_direc;
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_limitTime = 10.0f;

    float m_timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_direc = transform.forward;
        transform.position += m_direc * m_speed * Time.deltaTime;

        //§ŒÀŽžŠÔ‚ÅŽ©•ª‚ðíœ
        LimitDestory();
    }
    void LimitDestory()
    {
        m_timer += Time.deltaTime;
        if(m_timer > m_limitTime)
        {
            Destroy(gameObject);
        }
    }
}
