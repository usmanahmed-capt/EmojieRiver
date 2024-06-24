using GameVanilla.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text Time;
    public float msToWait;
    private ulong lastTimeClicked;
    public AnimatedButton ClickButton;
    public Image buttonImage;
    public Sprite enabledSprite, disabledSprite;
    public string key;
    private void Start()
    {
        if (PlayerPrefs.HasKey(key))
            lastTimeClicked = ulong.Parse(PlayerPrefs.GetString(key));

        if (!Ready())
        {
            ClickButton.interactable = false;
            buttonImage.sprite = disabledSprite;
        }
    }

    private void Update()
    {
        if (!ClickButton.interactable)
        {
            if (Ready())
            {
                buttonImage.sprite = enabledSprite;
                ClickButton.interactable = true;

                Time.text = "Ready!";
                return;
            }
            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            //HOURS

            if ((int)secondsLeft / 3600 != 0)
                r += ((int)secondsLeft / 3600).ToString() + ":";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //MINUTES
            r += ((int)secondsLeft / 60).ToString("00") + ":";
            //SECONDS
            r += (secondsLeft % 60).ToString("00");
            Time.text = r;


        }
    }


    public void Click()
    {
        lastTimeClicked = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString(key, lastTimeClicked.ToString());
        ClickButton.interactable = false;
        buttonImage.sprite = disabledSprite;
    }
    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            Time.text = "Ready!";
            //DO SOMETHING WHEN TIMER IS FINISHED
            return true;
        }

        return false;
    }
}



