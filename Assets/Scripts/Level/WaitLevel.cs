using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MarioSimpleDemo
{
    ///<summary>
    ///等待界面脚本
    ///</summary>
    public class WaitLevel : MonoBehaviour
    {
        public string liveNumName = "LiveNum";
        public string scoreName = "Score";

        private Text liveNumText;
        private Text scoreText;

        // Use this for initialization
        private void Start()
        {
            liveNumText = TransformHelper.FindChildByName(transform, liveNumName).GetComponentInChildren<Text>();
            scoreText = TransformHelper.FindChildByName(transform, scoreName).GetComponentInChildren<Text>();

            StartCoroutine("ReturnScene");
        }

        // Update is called once per frame
        private void Update()
        {
            DisplayLiveNumAndScore();
        }

        /// <summary>
        /// 显示剩余生命数和分数
        /// </summary>
        private void DisplayLiveNumAndScore()
        {
            liveNumText.text = PlayerStatusInfo.Instance.LiveNum.ToString();
            scoreText.text = PlayerStatusInfo.Instance.Score.ToString();
        }

        /// <summary>
        /// 2s后重新回到当前关卡
        /// </summary>
        private IEnumerator ReturnScene()
        {
            yield return new WaitForSeconds(2);
            if (PlayerPrefs.HasKey("curScore"))
                PlayerStatusInfo.Instance.Score = PlayerPrefs.GetInt("curScore");

            SceneManager.LoadScene(PlayerStatusInfo.Instance.GetLevelName());
        }
    }
}