using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MarioSimpleDemo
{
    ///<summary>
    ///关卡控制器
    ///</summary>
    public class LevelController : MonoBehaviour
    {
        public Sprite pauseSprite;
        public Sprite continueSprite;
        public string bgmClipName="morlin1";

        public string waitLevel;
        public string endLevel;

        public Transform envirTf;
        public string liveNumName = "LiveNum";
        public string scoreName = "Score";

        private Text liveNumText;
        private Text scoreText;

        private int curLiveNum;
        private int curScore;

        // Use this for initialization
        private void Start()
        {
            PlayerStatusInfo.Instance.IsSub = false;
            PlayerStatusInfo.Instance.gameState = GameState.Playing;

            AudioController.Instance.PlayBgm(bgmClipName, true);

            //每到一个关卡，记录起始分数(在当前关卡失败重来时的初始分数)
            curScore = PlayerStatusInfo.Instance.Score;
            //记录关卡开始时的剩余生命数(判断玩家是否掉血)
            curLiveNum = PlayerStatusInfo.Instance.LiveNum;

            liveNumText = TransformHelper.FindChildByName(envirTf, liveNumName).GetComponentInChildren<Text>();
            scoreText = TransformHelper.FindChildByName(envirTf, scoreName).GetComponentInChildren<Text>();
        }

        // Update is called once per frame
        private void Update()
        {
            //当生命数发生变化时
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && curLiveNum != PlayerStatusInfo.Instance.LiveNum)
            {
                curLiveNum = PlayerStatusInfo.Instance.LiveNum;
                IsGameOver();
            }
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

        public void IsGameOver()
        {
            if (PlayerStatusInfo.Instance.LiveNum >= 0)
            {
                PlayerStatusInfo.Instance.gameState = GameState.Pause;
                //跳转到等待场景 显示剩余生命数 2s后重新回到当前关卡
                PlayerPrefs.SetInt("curScore", curScore);
                SceneManager.LoadScene(waitLevel);
            }
            else
            {
                PlayerStatusInfo.Instance.gameState = GameState.GameOver;
                //跳转到结束场景 显示分数和通过关卡数
                SceneManager.LoadScene(endLevel);
            }
        }

        public void OnClickPause(Image image)
        {
            if(PlayerStatusInfo.Instance.gameState == GameState.Playing)
            {
                PlayerStatusInfo.Instance.gameState = GameState.Pause;
                image.sprite = continueSprite;
            }
            else if (PlayerStatusInfo.Instance.gameState == GameState.Pause)
            {
                PlayerStatusInfo.Instance.gameState = GameState.Playing;
                image.sprite = pauseSprite;
            }
        }

        public void OnClickExit()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}