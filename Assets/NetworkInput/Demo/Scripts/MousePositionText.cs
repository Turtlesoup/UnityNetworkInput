using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePositionText : MonoBehaviour
{
    private Text textBox;

    private void Awake()
    {
        textBox = GetComponent<Text>();
    }

    public void OnMousePositionRecieved(Vector3 mousePosition)
    {
        textBox.text = "Mouse position " + mousePosition.ToString();
    }
}
