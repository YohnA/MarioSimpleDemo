using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Transform助手类
///</summary>

public class TransformHelper : MonoBehaviour {

    ///<summary>
    ///在层级未知的情况下查找子物体（已知子物体名字）
    ///</summary>
    public static Transform FindChildByName(Transform tf, string childName)
    {
        Transform childTf = tf.Find(childName);
        if (childTf != null)
        {
            return childTf;
        }

        int count = tf.childCount;
        for (int i = 0; i < count; i++)
        {
            childTf = FindChildByName(tf.GetChild(i), childName);
            if (childTf != null)
            {
                return childTf;
            }
        }
        return null;
    }

    /// <summary>
    /// 转向
    /// </summary>
    public static void LookAtTarget(Transform tf, Vector3 target, float rotationSpeed)
    {
        if (target != tf.position)
        {
            Quaternion dir = Quaternion.LookRotation(target);
            tf.rotation = Quaternion.Lerp(tf.rotation, dir, rotationSpeed * Time.deltaTime);
        }
    }
}
