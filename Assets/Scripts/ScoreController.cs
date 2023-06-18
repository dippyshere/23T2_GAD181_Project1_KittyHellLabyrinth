using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private float timeAlive;
    private PlayerController playerController;

    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private Canvas leaderboardCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        timeAlive = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isGameOver)
        {
            timeAlive += Time.deltaTime;
        }
        score.text = string.Format("{0}", Mathf.Ceil(timeAlive));

        if (playerController.isGameOver)
        {
            leaderboardCanvas.rootCanvas.enabled = true;
        }
    }
}
