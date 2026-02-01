using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamenUI : MonoBehaviour
{
    Image m_image;
    public PlayerTransformation playerTransformation;
    [SerializeField] float m_fadeTime = 1.0f;
    bool m_isWear = false;

    float m_timer = 0.0f;
    int m_fadeDirection = 0;
    bool m_isfade = false;

    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        Color color = m_image.color;
        color.a = 0.0f;
        m_image.color = color;

        m_isWear = playerTransformation.GetTransfomaitonFlag();
    }

    // Update is called once per frame
    void Update()
    {
        m_isWear = playerTransformation.GetTransfomaitonFlag();

        if (m_isWear && m_fadeDirection != 1)
        {
            //FadeIn
            m_fadeDirection = 1;
            m_timer = 0.0f;
            m_isfade = true;
        }
        else if(!m_isWear && m_fadeDirection == 1)
        {
            //FadeOut
            m_fadeDirection = -1;
            m_timer = 0.0f;
            m_isfade = true;
        }

        //フェード処理
        if(m_isfade)
        {
            m_timer += Time.deltaTime;
            float fadeTime = Mathf.Clamp01(m_timer / m_fadeTime);

            //FadeInだったらそのままFadeOutだったら1.0から引く
            float alhpa = (m_fadeDirection == 1 ) ? fadeTime : 1.0f - fadeTime;

            Color color = m_image.color;
            color.a = alhpa;
            m_image.color = color;

            if(fadeTime >= 1.0f)
            {
                m_isfade = false;
            }
        }
    }
}
