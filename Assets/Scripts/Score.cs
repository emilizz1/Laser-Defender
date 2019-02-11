using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public static int score;

	Text myText;

	void Start ()
    {
		myText = GetComponent<Text> ();
		Reset ();
	}

	public static void Reset ()
	{
		score = 0;
	}

	public void ScorePoints(int points)
	{
		score += points;
        FindObjectOfType<Rewards>().CheckForRewards(score);
		myText.text = score.ToString ();
	}
}
