using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool isUpPressed;

	private void Update ()
    {
		if(isUpPressed)
        {
            transform.position = transform.position + Vector3.up * Time.deltaTime;
        }
	}

    public void OnNetworkInputKeyRecieved(KeyCode keyCode, NetworkInputMessages.InputKeyMessage.KeyState state)
    {
        if (state == NetworkInputMessages.InputKeyMessage.KeyState.Down)
        {
            if (keyCode == KeyCode.W)
            {
                isUpPressed = true;
            }
            else if(keyCode == KeyCode.Mouse0)
            {
                transform.position = Vector3.zero;
            }
        }
        else if(state == NetworkInputMessages.InputKeyMessage.KeyState.Up)
        {
            if (keyCode == KeyCode.W)
            {
                isUpPressed = false;
            }
        }
    }
}
