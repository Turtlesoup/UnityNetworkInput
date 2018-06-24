using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Events;

public class NetworkInputClient : MonoBehaviour
{
    [SerializeField] private string ipAddress = "127.0.0.1";
    [SerializeField] private int port = 4444;

    [System.Serializable] public class OnNetworkInputKeyEvent : UnityEvent<KeyCode, NetworkInputMessages.InputKeyMessage.KeyState> { }
    [SerializeField] public OnNetworkInputKeyEvent OnNetworkInputKey;

    [System.Serializable] public class OnNetworkMousePositionEvent : UnityEvent<Vector3> { }
    [SerializeField] public OnNetworkMousePositionEvent OnNetworkMousePosition;

    private NetworkClient client;

    private void Start()
    {
        SetupClient();
    }

    public void SetupClient()
    {
        client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnClientConnected);
        client.RegisterHandler(NetworkInputMessages.InputKeyMessage.MessageType, ReceiveInputKeyMessage);
        client.RegisterHandler(NetworkInputMessages.MousePositionMessage.MessageType, ReceiveMouseMovementMessage);
        client.Connect(ipAddress, port);
    }

    public void OnClientConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }

    #region Messages

    public void ReceiveInputKeyMessage(NetworkMessage networkMessage)
    {
        NetworkInputMessages.InputKeyMessage message = networkMessage.ReadMessage<NetworkInputMessages.InputKeyMessage>();

        if(OnNetworkInputKey != null)
        {
            OnNetworkInputKey.Invoke(message.Code, message.State);
        }
    }

    public void ReceiveMouseMovementMessage(NetworkMessage networkMessage)
    {
        NetworkInputMessages.MousePositionMessage message = networkMessage.ReadMessage<NetworkInputMessages.MousePositionMessage>();

        if(OnNetworkMousePosition != null)
        {
            OnNetworkMousePosition.Invoke(message.MousePosition);
        }
    }

    #endregion
}
