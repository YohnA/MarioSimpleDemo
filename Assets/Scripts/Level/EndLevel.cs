using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MarioSimpleDemo
{
    ///<summary>
    ///结束界面脚本
    ///</summary>
    public class EndLevel : MonoBehaviour
    {
        public string scoreName = "Score";
        public string levelNumName = "LevelNum";

        private Text scoreText;
        private Text levelNumText;

        // Use this for initialization
        private void Start()
        {
            scoreText = TransformHelper.FindChildByName(transform, scoreName).GetComponentInChildren<Text>();
            levelNumText = TransformHelper.FindChildByName(transform, levelNumName).GetComponentInChildren<Text>();

            StartCoroutine("ReturnScene");
        }

        // Update is called once per frame
        private void Update()
        {
            DisplayLevelIdAndScore();
        }

        /// <summary>
        /// 显示剩余生命数和分数
        /// </summary>
        private void DisplayLevelIdAndScore()
        {
            scoreText.text = PlayerStatusInfo.Instance.Score.ToString();
            levelNumText.text = (PlayerStatusInfo.Instance.CurrentLevel - 1).ToString();
        }

        /// <summary>
        /// 2s后回到开始界面
        /// </summary>
        private IEnumerator ReturnScene()
        {
            yield return new WaitForSeconds(2);
            
            SceneManager.LoadScene("StartScene");
        }
    }
}
