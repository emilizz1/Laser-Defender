using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    void Start()
    {
        Text myText = GetComponent<Text>();
        myText.text = Score.score.ToString();
        Score.Reset();
    }
}
