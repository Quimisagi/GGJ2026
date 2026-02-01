using UnityEngine;
using NativeWebSocket;
using System.Collections;

public class WSClient : MonoBehaviour
{
    public DistanceCounter distanceCounter;
    public int lane = 1;
    public int playerNumber = 1;

    private WebSocket websocket;
    private bool gameStarted = false;

    async void Start()
    {
        websocket = new WebSocket("wss://kamenherodash-server-production.up.railway.app");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connected to server");
            SendJoinMessage();
            GenerateAndSendCode();
            StartCoroutine(SendLoop());
        };

        websocket.OnError += (e) => { Debug.LogError("WS Error: " + e); };
        websocket.OnClose += (e) => { Debug.Log("Connection closed"); };

        websocket.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received: " + message);

            if (message.Contains("\"type\":\"start\""))
            {
                gameStarted = true;
                Debug.Log("Both players connected, game starting!");
            }

            try
            {
                var attackMsg = JsonUtility.FromJson<AttackMessage>(message);
                if (attackMsg != null && attackMsg.type == "attack")
                {
                    Debug.Log($"Received attack on lane {attackMsg.lane}");
                }
            }
            catch { }
        };

        await websocket.Connect();
    }

    private async void SendJoinMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            string joinJson = JsonUtility.ToJson(new JoinMessage
            {
                type = "join",
                player = playerNumber
            });
            await websocket.SendText(joinJson);
        }
    }

    void GenerateAndSendCode()
    {
        int code = Random.Range(100, 1000);
        Debug.Log($"Generated Code: {code}");

        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(new CodeMessage
            {
                type = "code",
                value = code
            });
            _ = websocket.SendText(json);
            Debug.Log("Code sent to server");
        }
    }

    IEnumerator SendLoop()
    {
        while (true)
        {
            if (websocket != null && websocket.State == WebSocketState.Open && distanceCounter != null && gameStarted)
            {
                string json = JsonUtility.ToJson(new PlayerDataMessage
                {
                    type = "distance",
                    value = distanceCounter.distanceRemaining,
                    initialDistance = distanceCounter.initialDistance,
                    lane = lane
                });

                _ = websocket.SendText(json);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (websocket != null)
        {
            websocket.DispatchMessageQueue();
        }
#endif
    }

    async void OnApplicationQuit()
    {
        if (websocket != null) await websocket.Close();
    }

    public void Victory()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(new ResultMessage
            {
                type = "result",
                outcome = "victory"
            });
            _ = websocket.SendText(json);
            Debug.Log("Victory message sent");
        }
    }

    public void Fail()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(new ResultMessage
            {
                type = "result",
                outcome = "fail"
            });
            _ = websocket.SendText(json);
            Debug.Log("Fail message sent");
        }
    }

    [System.Serializable] public class JoinMessage { public string type; public int player; }
    [System.Serializable] public class PlayerDataMessage { public string type; public float initialDistance; public float value; public int lane; }
    [System.Serializable] public class AttackMessage { public string type; public int lane; }
    [System.Serializable] public class ResultMessage { public string type; public string outcome; }
    [System.Serializable] public class CodeMessage { public string type; public int value; }
}
