using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkInputServer : MonoBehaviour
{
    [SerializeField] private int port = 4444;

    private List<int> clientConnectionIds = new List<int>();

    private void Start()
    {
        SetupServer();
    }

    void Update()
    {
        // Send KeyDown/KeyUp events
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                SendInputKeyMessage(vKey, NetworkInputMessages.InputKeyMessage.KeyState.Down);
            }
            else if(Input.GetKeyUp(vKey))
            {
                SendInputKeyMessage(vKey, NetworkInputMessages.InputKeyMessage.KeyState.Up);
            }
        }

        SendMousePositionMessage(Input.mousePosition);
    }

    // Create a server and listen on a port
    public void SetupServer()
    {
        NetworkServer.Listen(port);
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnectedToServer);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnectedFromServer);
    }

    private void OnClientConnectedToServer(NetworkMessage netMsg)
    {
        int connectionId = netMsg.conn.connectionId;
        if (!clientConnectionIds.Contains(connectionId))
        {
            clientConnectionIds.Add(connectionId);
            Debug.Log("client connected: " + connectionId.ToString());
        }
    }

    private void OnClientDisconnectedFromServer(NetworkMessage netMsg)
    {
        int connectionId = netMsg.conn.connectionId;
        if (clientConnectionIds.Contains(connectionId))
        {
            clientConnectionIds.Remove(connectionId);
            Debug.Log("client disconnected: " + connectionId.ToString());
        }
    }

    #region Messages

    private void SendInputKeyMessage(KeyCode keycode, NetworkInputMessages.InputKeyMessage.KeyState state)
    {
        NetworkInputMessages.InputKeyMessage newMessage = new NetworkInputMessages.InputKeyMessage();
        newMessage.Code = keycode;
        newMessage.State = state;
        NetworkServer.SendToAll(NetworkInputMessages.InputKeyMessage.MessageType, newMessage);
    }

    private void SendMousePositionMessage(Vector3 mousePosition)
    {
        NetworkInputMessages.MousePositionMessage newMessage = new NetworkInputMessages.MousePositionMessage();
        newMessage.MousePosition = mousePosition;
        NetworkServer.SendToAll(NetworkInputMessages.MousePositionMessage.MessageType, newMessage);
    }

    #endregion
}
