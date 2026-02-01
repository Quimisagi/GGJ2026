using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [SerializeField] Transform[] m_spawnPos;
    [SerializeField] GameObject[] m_obj;
    [SerializeField] float m_minRandTime = 2.0f;
    [SerializeField] float m_maxRandTime = 5.0f;

    int m_first;
    int m_second;

    float m_timer = 0.0f;
    float m_randTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        RandPos();
        RandTime();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateObj();
    }
    
    void RandPos()
    {
        m_first = Random.Range(0, m_spawnPos.Length);

        //m_firstÇ∆îÌÇ¡ÇƒÇ¢ÇΩÇÁÇ‡Ç§àÍìx
        do
        {
            m_second = Random.Range(0,m_spawnPos.Length);
        } while (m_second == m_first);

    }
    void RasndSwpanPos()
    {
        m_spawnPos[m_first].position = m_spawnPos[m_first].forward * Random.Range(-1.0f, 1.0f);
        m_spawnPos[m_second].position = m_spawnPos[m_second].forward * Random.Range(-1.0f, 1.0f);
    }
    void RandTime()
    {
        m_randTime = Random.Range(m_minRandTime, m_maxRandTime);
    }
    void GenerateObj()
    {
        m_timer += Time.deltaTime;
        if(m_timer > m_randTime)
        {
            Vector3 offset1 = m_spawnPos[m_first].forward * Random.Range(0, 2);
            Vector3 offset2 = m_spawnPos[m_second].forward * Random.Range(2, 4);


            //ê∂ê¨
            Instantiate(m_obj[Random.Range(0, m_obj.Length)], m_spawnPos[m_first].position + offset1, m_spawnPos[m_first].rotation);
            Instantiate(m_obj[Random.Range(0, m_obj.Length)], m_spawnPos[m_second].position + offset2, m_spawnPos[m_second].rotation);
            m_timer = 0.0f;
            RandPos();
            RandTime();
        }
    }

}
