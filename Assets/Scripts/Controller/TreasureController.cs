using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioSimpleDemo
{
    ///<summary>
    ///
    ///</summary>
    public class TreasureController : MonoBehaviour
    {
        public int score = 5;

        public string clipName="treasure";

        private bool isEnter = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && !isEnter && collision.tag == "Player")
            {
                isEnter = true;

                AudioController.Instance.PlayFx(clipName);

                PlayerStatusInfo.Instance.GotScore(score);
                Destroy(this.gameObject);
            }
        }
    }
}
