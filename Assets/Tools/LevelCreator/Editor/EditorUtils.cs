using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace MarioSimpleDemo.LevelCreator
{
    public static class EditorUtils
    {
        /// <summary>
        /// 新建场景
        /// </summary>
        private static void NewScene()
        {
            //是否保存当前修改过的场景
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            //新建空场景
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
        }

        /// <summary>
        /// 新建关卡对象
        /// </summary>
        public static void NewLevel()
        {
            NewScene();
            GameObject levelGo = new GameObject("Level");
            levelGo.transform.position = Vector3.zero;
            levelGo.AddComponent<Level>();
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        public static List<T> GetAssetsWithScript<T>(string path) where T : MonoBehaviour
        {
            List<T> assetList = new List<T>();
            //在path限制的路径里查找明确定义了类型(Prefab)的资源
            //返回guid-global uniqu id 每个资源都有一个唯一的id
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                T temp = asset.GetComponent<T>();
                if (temp != null)
                {
                    assetList.Add(temp);
                }
            }
            return assetList;
        }

        /// <summary>
        /// 获取枚举类型
        /// </summary>
        public static List<T> GetListFromEnum<T>()
        {
            List<T> enumList = new List<T>();
            System.Array enums = System.Enum.GetValues(typeof(T));
            foreach (T item in enums)
            {
                enumList.Add(item);
            }
            return enumList;
        }
    }
}
