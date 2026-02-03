using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] string m_nextSceneName = "GameScene";
    [SerializeField] string multiplayerScene = "MultiplayerWaiting";
    [SerializeField] Image m_fadeImage;
    [SerializeField] float m_fadeTime = 2.0f;
    bool normalGame = false;
    
    AudioSource m_audioSource;
    int m_isFadeIn = 0;
    float m_timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Color color = m_fadeImage.color;
        color.a = 0.0f;
        m_fadeImage.color = color;
        m_fadeImage.enabled = false;

        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
        if(m_isFadeIn == -1)
        {
            if(normalGame)
            SceneManager.LoadScene(m_nextSceneName);
            else
            SceneManager.LoadScene(multiplayerScene);
        }
    }
    public void OnCilckButton()
    {
        m_audioSource.Play();
        m_fadeImage.enabled = true;
        m_isFadeIn = 1;
        normalGame = true;
    }
    public void OnCilckMultiplayerButton()
    {
        m_audioSource.Play();
        m_fadeImage.enabled = true;
        m_isFadeIn = 1;
    }
    void FadeIn()
    {
        if(m_isFadeIn == 1)
        {
            m_timer += Time.deltaTime;
            float fadeTime = Mathf.Clamp01(m_timer/m_fadeTime);

            Color color = m_fadeImage.color;
            color.a = fadeTime;
            m_fadeImage.color = color;

            if(fadeTime >= 1.0f)
            {
                m_isFadeIn = -1;
            }
        }
    }
}
