using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioSimpleDemo
{
    ///<summary>
    ///人物控制类
    ///</summary>
    public class PlayerController : MonoBehaviour
    {
        public string jumpClipName = "jump";

        public Transform groundCheck;
        public LayerMask mask;

        public float moveSpeed = 1;
        public float jumpSpeed = 1;

        private bool isGround = true;          //是否在地面上
        private bool doubleJump = true;        //能否二级跳
        private float jumpTime = 0f;           //跳跃计时，用于实现二级跳

        private Animator animator;
        private Rigidbody2D rigidbody;

        // Use this for initialization
        private void Start()
        {
            animator = this.GetComponent<Animator>();
            rigidbody = this.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (PlayerStatusInfo.Instance.gameState == GameState.Playing || PlayerStatusInfo.Instance.gameState == GameState.Start)
            {
                if (rigidbody == null)
                {
                    rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
                    rigidbody.angularDrag = 0;
                    rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                IsGround();
                InputController();
                AnimationController();
            }
            else
                DestroyImmediate(rigidbody);
        }

        /// <summary>
        /// 动画控制
        /// </summary>
        private void AnimationController()
        {
            animator.SetBool("Ground", isGround);
            animator.SetFloat("VerticalSpeed", rigidbody.velocity.y / 3);
            animator.SetFloat("HorizontalSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));
        }

        /// <summary>
        /// 输入控制
        /// </summary>
        private void InputController()
        {
            if (Input.GetAxis("Horizontal") != 0)
                Move();
            else            //避免停止移动时人物的滑动效果
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

        private void Move()
        {
            float x = Input.GetAxis("Horizontal");
            var scale = transform.localScale;
            if ((x < 0 && scale.x == 1) || (x > 0 && scale.x == -1))
            {
                scale.x *= -1;
                transform.localScale = scale;
            }
            rigidbody.velocity = new Vector2(x * moveSpeed, rigidbody.velocity.y);
        }

        private void Jump()
        {
            if (isGround)
            {
                jumpTime += Time.deltaTime;
                rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

                AudioController.Instance.PlayFx(jumpClipName);

                doubleJump = true;
                isGround = false;
            }
            else
            {
                if (doubleJump)
                {
                    rigidbody.velocity = Vector2.zero;
                    rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

                    AudioController.Instance.PlayFx(jumpClipName);
                    doubleJump = false;
                }
            }
            if (jumpTime > 0.25f)
            {
                doubleJump = false;
                jumpTime = 0;
            }
        }

        /// <summary>
        /// 射线检测人物是否在地面上
        /// </summary>
        private void IsGround()
        {
            var collider = Physics2D.OverlapCircle(groundCheck.position, 0.1f, mask);
            if (collider != null)
                isGround = true;
            else
                isGround = false;
        }
    }
}
