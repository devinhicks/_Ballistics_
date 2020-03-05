using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;

    public Text youScored;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("ball"))
        {
            score++;
            youScored.text = "SCORE!";
        }
    }
}
