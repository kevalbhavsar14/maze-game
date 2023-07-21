using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.GameState == GameState.Running)
            GameManager.Instance.GameState = GameState.Won;
    }
}
