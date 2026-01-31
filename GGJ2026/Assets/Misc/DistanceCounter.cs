using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DistanceCounter : MonoBehaviour
{
    [Header("Distance Settings")]
    public float distanceRemaining = 10f;
    public bool counterIsRunning = false;

    [Header("UI Elements")]
    public TMP_Text distanceText;

    [Header("Events")]
    public UnityEvent OnCounterFinished;

    void Start()
    {
        counterIsRunning = true;
        UpdateDistanceUI();
    }

    void Update()
    {
        if (!counterIsRunning) return;

        if (distanceRemaining > 0)
        {
            distanceRemaining -= Time.deltaTime;
            if (distanceRemaining < 0) distanceRemaining = 0;

            UpdateDistanceUI();
        }
        else
        {
            counterIsRunning = false;
            UpdateDistanceUI();
            OnCounterFinished.Invoke();
        }
    }

    void UpdateDistanceUI()
    {
        distanceText.text = string.Format("{0:0.0} m", distanceRemaining);
    }
}
