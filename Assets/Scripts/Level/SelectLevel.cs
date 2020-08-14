using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarioSimpleDemo
{
    ///<summary>
    ///
    ///</summary>
    public class SelectLevel : MonoBehaviour
    {
        public void LoadLevel(int id)
        {
            PlayerStatusInfo.Instance.CurrentLevel = id;
            SceneManager.LoadScene("Level" + id.ToString());
        }
    }
}