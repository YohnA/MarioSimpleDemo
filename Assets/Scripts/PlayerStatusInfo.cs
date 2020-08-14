using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CommonTool;

namespace MarioSimpleDemo
{
    ///<summary>
    ///玩家状态信息脚本( 生命数、得分 )
    ///</summary>
    public class PlayerStatusInfo : Singleton<PlayerStatusInfo>
    {
        public GameState gameState;

        private int liveNum = 1;
        private int score = 0;
        private int currentLevel = 0;

        private LevelScenes levelScenes;

        private void Awake()
        {
            levelScenes = ScriptableObjectUtil.GetScriptableObject<LevelScenes>();
        }

        public int LiveNum
        {
            get { return liveNum; }
            set { liveNum = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                currentLevel = value;
                if (!levelScenes.IsIn(currentLevel))
                    currentLevel = 0;
            }
        }

        public string GetLevelName()
        {
            return levelScenes.GetName(CurrentLevel);
        }

        public void GotScore(int gotScore)
        {
            Score += gotScore;
        }

        private bool isSub = false;

        public bool IsSub
        {
            get { return isSub; }
            set { isSub = value; }
        }

        public void SubLiveNum()
        {
            //避免玩家同时碰到多个刺或小怪导致生命数减少过多
            if (!IsSub)
            {
                IsSub = true;
                LiveNum -= 1;
            }
        }

        public void Init()
        {
            gameState = GameState.Start;
            LiveNum = 5;
            Score = 0;
            CurrentLevel = 0;
        }
    }
}