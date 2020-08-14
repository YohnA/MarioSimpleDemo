using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarioSimpleDemo
{
    ///<summary>
    ///
    ///</summary>
    public class StartLevel : MonoBehaviour
    {
        public string bgmClipName="morlin2";

        private void Start()
        {
            PlayerStatusInfo.Instance.Init();

            AudioController.Instance.PlayBgm(bgmClipName, true);
        }

        public void TouchStart()
        {
            SceneManager.LoadScene("SelectLevel");
        }
    }
}