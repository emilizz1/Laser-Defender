using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Text text;

	void Start ()
    {
        text = GetComponent<Text>();
	}

    public void UpdateHealth(int currentHealth)
    {
        text.text = currentHealth.ToString();
    }
}
