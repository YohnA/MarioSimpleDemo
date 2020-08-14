using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioSimpleDemo
{
    ///<summary>
    ///关卡对象脚本
    ///</summary>
    public class Level : Singleton<Level>
    {
        [SerializeField]
        private int cols = 35;  //列
        [SerializeField]
        private int rows = 20;  //行
        public const float gridCellSize = 1.28f;

        private readonly Color normalColor = Color.grey;
        private readonly Color selectColor = Color.red;

        public int Cols { get { return cols; } set { cols = value; } }

        public int Rows { get { return rows; } set { rows = value; } }

        /// <summary>
        /// 绘制网格边界方法
        /// </summary>
        private void GridBorderGizmo()
        {
            Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, Rows * gridCellSize, 0));
            Gizmos.DrawLine(new Vector3(Cols * gridCellSize, 0, 0), new Vector3(Cols * gridCellSize, Rows * gridCellSize, 0));
            Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(Cols * gridCellSize, 0, 0));
            Gizmos.DrawLine(new Vector3(0, Rows * gridCellSize, 0), new Vector3(Cols * gridCellSize, Rows * gridCellSize, 0));
        }

        /// <summary>
        /// 绘制网格内部方法
        /// </summary>
        private void GridGizmo()
        {
            for (int i = 1; i < Cols; i++)
            {
                Gizmos.DrawLine(new Vector3(i * gridCellSize, 0, 0), new Vector3(i * gridCellSize, Rows * gridCellSize, 0));
            }
            for (int i = 1; i < Rows; i++)
            {
                Gizmos.DrawLine(new Vector3(0, i * gridCellSize, 0), new Vector3(Cols * gridCellSize, i * gridCellSize, 0));
            }
        }

        private void OnDrawGizmos()
        {
            Color color = Gizmos.color;
            Matrix4x4 matrix = Gizmos.matrix;

            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = normalColor;
            GridGizmo();
            GridBorderGizmo();
            Gizmos.color = color;
            Gizmos.matrix = matrix;
        }

        private void OnDrawGizmosSelected()
        {
            Color color = Gizmos.color;
            Matrix4x4 matrix = Gizmos.matrix;

            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = selectColor;
            GridBorderGizmo();
            Gizmos.color = color;
            Gizmos.matrix = matrix;
        }

        /// <summary>
        /// 将对象物体的世界坐标转换成Grid中的坐标（行列索引）
        /// </summary>
        public Vector3 WorldToGridCoordinates(Vector3 point)
        {
            int colIndex = (int)((point.x - transform.position.x) / gridCellSize);
            int rowIndex = (int)((point.y - transform.position.y) / gridCellSize);
            return new Vector3(colIndex, rowIndex, 0);
        }

        /// <summary>
        /// 将Grid中的坐标位置转换成世界坐标，让对象物体自动对齐网格，实现自动捕捉功能
        /// </summary>
        public Vector3 GridToWorldCoordinates(int colIndex,int rowIndex)
        {
            float x = (colIndex * gridCellSize + gridCellSize / 2);
            float y = (rowIndex * gridCellSize + gridCellSize / 2);
            return new Vector3(x, y, 0);
        }

        /// <summary>
        /// 判断是否在网格内
        /// </summary>
        public bool IsInGrid(Vector3 point)
        {
            float minX = transform.position.x;
            float maxX = transform.position.x + cols * gridCellSize;
            float minY = transform.position.y;
            float maxY = transform.position.y + rows * gridCellSize;
            return point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY;
        }
    }
}
