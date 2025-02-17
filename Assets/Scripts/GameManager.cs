using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageIndex;
    public PlayerMove player;
    public GameObject[] Stages;

    public void NextStage()
    {
        if (stageIndex < Stages.Length - 1)
        {
            // Change Stage
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
        }
        // Game Clear
        else
        {
            // Player Control Lock
            Time.timeScale = 0;
            // Result UI
            Debug.Log("게임 클리어");
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
}
