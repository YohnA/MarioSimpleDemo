using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarioSimpleDemo
{
    ///<summary>
    ///提示牌控制类
    ///</summary>
    public class SignController : MonoBehaviour
    {
        public string tip = "Please Input Tip!!!";

        public Text tipText;

        public string clipName="sign";

        private bool isEnter = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && !isEnter && collision.tag == "Player")
            {
                AudioController.Instance.PlayFx(clipName);

                tipText.text = tip;
                tipText.transform.parent.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && collision.tag == "Player")
            {
                tipText.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}