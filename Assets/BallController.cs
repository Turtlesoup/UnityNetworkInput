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

    public void OnNetworkInputKeyRecieved(KeyCode keyCode, Messages.InputKeyMessage.KeyState state)
    {
        if (state == Messages.InputKeyMessage.KeyState.Down)
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
        else if(state == Messages.InputKeyMessage.KeyState.Up)
        {
            if (keyCode == KeyCode.W)
            {
                isUpPressed = false;
            }
        }
    }

    public void OnMousePositionRecieved(Vector3 mousePosition)
    {
        Debug.Log("Mouse position! " + mousePosition.ToString());
    }
}
