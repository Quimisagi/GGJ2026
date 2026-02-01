using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [SerializeField] Image m_clearImage;
    [SerializeField] GameObject m_titleButton;

    [SerializeField] bool m_isFadeIn = false;
    float m_timer = 0.0f;
    float m_fadeTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_titleButton.SetActive(false);

        Color color = m_clearImage.color;
        color.a = 0.0f;
        m_clearImage.color = color;

    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
    }
    public void TitleButton()
    {
        SceneManager.LoadScene("TitleScene");
        
    }
    void FadeIn()
    {
        if (m_isFadeIn)
        {
            m_timer += Time.deltaTime;
            float fadeTime = Mathf.Clamp01(m_timer / m_fadeTime);

            Color color = m_clearImage.color;
            color.a = fadeTime;
            m_clearImage.color = color;

            if (fadeTime >= 1.0f)
            {
                m_titleButton.SetActive(true);
            }
        }
    }
    public void SetIsFadeIn(bool fadeIn)
    {
        m_isFadeIn = fadeIn;
    }
}
