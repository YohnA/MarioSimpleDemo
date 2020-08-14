using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarioSimpleDemo;

namespace MarioSimpleDemo
{
    ///<summary>
    ///网格自动捕捉测试类
    ///</summary>
    [ExecuteInEditMode]
    public class SnapToGridTest : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            Vector3 point = Level.Instance.WorldToGridCoordinates(transform.position);
            transform.position = Level.Instance.GridToWorldCoordinates((int)point.x, (int)point.y);
        }

        private void OnDrawGizmos()
        {
            Color oldColor = Gizmos.color;

            Gizmos.color = (Level.Instance.IsInGrid(transform.position)) ? Color.green : Color.red;
            Gizmos.DrawCube(transform.position, Level.gridCellSize * Vector3.one);

            Gizmos.color = oldColor;
        }
    }
}
