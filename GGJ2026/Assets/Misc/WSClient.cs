using UnityEngine;
using NativeWebSocket;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class WSClient : MonoBehaviour
{
    public DistanceCounter distanceCounter;
    public ObjManager objManager;
    
    // Reference to the PlayerMove script from the Canvas
    private PlayerMove playerMove;
    
    // Updated to TextMeshProUGUI for proper UI text interaction
    public TextMeshProUGUI codeDisplay;

    public int lane = 1;
    public int playerNumber = 1;

    private WebSocket websocket;
    private bool gameStarted = false;
    private string serverUrl = "wss://kamenherodash-server-production.up.railway.app";

    void Awake()
    {
        // Make this object persistent among scenes
        DontDestroyOnLoad(gameObject);
        
        // Subscribe to the sceneLoaded event to find references in new scenes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When the Multiplayer scene loads, automatically find and assign references
        if (scene.name == "Multiplayer")
        {
            distanceCounter = Object.FindFirstObjectByType<DistanceCounter>();
            objManager = Object.FindFirstObjectByType<ObjManager>();
            
            // Automatically find the PlayerMove instance in the scene
            playerMove = Object.FindFirstObjectByType<PlayerMove>();
            
            if (distanceCounter == null) Debug.LogWarning("WSClient: DistanceCounter not found in Multiplayer scene.");
            if (objManager == null) Debug.LogWarning("WSClient: ObjManager not found in Multiplayer scene.");
            if (playerMove == null) Debug.LogWarning("WSClient: PlayerMove not found in Multiplayer scene.");
        }
    }

    async void Start()
    {
        await ConnectToServer();
    }

    private async System.Threading.Tasks.Task ConnectToServer()
    {
        websocket = new WebSocket(serverUrl);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connected to server");
            SendJoinMessage();
            GenerateAndSendCode();
            StartCoroutine(SendLoop());
        };

        websocket.OnError += (e) => { Debug.LogError("WS Error: " + e); };
        
        websocket.OnClose += (e) => 
        { 
            Debug.Log("Connection closed");
            // If we disconnect while in the Multiplayer scene, delete this object
            if (SceneManager.GetActiveScene().name == "Multiplayer")
            {
                Destroy(gameObject);
            }
        };

        websocket.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received: " + message);

            if (message.Contains("\"type\":\"start\""))
            {
                if (!gameStarted)
                {
                    gameStarted = true;
                    Debug.Log("Both players connected, switching to Multiplayer scene!");
                    HandleGameStart();
                }
            }

            try
            {
                var attackMsg = JsonUtility.FromJson<AttackMessage>(message);
                if (attackMsg != null && attackMsg.type == "attack")
                {
                    Debug.Log($"Received attack on lane {attackMsg.lane}");
                    if (objManager != null) objManager.SpawnAtLane(attackMsg.lane);
                }
            }
            catch { }
        };

        await websocket.Connect();
    }

    private void HandleGameStart()
    {
        // Keep the connection active and just change the scene
        SceneManager.LoadScene("Multiplayer");
    }

    private async void SendJoinMessage()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
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

        if (codeDisplay != null)
        {
            codeDisplay.text = code.ToString();
        }

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
                // Sync the local lane variable with the PlayerMove's CurrentLane before sending
                if (playerMove != null)
                {
                    lane = playerMove.CurrentLane;
                }

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
