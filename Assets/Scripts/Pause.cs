using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.GameState == GameState.Running)
            {
                GameManager.Instance.GameState = GameState.Paused;
            }
            else if (GameManager.Instance.GameState == GameState.Paused)
            {
                GameManager.Instance.GameState = GameState.Running;
            }
        }
    }
}
