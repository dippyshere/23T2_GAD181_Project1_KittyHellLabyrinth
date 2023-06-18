using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        Debug.LogFormat("'{0}'", inputScore.text);
        submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
    }
}
