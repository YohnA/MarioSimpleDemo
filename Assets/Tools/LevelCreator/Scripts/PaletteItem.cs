using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioSimpleDemo.LevelCreator
{
    public class PaletteItem : MonoBehaviour
    {
#if UNITY_EDITOR
        public enum Category
        {
            Other,
            Collectables,
            Enemies,
            Blocks,
        }

        public Category category = Category.Other;

        public string itemName = "";

        public Object inspectedScript;
#endif
    }
}
