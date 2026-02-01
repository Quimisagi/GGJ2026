using UnityEngine;

public class VictoryHandler : MonoBehaviour
{
    [Header("Distance Counter Reference")]
    public DistanceCounter distanceCounter;

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
        Debug.Log("Victory! Distance counter finished!");
    }
}
