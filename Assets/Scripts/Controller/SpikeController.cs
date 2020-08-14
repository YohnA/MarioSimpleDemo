using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioSimpleDemo
{
    ///<summary>
    ///
    ///</summary>
    public class SpikeController : MonoBehaviour
    {
        public string clipName="lose";

        private bool isEnter = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && !isEnter && collision.gameObject.layer==LayerMask.NameToLayer("Player"))
            {
                isEnter = true;

                PlayerController obj = GameObject.FindObjectOfType<PlayerController>();
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                obj.enabled = false;

                AudioController.Instance.PlayFx(clipName);
                StartCoroutine("Delay");
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(1);
            PlayerStatusInfo.Instance.SubLiveNum();
        }
    }
}