using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class gameController : MonoBehaviour
{
    public GameObject[] slides;
    public GameObject[] scenes;
    public GameObject helpScreen;
    public GameObject[] menu;
    public GameObject ARDetail, emptyscreen;
    public GameObject play;
    public GameObject menucontainer;
    public GameObject statedropdown, statedropdown2;
    public GameObject potriattolandscape;
    public GameObject[] updateInfoForm, nonrickCard, nonrickdisable, nonrickenable;
    public GameObject gameMusic, formcontrolUpdated;

    //public Animator animator;

    //public GameObject OrientationRes;
    // public GameObject LarcenyForm;
    // Start is called before the first frame update


    void Start()
    {
        //animator = GetComponent<Animator>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }


    public void nextBtn(string name)
    {

        SceneManager.LoadSceneAsync(name);

    }


    public void changeScene4()
    {
        if (scoreScript.scoreValue == 1)
        {
            for (int count = 0; count < slides.Length; count++)
            {

                slides[count].SetActive(false);
                count++;
            }
            slides[20].SetActive(true);
            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
            updateInfoForm[0].SetActive(false);
            updateInfoForm[1].SetActive(false);
            updateInfoForm[2].SetActive(false);
            updateInfoForm[3].SetActive(false);
            updateInfoForm[4].SetActive(false);
            updateInfoForm[5].SetActive(false);
            updateInfoForm[6].SetActive(false);
            updateInfoForm[7].SetActive(false);
            updateInfoForm[8].SetActive(false);
            updateInfoForm[9].SetActive(false);
            updateInfoForm[10].SetActive(false);
            updateInfoForm[11].SetActive(false);

            potriattolandscape.SetActive(false);

            //Non Rickhouse Card Disabled Here
            nonrickCard[0].SetActive(false);
            nonrickCard[1].SetActive(false);
            nonrickCard[2].SetActive(false);
            nonrickCard[3].SetActive(false);
        }
        else
        {
            for (int count = 0; count < slides.Length; count++)
            {

                slides[count].SetActive(false);
                count++;
            }
            slides[24].SetActive(true);
            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
            updateInfoForm[0].SetActive(false);
            updateInfoForm[1].SetActive(false);
            updateInfoForm[2].SetActive(false);
            updateInfoForm[3].SetActive(false);
            updateInfoForm[4].SetActive(false);
            updateInfoForm[5].SetActive(false);
            updateInfoForm[6].SetActive(false);
            updateInfoForm[7].SetActive(false);
            updateInfoForm[8].SetActive(false);
            updateInfoForm[9].SetActive(false);
            updateInfoForm[10].SetActive(false);
            updateInfoForm[11].SetActive(false);

            //Non Rickhouse Card Disabled Here
            nonrickCard[0].SetActive(false);
            nonrickCard[1].SetActive(false);
            nonrickCard[2].SetActive(false);
            nonrickCard[3].SetActive(false);

            potriattolandscape.SetActive(false);
        }
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void changeScene5()
    {
        scenes[0].SetActive(true);
        ARDetail.SetActive(true);
        helpScreen.SetActive(false);
        emptyscreen.SetActive(false);
    }

    public void playBtn()
    {
        play.SetActive(false);
    }
    public void menuclose()
    {
        menucontainer.SetActive(false);
        scenes[0].SetActive(true);

        //Non Rickhouse Card Disabled Here
        nonrickCard[0].SetActive(false);
        nonrickCard[1].SetActive(false);
        nonrickCard[2].SetActive(false);
        nonrickCard[3].SetActive(false);

        //Non Rickhouse Buildings Disabled
        nonrickdisable[0].SetActive(false);
        nonrickdisable[1].SetActive(false);
        nonrickdisable[2].SetActive(false);
        nonrickdisable[3].SetActive(false);
        nonrickdisable[4].SetActive(false);
        nonrickdisable[5].SetActive(false);
        nonrickdisable[6].SetActive(false);

        //Non Rickhouse Buildings Enabled
        nonrickenable[0].SetActive(true);
        nonrickenable[1].SetActive(true);
        nonrickenable[2].SetActive(true);
        nonrickenable[3].SetActive(true);
        nonrickenable[4].SetActive(true);
        nonrickenable[5].SetActive(true);
        nonrickenable[6].SetActive(true);
    }
    public void menuopen()
    {
        menucontainer.SetActive(true);
        scenes[0].SetActive(false);

        //Non Rickhouse Card Disabled Here
        nonrickCard[0].SetActive(false);
        nonrickCard[1].SetActive(false);
        nonrickCard[2].SetActive(false);
        nonrickCard[3].SetActive(false);

        //Non Rickhouse Buildings Disabled
        nonrickdisable[0].SetActive(true);
        nonrickdisable[1].SetActive(true);
        nonrickdisable[2].SetActive(true);
        nonrickdisable[3].SetActive(true);
        nonrickdisable[4].SetActive(true);
        nonrickdisable[5].SetActive(true);
        nonrickdisable[6].SetActive(true);

        //Non Rickhouse Buildings Enabled
        nonrickenable[0].SetActive(false);
        nonrickenable[1].SetActive(false);
        nonrickenable[2].SetActive(false);
        nonrickenable[3].SetActive(false);
        nonrickenable[4].SetActive(false);
        nonrickenable[5].SetActive(false);
        nonrickenable[6].SetActive(false);

    }

    public void howto()
    {
        menu[0].SetActive(true);
        menucontainer.SetActive(false);

        //Non Rickhouse Card Disabled Here
        nonrickCard[0].SetActive(false);
        nonrickCard[1].SetActive(false);
        nonrickCard[2].SetActive(false);
        nonrickCard[3].SetActive(false);
    }
    public void storylar()
    {
        Application.OpenURL("https://larcenybourbon.com/home/?fg=adv");
    }
    public void privacypolicy()
    {
        Application.OpenURL("https://larcenybourbon.com/home/index.php?show=privacy");
    }
    public void thinkwisly()
    {
        Application.OpenURL("http://www.heavenhill.com/responsibility-statement.php");
    }
    public void editsaved()
    {
        updateInfoForm[0].SetActive(true);

        updateInfoForm[1].SetActive(false);
        updateInfoForm[2].SetActive(false);

        updateInfoForm[3].SetActive(true);
        updateInfoForm[4].SetActive(true);
        updateInfoForm[5].SetActive(true);

        updateInfoForm[6].SetActive(false);
        updateInfoForm[7].SetActive(false);
        updateInfoForm[8].SetActive(false);

        updateInfoForm[9].SetActive(true);
        updateInfoForm[10].SetActive(false);

        menucontainer.SetActive(false);
        slides[26].SetActive(false);
        gameMusic.SetActive(false);

        //Non Rickhouse Card Disabled Here
        nonrickCard[0].SetActive(false);
        nonrickCard[1].SetActive(false);
        nonrickCard[2].SetActive(false);
        nonrickCard[3].SetActive(false);



    }
   
    public void rules()
    {
        Application.OpenURL("https://larcenybourbon.com/home/?fg=adv");
    }

    public void emailLink()
    {
        Application.OpenURL("mailto:unlocktherickhouse@larcenybourbon.com");
    }


    public void changeScene6()
    {
        if (scoreScript.scoreValue == 0)
        {
            slides[0].SetActive(false);
            slides[1].SetActive(false);
            slides[2].SetActive(false);
            slides[3].SetActive(false);
            slides[4].SetActive(false);
            slides[5].SetActive(false);
            slides[6].SetActive(false);
            slides[7].SetActive(false);
            slides[8].SetActive(false);
            slides[9].SetActive(false);
            slides[10].SetActive(false);
            slides[11].SetActive(false);
            slides[12].SetActive(false);
            slides[13].SetActive(false);
            slides[14].SetActive(false);
            slides[15].SetActive(false);
            slides[16].SetActive(false);
            slides[17].SetActive(false);
            slides[18].SetActive(false);

            slides[19].SetActive(true);

            slides[20].SetActive(false);
            slides[21].SetActive(false);
            slides[22].SetActive(false);
            slides[23].SetActive(false);
            slides[24].SetActive(false);
            slides[25].SetActive(false);

            scenes[0].SetActive(false);

            scenes[1].SetActive(false);
            scenes[2].SetActive(false);

            slides[26].SetActive(true);

            updateInfoForm[0].SetActive(false);
            updateInfoForm[1].SetActive(false);
            updateInfoForm[2].SetActive(false);
            updateInfoForm[3].SetActive(false);
            updateInfoForm[4].SetActive(false);
            updateInfoForm[5].SetActive(false);
            updateInfoForm[6].SetActive(false);
            updateInfoForm[7].SetActive(false);
            updateInfoForm[8].SetActive(false);
            updateInfoForm[9].SetActive(false);
            updateInfoForm[10].SetActive(false);
            updateInfoForm[11].SetActive(false);

            potriattolandscape.SetActive(false);

            gameMusic.SetActive(true);

            //Non Rickhouse Card Disabled Here
            nonrickCard[0].SetActive(false);
            nonrickCard[1].SetActive(false);
            nonrickCard[2].SetActive(false);
            nonrickCard[3].SetActive(false);

            //Non Rickhouse Buildings Disabled
            nonrickdisable[0].SetActive(false);
            nonrickdisable[1].SetActive(false);
            nonrickdisable[2].SetActive(false);
            nonrickdisable[3].SetActive(false);
            nonrickdisable[4].SetActive(false);
            nonrickdisable[5].SetActive(false);
            nonrickdisable[6].SetActive(false);

            //Non Rickhouse Buildings Enabled
            nonrickenable[0].SetActive(true);
            nonrickenable[1].SetActive(true);
            nonrickenable[2].SetActive(true);
            nonrickenable[3].SetActive(true);
            nonrickenable[4].SetActive(true);
            nonrickenable[5].SetActive(true);
            nonrickdisable[6].SetActive(true);

            //this.animator.enabled = !animator.enabled;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            slides[0].SetActive(false);
            slides[1].SetActive(false);
            slides[2].SetActive(false);
            slides[3].SetActive(false);
            slides[4].SetActive(false);
            slides[5].SetActive(false);
            slides[6].SetActive(false);
            slides[7].SetActive(false);
            slides[8].SetActive(false);
            slides[9].SetActive(false);
            slides[10].SetActive(false);
            slides[11].SetActive(false);
            slides[12].SetActive(false);
            slides[13].SetActive(false);
            slides[14].SetActive(false);
            slides[15].SetActive(false);
            slides[16].SetActive(false);
            slides[17].SetActive(false);
            slides[18].SetActive(false);
            slides[19].SetActive(false);
            slides[20].SetActive(false);
            slides[21].SetActive(false);
            slides[22].SetActive(false);
            slides[23].SetActive(false);
            slides[24].SetActive(false);
            slides[25].SetActive(false);

            scenes[0].SetActive(true);

            scenes[1].SetActive(false);
            scenes[2].SetActive(false);

            slides[26].SetActive(true);

            updateInfoForm[0].SetActive(false);
            updateInfoForm[1].SetActive(false);
            updateInfoForm[2].SetActive(false);
            updateInfoForm[3].SetActive(false);
            updateInfoForm[4].SetActive(false);
            updateInfoForm[5].SetActive(false);
            updateInfoForm[6].SetActive(false);
            updateInfoForm[7].SetActive(false);
            updateInfoForm[8].SetActive(false);
            updateInfoForm[9].SetActive(false);
            updateInfoForm[10].SetActive(false);
            updateInfoForm[11].SetActive(false);

            //Non Rickhouse Card Disabled Here
            nonrickCard[0].SetActive(false);
            nonrickCard[1].SetActive(false);
            nonrickCard[2].SetActive(false);
            nonrickCard[3].SetActive(false);

            //Non Rickhouse Buildings Disabled
            nonrickdisable[0].SetActive(false);
            nonrickdisable[1].SetActive(false);
            nonrickdisable[2].SetActive(false);
            nonrickdisable[3].SetActive(false);
            nonrickdisable[4].SetActive(false);
            nonrickdisable[5].SetActive(false);
            nonrickdisable[6].SetActive(false);

            //Non Rickhouse Buildings Enabled
            nonrickenable[0].SetActive(true);
            nonrickenable[1].SetActive(true);
            nonrickenable[2].SetActive(true);
            nonrickenable[3].SetActive(true);
            nonrickenable[4].SetActive(true);
            nonrickenable[5].SetActive(true);
            nonrickenable[6].SetActive(true);

            gameMusic.SetActive(true);
            //this.animator.enabled = !animator.enabled;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    public void changeScene9()
    {

        if (scoreScript.scoreValue == 0)
        {
            slides[0].SetActive(false);
            slides[1].SetActive(false);
            slides[2].SetActive(false);
            slides[3].SetActive(false);
            slides[4].SetActive(false);
            slides[5].SetActive(false);
            slides[6].SetActive(false);
            slides[7].SetActive(false);
            slides[8].SetActive(false);
            slides[9].SetActive(false);
            slides[10].SetActive(false);
            slides[11].SetActive(false);
            slides[12].SetActive(false);
            slides[13].SetActive(false);
            slides[14].SetActive(false);
            slides[15].SetActive(false);
            slides[16].SetActive(false);
            slides[17].SetActive(false);
            slides[18].SetActive(false);
            slides[20].SetActive(false);
            slides[21].SetActive(false);
            slides[22].SetActive(false);
            slides[23].SetActive(false);
            slides[24].SetActive(false);
            slides[25].SetActive(false);
            slides[26].SetActive(false);
            scenes[0].SetActive(false);

            slides[19].SetActive(true);

            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
            updateInfoForm[0].SetActive(false);
            updateInfoForm[1].SetActive(false);
            updateInfoForm[2].SetActive(false);
            updateInfoForm[3].SetActive(false);
            updateInfoForm[4].SetActive(false);
            updateInfoForm[5].SetActive(false);
            updateInfoForm[6].SetActive(false);
            updateInfoForm[7].SetActive(false);
            updateInfoForm[8].SetActive(false);
            updateInfoForm[9].SetActive(false);
            updateInfoForm[10].SetActive(false);
            updateInfoForm[11].SetActive(false);

            //Non Rickhouse Card Disabled Here
            nonrickCard[0].SetActive(false);
            nonrickCard[1].SetActive(false);
            nonrickCard[2].SetActive(false);
            nonrickCard[3].SetActive(false);

            potriattolandscape.SetActive(false);
            // OrientationRes.GetComponent<DeviceChange>().enabled = false;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            // Screen.orientation = ScreenOrientation.LandscapeRight;
        }
        else
        {
            slides[0].SetActive(false);
            slides[1].SetActive(false);
            slides[2].SetActive(false);
            slides[3].SetActive(false);
            slides[4].SetActive(false);
            slides[5].SetActive(false);
            slides[6].SetActive(false);
            slides[7].SetActive(false);
            slides[8].SetActive(false);
            slides[9].SetActive(false);
            slides[10].SetActive(false);
            slides[11].SetActive(false);
            slides[12].SetActive(false);
            slides[13].SetActive(false);
            slides[14].SetActive(false);
            slides[15].SetActive(false);
            slides[16].SetActive(false);
            slides[17].SetActive(false);
            slides[18].SetActive(false);
            slides[19].SetActive(false);
            slides[20].SetActive(false);
            slides[21].SetActive(false);
            slides[22].SetActive(false);
            slides[23].SetActive(false);
            slides[24].SetActive(false);
            slides[25].SetActive(false);


            scenes[1].SetActive(false);
            scenes[2].SetActive(false);

            updateInfoForm[0].SetActive(false);
            updateInfoForm[1].SetActive(false);
            updateInfoForm[2].SetActive(false);
            updateInfoForm[3].SetActive(false);
            updateInfoForm[4].SetActive(false);
            updateInfoForm[5].SetActive(false);
            updateInfoForm[6].SetActive(false);
            updateInfoForm[7].SetActive(false);
            updateInfoForm[8].SetActive(false);
            updateInfoForm[9].SetActive(false);
            updateInfoForm[10].SetActive(false);
            updateInfoForm[11].SetActive(false);

            potriattolandscape.SetActive(true);

            //Non Rickhouse Card Disabled Here
            nonrickCard[0].SetActive(false);
            nonrickCard[1].SetActive(false);
            nonrickCard[2].SetActive(false);
            nonrickCard[3].SetActive(false);

            //this.animator.enabled = !animator.enabled;
            // OrientationRes.GetComponent<DeviceChange>().enabled = false;
            //Screen.orientation = ScreenOrientation.LandscapeLeft;
            // Screen.orientation = ScreenOrientation.LandscapeRight;
        }


        //Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void closeBtn(){
        slides[0].SetActive(false);
        slides[1].SetActive(false);
        slides[2].SetActive(false);
        slides[3].SetActive(false);
        slides[4].SetActive(false);
        slides[5].SetActive(false);
        slides[6].SetActive(false);
        slides[7].SetActive(false);
        slides[8].SetActive(false);
        slides[9].SetActive(false);
        slides[10].SetActive(false);
        slides[11].SetActive(false);
        slides[12].SetActive(false);
        slides[13].SetActive(false);
        slides[14].SetActive(false);
        slides[15].SetActive(false);
        slides[16].SetActive(false);
        slides[17].SetActive(false);
        slides[18].SetActive(false);
        slides[19].SetActive(false);
        slides[20].SetActive(false);
        slides[21].SetActive(false);
        slides[22].SetActive(false);
        slides[23].SetActive(false);
        slides[24].SetActive(false);
        slides[25].SetActive(false);

        scenes[0].SetActive(true);

        scenes[1].SetActive(false);
        scenes[2].SetActive(false);

        slides[26].SetActive(true);

        updateInfoForm[0].SetActive(false);
        updateInfoForm[1].SetActive(false);
        updateInfoForm[2].SetActive(false);
        updateInfoForm[3].SetActive(false);
        updateInfoForm[4].SetActive(false);
        updateInfoForm[5].SetActive(false);
        updateInfoForm[6].SetActive(false);
        updateInfoForm[7].SetActive(false);
        updateInfoForm[8].SetActive(false);
        updateInfoForm[9].SetActive(false);
        updateInfoForm[10].SetActive(false);
        updateInfoForm[11].SetActive(false);

        potriattolandscape.SetActive(false);

        gameMusic.SetActive(true);
        //this.animator.enabled = !animator.enabled;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        //Non Rickhouse Card Disabled Here
        nonrickCard[0].SetActive(false);
        nonrickCard[1].SetActive(false);
        nonrickCard[2].SetActive(false);
        nonrickCard[3].SetActive(false);

        //Non Rickhouse Buildings Disabled
        nonrickdisable[0].SetActive(false);
        nonrickdisable[1].SetActive(false);
        nonrickdisable[2].SetActive(false);
        nonrickdisable[3].SetActive(false);
        nonrickdisable[4].SetActive(false);
        nonrickdisable[5].SetActive(false);
        nonrickdisable[6].SetActive(false);

        //Non Rickhouse Buildings Enabled
        nonrickenable[0].SetActive(true);
        nonrickenable[1].SetActive(true);
        nonrickenable[2].SetActive(true);
        nonrickenable[3].SetActive(true);
        nonrickenable[4].SetActive(true);
        nonrickenable[5].SetActive(true);
        nonrickenable[6].SetActive(true);
    }

   
    public void formCloseupd()
    {
        updateInfoForm[1].SetActive(false);
        scenes[0].SetActive(true);
        slides[26].SetActive(true);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        gameMusic.SetActive(true);

        formcontrolUpdated.SetActive(false);

        //Non Rickhouse Buildings Disabled
        nonrickdisable[0].SetActive(false);
        nonrickdisable[1].SetActive(false);
        nonrickdisable[2].SetActive(false);
        nonrickdisable[3].SetActive(false);
        nonrickdisable[4].SetActive(false);
        nonrickdisable[5].SetActive(false);
        nonrickdisable[6].SetActive(false);

        //Non Rickhouse Buildings Enabled
        nonrickenable[0].SetActive(true);
        nonrickenable[1].SetActive(true);
        nonrickenable[2].SetActive(true);
        nonrickenable[3].SetActive(true);
        nonrickenable[4].SetActive(true);
        nonrickenable[5].SetActive(true);
        nonrickenable[6].SetActive(true);

    }

    public void closenonrickCard()
    {
        //Non Rickhouse Card Disabled Here
        nonrickCard[0].SetActive(false);
        nonrickCard[1].SetActive(false);
        nonrickCard[2].SetActive(false);
        nonrickCard[3].SetActive(false);

        //Non Rickhouse Buildings Disabled
        nonrickdisable[0].SetActive(false);
        nonrickdisable[1].SetActive(false);
        nonrickdisable[2].SetActive(false);
        nonrickdisable[3].SetActive(false);
        nonrickdisable[4].SetActive(false);
        nonrickdisable[5].SetActive(false);
        nonrickdisable[6].SetActive(false);

        //Non Rickhouse Buildings Enabled
        nonrickenable[0].SetActive(true);
        nonrickenable[1].SetActive(true);
        nonrickenable[2].SetActive(true);
        nonrickenable[3].SetActive(true);
        nonrickenable[4].SetActive(true);
        nonrickenable[5].SetActive(true);
        nonrickenable[6].SetActive(true);

    }

    public void stateDropActive()
    {
        statedropdown.SetActive(true);
    }
    public void stateDropUnActive()
    {
        statedropdown.SetActive(false);
    }
    public void stateDropActive2()
    {
        statedropdown2.SetActive(true);
    }
    public void stateDropUnActive2()
    {
        statedropdown2.SetActive(false);
    }

   



}
