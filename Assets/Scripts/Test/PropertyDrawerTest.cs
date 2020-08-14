using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///属性绘制器
///</summary>

public class PropertyDrawerTest : MonoBehaviour {

    [Tooltip("This is a tooltip for intValue")]
    [Range(0, 100)]
    public int intValue = 50;

    [Range(0, 1)]
    public float floatValue = 0.5f;

    [Multiline(3)]          //3行，大于3行时不显示滚动条
    public string stringMultiline = "123 \n 456 \n 789";

    [TextArea(2, 4)]        //一个2-4行的文本区域，输入内容大于4行时显示滚动条
    public string stringTextArea = "123 \n 456 \n 789 \n 110 \n 120";

    [ContextMenu("Test")]
    public void TestFunc()      //响应方法
    {
        Debug.Log("123");
    }

    [ContextMenuItem("Reset", "ResetFunc")]
    public int intReset = 13;

    public void ResetFunc()     //响应方法
    {
        intReset = 0;
    }
}
