using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarioSimpleDemo
{
    ///<summary>
    ///
    ///</summary>
    public class GoalFlagController : MonoBehaviour
    {
        public string waitLevel = "WaitScene";

        public string clipName="win";

        private bool isEnter = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && !isEnter && collision.tag == "Player")
            {
                isEnter = true;
                
                AudioController.Instance.PlayFx(clipName);

                StartCoroutine("Delay");
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.5f);
            PlayerStatusInfo.Instance.gameState = GameState.Pause;

            yield return new WaitForSeconds(1.5f);

            PlayerStatusInfo.Instance.CurrentLevel += 1;
            PlayerPrefs.SetInt("curScore", PlayerStatusInfo.Instance.Score);
            //跳转到下一个关卡前的等待关卡(显示剩余生命数和当前分数)
            SceneManager.LoadScene(waitLevel);
        }
    }
}