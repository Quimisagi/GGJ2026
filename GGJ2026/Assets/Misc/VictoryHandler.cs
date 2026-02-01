using UnityEngine;

public class VictoryHandler : MonoBehaviour
{
    [Header("Distance Counter Reference")]
    public DistanceCounter distanceCounter;
    public WSClient wsClient;

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
        Debug.Log("Victory! Distance counter finished!");
        if(wsClient != null)
        {
          wsClient.Victory();
        }
    }
}
