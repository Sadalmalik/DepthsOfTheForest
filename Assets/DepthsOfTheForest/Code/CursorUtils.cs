using System.Collections.Generic;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public static class CursorUtils
    {
        private static Stack<CursorLockMode> _stateStack;

        static CursorUtils()
        {
            _stateStack = new Stack<CursorLockMode>();
        }

        public static void BeginState(CursorLockMode state)
        {
            _stateStack.Push(Cursor.lockState);
            Cursor.lockState = state;
        }

        public static void EndState()
        {
            if (_stateStack.Count > 0)
                Cursor.lockState = _stateStack.Pop();
        }
    }
}