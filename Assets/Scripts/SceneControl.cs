using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
   public void RestartCurrentScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
