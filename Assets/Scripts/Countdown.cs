using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    int currentTime;
    int startingTime = 3;
    private float timer = 1.5f;
    private float waitTime = 1.5f;

    public TextMeshProUGUI countdownText;
    public Animator countdownAnimator;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            if (currentTime == 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                AnimateCountdown();
                timer = 0;
            }
        }
    }

    public void AnimateCountdown()
    {
            SetCountdownText(currentTime);
            Highlight();
            currentTime--;
    }


    public void Highlight()
    {
        countdownAnimator.SetTrigger("hlcountdown");
    }

    public void SetCountdownText(int value)
    {
        countdownText.text = value.ToString();
    }
}
