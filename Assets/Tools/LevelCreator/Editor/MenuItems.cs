using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MarioSimpleDemo.LevelCreator
{
    public static class MenuItems
    {
        [MenuItem("Tools/Level Creator/New Level Scene")]
        private static void NewLevel()      //响应事件
        {
            EditorUtils.NewLevel();
        }

        [MenuItem("Tools/Level Creator/Show Palette")]
        private static void ShowPalette()      //响应事件
        {
            //创建编辑器窗体
            PaletteWindow.ShowPalette();
        }
    }
}
