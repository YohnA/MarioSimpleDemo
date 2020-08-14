using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioSimpleDemo
{
    ///<summary>
    ///小怪控制脚本
    ///</summary>
    public class EnemyController : MonoBehaviour
    {
        public LayerMask gcMask;
        public LayerMask ocMask;
        public Transform groundCheck;
        public Transform obstacleCheck;

        public string clipName = "lose";

        private Animator animator;

        public float moveSpeed = 2f;
        private int turnDir = 1;

        // Use this for initialization
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private bool isEnter = false;

        private bool isMove = true;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && !isEnter && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isEnter = true;

                isMove = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;

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

        // Update is called once per frame
        private void Update()
        {
            if (PlayerStatusInfo.Instance.gameState != GameState.Playing)
            {
                isMove = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else
            {
                isMove = true;
            }

            if (isMove)
                Move();
        }

        private void Move()
        {
            var groundCollider = Physics2D.OverlapCircle(groundCheck.position, 0.02f, gcMask);
            var obstacleCollider = Physics2D.OverlapCircle(obstacleCheck.position, 0.02f, ocMask);
            //转向
            if (groundCollider == null || obstacleCollider != null)
            {
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                turnDir *= -1;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * turnDir * Time.deltaTime, 0);
        }

        /// <summary>
        /// 触发器检测是否受到玩家的攻击(玩家从上方落下碰到小怪视为攻击小怪)
        /// </summary>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing && !isEnter && collision.tag == "Player")
            {
                isEnter = true;
                animator.SetBool("IsGotHit", true);
                Death();
            }
        }

        private void Death()
        {
            Destroy(this.gameObject, 0.2f);
        }
    }
}