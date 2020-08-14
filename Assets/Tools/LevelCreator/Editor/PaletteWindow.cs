using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MarioSimpleDemo.LevelCreator
{
    ///<summary>
    ///编辑器窗体类
    ///</summary>
    public class PaletteWindow : EditorWindow
    {
        //Category枚举中所有的类型
        private List<PaletteItem.Category> categories;
        //类型的名称
        private List<string> categoryNames=new List<string>();
        //被选中的类型
        private PaletteItem.Category categorySelected;

        //获取窗体内容所需的路径
        private string path = "Assets/Prefabs/LevelPieces";
        //获取到的预制件集合
        private List<PaletteItem> itemList;
        //根据枚举类型分类的预制件集合
        private Dictionary<PaletteItem.Category, List<PaletteItem>> categoryDic;
        //预制件各自对应的图片
        private Dictionary<PaletteItem, Texture2D> previewsDic = new Dictionary<PaletteItem, Texture2D>();
        //窗体滚动区域
        private Vector2 scrollPosition;
        private const float itemWidth = 80;     //窗体内容的宽度
        private const float itemHeight = 90;    //窗体内容的高度

        //选中预设体预览图后触发的 委托和事件
        public delegate void ItemSelectedHandler(PaletteItem item, Texture2D texture);
        public static event ItemSelectedHandler ItemSelectedEvent;

        public static PaletteWindow instance;
        /// <summary>
        /// 显示编辑器窗体
        /// </summary>
        public static void ShowPalette()
        {
            instance = EditorWindow.GetWindow<PaletteWindow>();
            instance.titleContent = new GUIContent("Palette");  //窗体标题
        }

        private void OnEnable()
        {
            if (categories == null)
                InitCategories();
            if (categoryDic == null)
                InitContent();
        }

        private void OnGUI()
        {
            DrawTabs();
            DrawScrollView();
        }

        private void Update()
        {
            if (previewsDic.Count != itemList.Count)
            {
                GeneratePreviews();
            }
        }

        /// <summary>
        /// 初始化获取所有类别
        /// </summary>
        private void InitCategories()
        {
            categories = EditorUtils.GetListFromEnum<PaletteItem.Category>();
            foreach (var item in categories)
            {
                categoryNames.Add(item.ToString());
            } 
        }
        /// <summary>
        /// 初始化 将获取到的预制件集合进行分类
        /// </summary>
        private void InitContent()
        {
            itemList = EditorUtils.GetAssetsWithScript<PaletteItem>(path);
            categoryDic = new Dictionary<PaletteItem.Category, List<PaletteItem>>();

            foreach (var category in categories)
            {
                categoryDic.Add(category, new List<PaletteItem>());
            }
            foreach (var item in itemList)
            {
                categoryDic[item.category].Add(item);
            }
        }

        /// <summary>
        /// 绘制编辑器窗体中的类别信息
        /// </summary>
        private void DrawTabs()
        {
            int selectedIndex = (int)categorySelected;
            selectedIndex = GUILayout.Toolbar(selectedIndex, categoryNames.ToArray());
            categorySelected = categories[selectedIndex];
        }

        /// <summary>
        /// 绘制窗体中选择的类别内容
        /// </summary>
        private void DrawScrollView()
        {
            if (categoryDic[categorySelected].Count != 0)
            {
                int colCapacity = Mathf.FloorToInt(position.width / itemWidth); //向下取整

                scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                int selectedGridIndex = -1;
                selectedGridIndex = GUILayout.SelectionGrid(selectedGridIndex, GetGUIContentsFromItems(), colCapacity, GetGUIStyles());
                GetSelectedItem(selectedGridIndex);
                GUILayout.EndScrollView();
            }
        }

        /// <summary>
        /// 获取预制件的预览图
        /// </summary>
        private void GeneratePreviews()
        {
            foreach (var item in itemList)
            {
                if (!previewsDic.ContainsKey(item))
                {
                    var texture = AssetPreview.GetAssetPreview(item.gameObject);
                    if (texture != null)
                        previewsDic.Add(item, texture);
                }
            }
        }

        private GUIContent[] GetGUIContentsFromItems()
        {
            List<GUIContent> guiContents = new List<GUIContent>();
            if (previewsDic.Count == itemList.Count)
            {
                int totalItems = categoryDic[categorySelected].Count;
                
                for(int i = 0; i < totalItems; i++)
                {
                    GUIContent guiContent = new GUIContent();
                    guiContent.text = categoryDic[categorySelected][i].itemName;
                    guiContent.image = previewsDic[categoryDic[categorySelected][i]];
                    guiContents.Add(guiContent);
                }
            }
            return guiContents.ToArray();
        }

        private GUIStyle GetGUIStyles()
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
            guiStyle.alignment = TextAnchor.LowerCenter;
            guiStyle.imagePosition = ImagePosition.ImageAbove;
            guiStyle.fixedWidth = itemWidth;
            guiStyle.fixedHeight = itemHeight;
            return guiStyle;
        }

        //选中预设体预览图
        private void GetSelectedItem(int index)
        {
            if (index != -1)
            {
                PaletteItem selectedItem = categoryDic[categorySelected][index];
                //触发事件
                if (ItemSelectedEvent != null)
                    ItemSelectedEvent(selectedItem, previewsDic[selectedItem]);
            }
        }
    }
}

