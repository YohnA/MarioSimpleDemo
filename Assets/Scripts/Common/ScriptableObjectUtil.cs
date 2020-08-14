using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonTool
{
    public class ScriptableObjectUtil : MonoBehaviour
    {
        internal static T GetScriptableObject<T>() where T : UnityEngine.Object
        {
            return Resources.Load<T>("ScriptableObject/" + typeof(T).Name);
        }
    }
}