using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioSimpleDemo
{
    ///<summary>
    ///
    ///</summary>
    public class CoinController : MonoBehaviour
    {
        public int score = 1;

        public string clipName="coin";

        private bool isEnter = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && !isEnter && collision.tag == "Player")
            {
                //因为玩家身上有两个碰撞器，所以会调用两次(暂时先用一个变量控制)
                isEnter = true;

                AudioController.Instance.PlayFx(clipName);

                PlayerStatusInfo.Instance.GotScore(score);
                Destroy(this.gameObject);
            }
        }
    }
}
