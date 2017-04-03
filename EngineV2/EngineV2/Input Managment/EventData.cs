﻿using System;
using Microsoft.Xna.Framework.Input;
using OpenTK.Input;
using KeyboardState = Microsoft.Xna.Framework.Input.KeyboardState;


namespace EngineV2.Input
{
    public class EventData : EventArgs
    {
        public KeyboardState newKey;

        public EventData(KeyboardState state)
        {
            newKey = state;
        }
    }
}