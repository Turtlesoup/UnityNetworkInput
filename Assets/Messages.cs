using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class Messages
{
    public class InputKeyMessage : MessageBase
    {
        public static short MessageType = MsgType.Highest + 1;

        public enum KeyState : Int32
        {
            None = 0,
            Down = 1,
            Up = 2
        }

        public KeyCode Code { get; set; }
        public KeyState State { get; set; }

        // This method would be generated
        public override void Deserialize(NetworkReader reader)
        {
            Code = (KeyCode)reader.ReadInt32();
            State = (KeyState)reader.ReadInt32();
        }

        // This method would be generated
        public override void Serialize(NetworkWriter writer)
        {
            writer.Write((Int32)Code);
            writer.Write((Int32)State);
        }
    }

    public class MousePositionMessage : MessageBase
    {
        public static short MessageType = MsgType.Highest + 2;

        public Vector2 MousePosition { get; set; }

        public override void Deserialize(NetworkReader reader)
        {
            MousePosition = reader.ReadVector2();
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(MousePosition);
        }
    }
}
