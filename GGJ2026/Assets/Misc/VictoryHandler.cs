using UnityEngine;

public class VictoryHandler : MonoBehaviour
{
    [Header("Distance Counter Reference")]
    public DistanceCounter distanceCounter;

    [SerializeField] ClearUI m_clearUI;

    void Start()
    {
        if (distanceCounter != null)
        {
            distanceCounter.OnCounterFinished.AddListener(OnVictory);
        }
        else
        {
            Debug.LogWarning("DistanceCounter reference not set on VictoryHandler.");
        }
    }

    void OnVictory()
    {
        SoundManager.Instance.PlaySound("Victory");
        m_clearUI.SetIsFadeIn(true);
        Debug.Log("Victory! Distance counter finished!");
    }
}