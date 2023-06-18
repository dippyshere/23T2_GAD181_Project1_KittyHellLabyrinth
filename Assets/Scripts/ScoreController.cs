using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private float timeAlive;

    [SerializeField] private TextMeshProUGUI score;
    
    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        score.text = string.Format("{0}", timeAlive);
    }
}
