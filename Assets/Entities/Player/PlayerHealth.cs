using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Text text;

    public void UpdateHealth(int currentHealth)
    {
        text.text = currentHealth.ToString();
    }
}
