using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionControls : MonoBehaviour
{
    public void LoadTargetScene()
    {
        SceneManager.LoadScene("Controls Menu");
    }
}