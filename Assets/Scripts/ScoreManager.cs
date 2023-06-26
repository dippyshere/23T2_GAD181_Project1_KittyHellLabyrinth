using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] TMP_InputField inputName;
    [SerializeField] Button submitButton;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        submitButton.interactable = false;
        Debug.LogFormat("'{0}'", inputScore.text);
        submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
    }
}
