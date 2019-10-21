using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Globalization;

public class houseScript : MonoBehaviour
{
    public Text prizeImgApi;
    public AudioClip[] aClips;
    public AudioSource myAudioSource;
    public GameObject[] theBarrel;
    public GameObject[] theBirds;
    public GameObject[] b00;
    public GameObject[] bnull;
    public GameObject directionalLight;
    public GameObject[] infotainment;
    public GameObject[] scenes;

    public GameObject[] thanksmsg;
    public string btnName;
    int infotaincards0;
    int infotaincards1;
    int infotaincards2;
    int infotaincards3;
    int infotaincards4;
    int infotaincards5;
    int infotaincards6;
    public int winingcard;
    public int rickhouseno;
    public GameObject winnerprize1;
    public GameObject noprize, menucontainer, heading4winner, enternowBtn, placeconfirm;

    public Text instructionmap, rickhousesprizewinner;
    public Text[] winner, rdisable;


    public GameObject FormControl;
    public int totalentriesCounter;

    public GameObject helpScreen;
    public GameObject ARDetail, deviceinfo, emptyscreen, FormUpdateBtn, placemap, startplacement, truckHorn;

    public GameObject gameMusic;
    public GameObject[] nonrickCard, nonrickdisable, nonrickenable;

    string[] items;

    public GameObject[] updateInfoForm;

    public Text[] GetTexts;
    public string itemsDataString;
    string CreateUserURL = "https://larcenybourbon.com/ar-app/api-dev/register_device.php";
    string CounterAppURL = "https://larcenybourbon.com/ar-app/api-dev/counter.php";


    public static houseScript instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
         StartCoroutine(Upload("ABCDEFGHIJK", SystemInfo.deviceUniqueIdentifier, SystemInfo.deviceModel, Application.version));

    }

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        //TouchScreenKeyboard keyboard;
        TouchScreenKeyboard.hideInput = true;
        //if (keyboard == null || !TouchScreenKeyboard.visible)
        // keyboard = TouchScreenKeyboard.Open ("", TouchScreenKeyboardType.Default, false, false, false, false, "xyz"); 
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Counter API function Write Here and checked how many entries in the Database
    public IEnumerator Counter(string deviceinfo)
    {
        WWWForm CRform = new WWWForm();
        CRform.AddField("deviceinfoPost", deviceinfo);
        CRform.AddField("rickhouse_api_counter", totalentriesCounter);


        CRform.AddField("disablerickhouse1", rdisable[0].text);
        CRform.AddField("disablerickhouse2", rdisable[1].text);
        CRform.AddField("disablerickhouse3", rdisable[2].text);
        CRform.AddField("disablerickhouse4", rdisable[3].text);
        CRform.AddField("disablerickhouse5", rdisable[4].text);
        CRform.AddField("disablerickhouse6", rdisable[5].text);
        CRform.AddField("disablerickhouse7", rdisable[6].text);


        using (UnityWebRequest www = UnityWebRequest.Post(CounterAppURL, CRform))
        {
            // WWW itemsData = www.SendWebRequest();
            yield return www.SendWebRequest();



            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.downloadHandler.data);
                Debug.Log("User Data not submit");
            }
            else
            {
                Debug.Log("User Data Submit");
            }
        }

    }

    // Register Device API Function Write Here and fetch data from database of different parameters.
    public IEnumerator Upload(string dtoken, string deviceinfo, string devicemodel, string iosversion)
    {
        WWWForm form = new WWWForm();
        form.AddField("device_token", dtoken);
        form.AddField("deviceinfoPost", deviceinfo);
        form.AddField("iosversionPost", devicemodel);
        form.AddField("appversionPost", iosversion);

        using (UnityWebRequest www = UnityWebRequest.Post(CreateUserURL, form))
        {
            // WWW itemsData = www.SendWebRequest();
            yield return www.SendWebRequest();



            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.downloadHandler.data);
                Debug.Log("Info not submit");
            }
            else
            {
                itemsDataString = www.downloadHandler.text;
                Debug.Log(itemsDataString);
                //itemsDataString = "";
                //if (itemsDataString == "")
                //{
                  //  itemsDataString = "success:Yes|error:0|device_id:" + SystemInfo.deviceUniqueIdentifier + "|isLock:No|timer:13:32:32|disable-rickhouse:r00,r00,r00,r00,r00,r00,r00|remaining-rickhouse:4|prize:cap|age-gate:Yes|instructions:No|prize-rickhouse:b03|infotainment-cards:6,9,10,11,7,3,4|counter:0";
                //}
                //Debug.Log(www);
                Debug.Log("Info submitted");

                //Print Complete array from php and split with itemsDataString function
                GetTexts[0].text = (itemsDataString);
                items = itemsDataString.Split(';');
                GetTexts[9].text = (GetDataValue(items[0], "success:"));
                int.TryParse(GetDataValue(items[0], "counter:"), out totalentriesCounter);
                // Debug.Log("API Counter = " + totalentriesCounter);
                GetTexts[10].text = (GetDataValue(items[0], "showForm:"));
                instructionmap.text = (GetDataValue(items[0], "instructions:"));
                prizeImgApi.text = (GetDataValue(items[0], "prize:"));
                GetTexts[0].text = (GetDataValue(items[0], "prize-rickhouse:"));
                GetTexts[1].text = (GetDataValue(items[0], "infotainment-cards:"));
                winner[0].text = (GetDataValue(items[0], "disable-rickhouse:"));

                rickhousesprizewinner.text = (GetDataValue(items[0], "prize-rickhouse:"));
                //Split the Disable Rickhouses values and print into text fields.
                string str2 = winner[0].text;
                char[] splitchar2 = { ',' };
                string[] strArr2 = str2.Split(splitchar2);
                int infocount2 = 0;
                for (int count = 0; count <= strArr2.Length - 1; count++)
                {
                    winner[infocount2].text = strArr2[count];
                    infocount2++;
                }

                Debug.Log(winner[0].text);
                Debug.Log(winner[1].text);
                Debug.Log(winner[2].text);
                Debug.Log(winner[3].text);
                Debug.Log(winner[4].text);
                Debug.Log(winner[5].text);
                Debug.Log(winner[6].text);

                //Split the infotainment card values and print into text fields.
                string str = GetTexts[1].text;
                char[] splitchar = { ',' };
                string[] strArr = str.Split(splitchar);
                int infocount = 2;
                for (int count = 0; count <= strArr.Length - 1; count++)
                {
                    GetTexts[infocount].text = strArr[count];
                    infocount++;
                }
            }

            if (instructionmap.text == "No")
            {
                ARDetail.SetActive(true);
                helpScreen.SetActive(false);
                emptyscreen.SetActive(false);

            }
            else
            {
                helpScreen.SetActive(true);
                ARDetail.SetActive(false);
            }



        }
    }

    // Parameters splits here.
    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }


    // Update is called once per frame
    void Update()
    {

        //int.TryParse(GetTexts[0].text, out winingcard);
        int.TryParse(GetTexts[2].text, out infotaincards0);
        int.TryParse(GetTexts[3].text, out infotaincards1);
        int.TryParse(GetTexts[4].text, out infotaincards2);
        int.TryParse(GetTexts[5].text, out infotaincards3);
        int.TryParse(GetTexts[6].text, out infotaincards4);
        int.TryParse(GetTexts[7].text, out infotaincards5);
        int.TryParse(GetTexts[8].text, out infotaincards6);


        if (winner[0].text == "r01")
        {
            theBarrel[0].SetActive(false);
            bnull[0].SetActive(false);
            b00[0].SetActive(true);
            scoreScript.scoreValue -= 1;

        }
        if (winner[1].text == "r02")
        {
            theBarrel[1].SetActive(false);
            bnull[1].SetActive(false);
            b00[1].SetActive(true);
            scoreScript.scoreValue -= 1;

        }

        if (winner[2].text == "r03")
        {
            theBarrel[2].SetActive(false);
            bnull[2].SetActive(false);
            b00[2].SetActive(true);
            scoreScript.scoreValue -= 1;
        }

        if (winner[3].text == "r04")
        {
            theBarrel[3].SetActive(false);
            bnull[3].SetActive(false);
            b00[3].SetActive(true);
            scoreScript.scoreValue -= 1;


        }

        if (winner[4].text == "r05")
        {
            theBarrel[4].SetActive(false);
            bnull[4].SetActive(false);
            b00[4].SetActive(true);
            scoreScript.scoreValue -= 1;

        }

        if (winner[5].text == "r06")
        {
            theBarrel[5].SetActive(false);
            bnull[5].SetActive(false);
            b00[5].SetActive(true);
            scoreScript.scoreValue -= 1;

        }

        if (winner[6].text == "r07")
        {
            theBarrel[6].SetActive(false);
            bnull[6].SetActive(false);
            b00[6].SetActive(true);
            scoreScript.scoreValue -= 1;
        }




        // Debug.Log("Update Method" + GetTexts[0].text);

       if (Input.GetMouseButtonDown(0))
       {

         RaycastHit hit;
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if (Physics.Raycast(ray, out hit, 100.0f))
           {

       /* if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                                    {
                                       Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                                       RaycastHit hit;
                                       if (Physics.Raycast(ray, out hit))
                                       {*/


                btnName = hit.transform.name;

                switch (btnName)
                {
                    case "b01":
                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();
                        scoreScript.scoreValue--;
                        totalentriesCounter++;

                        // Infotainments Cards Disabled Enabled Here according to rickhouse number.

                        if (scoreScript.scoreValue == 0)
                        {
                            Debug.Log("6");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(true);

                        }
                        else if (scoreScript.scoreValue == 1)
                        {
                            //Debug.Log("5");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(true);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 2)
                        {
                            //Debug.Log("4");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(true);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 3)
                        {
                            //Debug.Log("3");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(true);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 4)
                        {
                            //Debug.Log("2");s
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(true);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 5)
                        {
                            //Debug.Log("1");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(true);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 6)
                        {
                            //Debug.Log("0");
                            infotainment[infotaincards0].SetActive(true);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }



                        // Rickehouse Disabled here.
                        theBarrel[0].SetActive(false);

                        // Rickehouse non Clickable Enabled here.
                        b00[0].SetActive(true);


                        bnull[0].SetActive(false);

                        //Non Rickhouse Card Disabled Here
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        //Rickhouse Disable Parameter Pass Here
                        rdisable[0].text = "r01";

                        // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
                        scenes[0].SetActive(false);
                        scenes[1].SetActive(false);
                        scenes[2].SetActive(false);
                        scenes[3].SetActive(false);
                        scenes[4].SetActive(false);
                        scenes[5].SetActive(false);
                        Debug.Log("R1 = " + infotaincards0);

                        // Counter Api Starts here.
                        StartCoroutine(Counter(SystemInfo.deviceUniqueIdentifier));

                        infotainment[26].SetActive(false);

                        // Music Disabled Here.
                        gameMusic.SetActive(false);
                        break;

                    case "b02":
                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();
                        scoreScript.scoreValue--;
                        totalentriesCounter++;

                        // Infotainments Cards Disabled Enabled Here according to rickhouse number.

                        if (scoreScript.scoreValue == 0)
                        {
                            Debug.Log("6");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(true);

                        }
                        else if (scoreScript.scoreValue == 1)
                        {
                            //Debug.Log("5");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(true);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 2)
                        {
                            //Debug.Log("4");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(true);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 3)
                        {
                            //Debug.Log("3");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(true);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 4)
                        {
                            //Debug.Log("2");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(true);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 5)
                        {
                            //Debug.Log("1");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(true);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 6)
                        {
                            //Debug.Log("0");
                            infotainment[infotaincards0].SetActive(true);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        infotainment[26].SetActive(false);

                        // Rickehouse Disabled here.
                        theBarrel[1].SetActive(false);

                        // Rickehouse non Clickable Enabled here.
                        b00[1].SetActive(true);

                        bnull[1].SetActive(false);

                        //Non Rickhouse Card Disabled Here
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        //Rickhouse Disable Parameter Pass Here
                        rdisable[1].text = "r02";

                        // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
                        scenes[0].SetActive(false);
                        scenes[1].SetActive(false);
                        scenes[2].SetActive(false);
                        scenes[3].SetActive(false);
                        scenes[4].SetActive(false);
                        scenes[5].SetActive(false);


                        Debug.Log("R2 = " + infotaincards1);

                        // Counter Api Starts here.
                        StartCoroutine(Counter(SystemInfo.deviceUniqueIdentifier));

                        // Music Disabled Here.
                        gameMusic.SetActive(false);
                        break;

                    case "b03":
                        myAudioSource.clip = aClips[2];
                        myAudioSource.Play();
                        scoreScript.scoreValue--;
                        totalentriesCounter++;

                        // Infotainments Cards Disabled Enabled Here according to rickhouse number.

                        if (scoreScript.scoreValue == 0)
                        {
                            Debug.Log("6");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(true);

                        }
                        else if (scoreScript.scoreValue == 1)
                        {
                            //Debug.Log("5");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(true);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 2)
                        {
                            //Debug.Log("4");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(true);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 3)
                        {
                            //Debug.Log("3");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(true);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 4)
                        {
                            //Debug.Log("2");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(true);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 5)
                        {
                            //Debug.Log("1");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(true);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 6)
                        {
                            //Debug.Log("0");
                            infotainment[infotaincards0].SetActive(true);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }

                        infotainment[26].SetActive(false);

                        // Rickehouse Disabled here.
                        theBarrel[2].SetActive(false);

                        // Rickehouse non Clickable Enabled here.
                        b00[2].SetActive(true);


                        bnull[2].SetActive(false);

                        //Non Rickhouse Card Disabled Here
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        //Rickhouse Disable Parameter Pass Here
                        rdisable[2].text = "r03";

                        // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
                        scenes[0].SetActive(false);
                        scenes[1].SetActive(false);
                        scenes[2].SetActive(false);
                        scenes[3].SetActive(false);
                        scenes[4].SetActive(false);
                        scenes[5].SetActive(false);
                        Debug.Log("R3 = " + infotaincards2);

                        // Counter Api Starts here.
                        StartCoroutine(Counter(SystemInfo.deviceUniqueIdentifier));

                        // Music Disabled Here.
                        gameMusic.SetActive(false);
                        break;

                    case "b04":
                        myAudioSource.clip = aClips[3];
                        myAudioSource.Play();
                        scoreScript.scoreValue--;
                        totalentriesCounter++;

                        // Infotainments Cards Disabled Enabled Here according to rickhouse number.

                        if (scoreScript.scoreValue == 0)
                        {
                            Debug.Log("6");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(true);

                        }
                        else if (scoreScript.scoreValue == 1)
                        {
                            //Debug.Log("5");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(true);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 2)
                        {
                            //Debug.Log("4");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(true);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 3)
                        {
                            //Debug.Log("3");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(true);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 4)
                        {
                            //Debug.Log("2");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(true);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 5)
                        {
                            //Debug.Log("1");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(true);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 6)
                        {
                            //Debug.Log("0");
                            infotainment[infotaincards0].SetActive(true);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }

                        infotainment[26].SetActive(false);

                        // Rickehouse Disabled here.
                        theBarrel[3].SetActive(false);

                        // Rickehouse non Clickable Enabled here.
                        b00[3].SetActive(true);


                        bnull[3].SetActive(false);

                        //Non Rickhouse Card Disabled Here
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        //Rickhouse Disable Parameter Pass Here
                        rdisable[3].text = "r04";

                        // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
                        scenes[0].SetActive(false);
                        scenes[1].SetActive(false);
                        scenes[2].SetActive(false);
                        scenes[3].SetActive(false);
                        scenes[4].SetActive(false);
                        scenes[5].SetActive(false);
                        Debug.Log("R4 = " + infotaincards3);

                        // Counter Api Starts here.
                        StartCoroutine(Counter(SystemInfo.deviceUniqueIdentifier));

                        // Music Disabled Here.
                        gameMusic.SetActive(false);
                        break;

                    case "b05":
                        myAudioSource.clip = aClips[4];
                        myAudioSource.Play();
                        scoreScript.scoreValue--;
                        totalentriesCounter++;

                        // Infotainments Cards Disabled Enabled Here according to rickhouse number.

                        if (scoreScript.scoreValue == 0)
                        {
                            Debug.Log("6");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(true);

                        }
                        else if (scoreScript.scoreValue == 1)
                        {
                            //Debug.Log("5");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(true);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 2)
                        {
                            //Debug.Log("4");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(true);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 3)
                        {
                            //Debug.Log("3");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(true);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 4)
                        {
                            //Debug.Log("2");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(true);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 5)
                        {
                            //Debug.Log("1");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(true);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 6)
                        {
                            //Debug.Log("0");
                            infotainment[infotaincards0].SetActive(true);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        infotainment[26].SetActive(false);

                        // Rickehouse Disabled here.
                        theBarrel[4].SetActive(false);

                        // Rickehouse non Clickable Enabled here.
                        b00[4].SetActive(true);


                        bnull[4].SetActive(false);

                        //Non Rickhouse Card Disabled Here
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        //Rickhouse Disable Parameter Pass Here
                        rdisable[4].text = "r05";

                        // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
                        scenes[0].SetActive(false);
                        scenes[1].SetActive(false);
                        scenes[2].SetActive(false);
                        scenes[3].SetActive(false);
                        scenes[4].SetActive(false);
                        scenes[5].SetActive(false);
                        Debug.Log("R5 = " + infotaincards4);

                        // Counter Api Starts here.
                        StartCoroutine(Counter(SystemInfo.deviceUniqueIdentifier));

                        // Music Disabled Here.
                        gameMusic.SetActive(false);
                        break;
                    case "b06":
                        myAudioSource.clip = aClips[5];
                        myAudioSource.Play();
                        scoreScript.scoreValue--;
                        totalentriesCounter++;

                        // Infotainments Cards Disabled Enabled Here according to rickhouse number.
                                               
                        if (scoreScript.scoreValue == 0)
                        {
                            Debug.Log("6");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(true);

                        }
                        else if (scoreScript.scoreValue == 1)
                        {
                            //Debug.Log("5");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(true);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 2)
                        {
                            //Debug.Log("4");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(true);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 3)
                        {
                            //Debug.Log("3");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(true);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 4)
                        {
                            //Debug.Log("2");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(true);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 5)
                        {
                            //Debug.Log("1");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(true);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 6)
                        {
                            //Debug.Log("0");
                            infotainment[infotaincards0].SetActive(true);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        infotainment[26].SetActive(false);

                        // Rickehouse Disabled here.
                        theBarrel[5].SetActive(false);

                        // Rickehouse non Clickable Enabled here.
                        b00[5].SetActive(true);


                        bnull[5].SetActive(false);

                        //Non Rickhouse Card Disabled Here
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        //Rickhouse Disable Parameter Pass Here
                        rdisable[5].text = "r06";

                        // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
                        scenes[0].SetActive(false);
                        scenes[1].SetActive(false);
                        scenes[2].SetActive(false);
                        scenes[3].SetActive(false);
                        scenes[4].SetActive(false);
                        scenes[5].SetActive(false);
                        Debug.Log("R6 = " + infotaincards5);

                        // Counter Api Starts here.
                        StartCoroutine(Counter(SystemInfo.deviceUniqueIdentifier));

                        // Music Disabled Here.
                        gameMusic.SetActive(false);
                        break;
                    case "b07":
                        //Debug.Log("B07");
                        myAudioSource.clip = aClips[6];
                        myAudioSource.Play();
                        scoreScript.scoreValue--;
                        totalentriesCounter++;

                        // Infotainments Cards Disabled Enabled Here according to rickhouse number.

                        if (scoreScript.scoreValue == 0)
                        {
                            Debug.Log("6");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(true);

                        }
                        else if (scoreScript.scoreValue == 1)
                        {
                            //Debug.Log("5");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(true);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 2)
                        {
                            //Debug.Log("4");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(true);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 3)
                        {
                            //Debug.Log("3");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(true);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 4)
                        {
                            //Debug.Log("2");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(true);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 5)
                        {
                            //Debug.Log("1");
                            infotainment[infotaincards0].SetActive(false);
                            infotainment[infotaincards1].SetActive(true);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        else if (scoreScript.scoreValue == 6)
                        {
                            //Debug.Log("0");
                            infotainment[infotaincards0].SetActive(true);
                            infotainment[infotaincards1].SetActive(false);
                            infotainment[infotaincards2].SetActive(false);
                            infotainment[infotaincards3].SetActive(false);
                            infotainment[infotaincards4].SetActive(false);
                            infotainment[infotaincards5].SetActive(false);
                            infotainment[infotaincards6].SetActive(false);

                        }
                        infotainment[26].SetActive(false);

                        // Rickehouse Disabled here.
                        theBarrel[6].SetActive(false);

                        // Rickehouse non Clickable Enabled here.
                        b00[6].SetActive(true);


                        bnull[6].SetActive(false);

                        //Non Rickhouse Card Disabled Here
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        //Rickhouse Disable Parameter Pass Here
                        rdisable[6].text = "r07";

                        // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
                        scenes[0].SetActive(false);
                        scenes[1].SetActive(false);
                        scenes[2].SetActive(false);
                        scenes[3].SetActive(false);
                        scenes[4].SetActive(false);
                        scenes[5].SetActive(false);
                        Debug.Log("R7 = " + infotaincards6);

                        // Counter Api Starts here.
                        StartCoroutine(Counter(SystemInfo.deviceUniqueIdentifier));

                        // Music Disabled Here.
                        gameMusic.SetActive(false);
                        break;
                    case "TruckAnim":
                        myAudioSource.clip = aClips[8];
                        myAudioSource.Play();

                        break;
                    case "gazebobtn":
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(true);
                        nonrickCard[3].SetActive(false);

                        nonrickdisable[0].SetActive(true);
                        nonrickdisable[1].SetActive(true);
                        nonrickdisable[2].SetActive(true);
                        nonrickdisable[3].SetActive(true);
                        nonrickdisable[4].SetActive(true);
                        nonrickdisable[5].SetActive(true);
                        nonrickdisable[6].SetActive(true);

                        nonrickenable[0].SetActive(false);
                        nonrickenable[1].SetActive(false);
                        nonrickenable[2].SetActive(false);
                        nonrickenable[3].SetActive(false);
                        nonrickenable[4].SetActive(false);
                        nonrickenable[5].SetActive(false);
                        nonrickenable[6].SetActive(false);

                        break;
                    case "hallwatbtn":
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(true);
                        nonrickCard[3].SetActive(false);

                        nonrickdisable[0].SetActive(true);
                        nonrickdisable[1].SetActive(true);
                        nonrickdisable[2].SetActive(true);
                        nonrickdisable[3].SetActive(true);
                        nonrickdisable[4].SetActive(true);
                        nonrickdisable[5].SetActive(true);
                        nonrickdisable[6].SetActive(true);

                        nonrickenable[0].SetActive(false);
                        nonrickenable[1].SetActive(false);
                        nonrickenable[2].SetActive(false);
                        nonrickenable[3].SetActive(false);
                        nonrickenable[4].SetActive(false);
                        nonrickenable[5].SetActive(false);
                        nonrickenable[6].SetActive(false);

                        break;
                    case "shackbtn":
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(true);
                        nonrickCard[3].SetActive(false);

                        nonrickdisable[0].SetActive(true);
                        nonrickdisable[1].SetActive(true);
                        nonrickdisable[2].SetActive(true);
                        nonrickdisable[3].SetActive(true);
                        nonrickdisable[4].SetActive(true);
                        nonrickdisable[5].SetActive(true);
                        nonrickdisable[6].SetActive(true);

                        nonrickenable[0].SetActive(false);
                        nonrickenable[1].SetActive(false);
                        nonrickenable[2].SetActive(false);
                        nonrickenable[3].SetActive(false);
                        nonrickenable[4].SetActive(false);
                        nonrickenable[5].SetActive(false);
                        nonrickenable[6].SetActive(false);

                        break;
                    case "glasswoodbtn":
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(true);
                        nonrickCard[3].SetActive(false);

                        nonrickdisable[0].SetActive(true);
                        nonrickdisable[1].SetActive(true);
                        nonrickdisable[2].SetActive(true);
                        nonrickdisable[3].SetActive(true);
                        nonrickdisable[4].SetActive(true);
                        nonrickdisable[5].SetActive(true);
                        nonrickdisable[6].SetActive(true);

                        nonrickenable[0].SetActive(false);
                        nonrickenable[1].SetActive(false);
                        nonrickenable[2].SetActive(false);
                        nonrickenable[3].SetActive(false);
                        nonrickenable[4].SetActive(false);
                        nonrickenable[5].SetActive(false);
                        nonrickenable[6].SetActive(false);

                        break;
                    case "bottlingbtn":
                        nonrickCard[0].SetActive(true);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        nonrickdisable[0].SetActive(true);
                        nonrickdisable[1].SetActive(true);
                        nonrickdisable[2].SetActive(true);
                        nonrickdisable[3].SetActive(true);
                        nonrickdisable[4].SetActive(true);
                        nonrickdisable[5].SetActive(true);
                        nonrickdisable[6].SetActive(true);

                        nonrickenable[0].SetActive(false);
                        nonrickenable[1].SetActive(false);
                        nonrickenable[2].SetActive(false);
                        nonrickenable[3].SetActive(false);
                        nonrickenable[4].SetActive(false);
                        nonrickenable[5].SetActive(false);
                        nonrickenable[6].SetActive(false);

                        break;
                    case "cisternbtn":
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(true);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(false);

                        nonrickdisable[0].SetActive(true);
                        nonrickdisable[1].SetActive(true);
                        nonrickdisable[2].SetActive(true);
                        nonrickdisable[3].SetActive(true);
                        nonrickdisable[4].SetActive(true);
                        nonrickdisable[5].SetActive(true);
                        nonrickdisable[6].SetActive(true);

                        nonrickenable[0].SetActive(false);
                        nonrickenable[1].SetActive(false);
                        nonrickenable[2].SetActive(false);
                        nonrickenable[3].SetActive(false);
                        nonrickenable[4].SetActive(false);
                        nonrickenable[5].SetActive(false);
                        nonrickenable[6].SetActive(false);

                        break;
                    case "wheatbtn":
                        nonrickCard[0].SetActive(false);
                        nonrickCard[1].SetActive(false);
                        nonrickCard[2].SetActive(false);
                        nonrickCard[3].SetActive(true);

                        nonrickdisable[0].SetActive(true);
                        nonrickdisable[1].SetActive(true);
                        nonrickdisable[2].SetActive(true);
                        nonrickdisable[3].SetActive(true);
                        nonrickdisable[4].SetActive(true);
                        nonrickdisable[5].SetActive(true);
                        nonrickdisable[6].SetActive(true);

                        nonrickenable[0].SetActive(false);
                        nonrickenable[1].SetActive(false);
                        nonrickenable[2].SetActive(false);
                        nonrickenable[3].SetActive(false);
                        nonrickenable[4].SetActive(false);
                        nonrickenable[5].SetActive(false);
                        nonrickenable[6].SetActive(false);

                        break;
                    default:
                        break;

                }


            }
        }

    }

    //Rickhouse One CTA
    public void checkrickhouse1()
    {

        // Debug.Log("Win Card = " + GetTexts[0].text);
        //Debug.Log("BtName = " + btnName);
        //int.TryParse(btnName, out rickhouseno);

        //Debug.Log("RickHouseno = " + btnName);
        // Disable Enable Enter now button on no prize card
        /*if (GetTexts[10].text == "1")
        {
            enternowBtn.SetActive(true);
        }
        else
        {
            enternowBtn.SetActive(false);
        }*/

        //Matching Winner Card Rickhouse Number in this Statement with Database parameter Here 
        if (GetTexts[0].text == btnName)
        {

            //Disabled All Other infotainment Card in this loop.
            for (int count = 0; count < infotainment.Length; count++)
            {
                infotainment[count].SetActive(false);
                count++;
            }

            //Landscape Winner Card Activated Here
            infotainment[17].SetActive(true);

            // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
            scenes[3].SetActive(false);
            scenes[4].SetActive(false);
            scenes[5].SetActive(false);

        }
        else
        {

            for (int count = 0; count < infotainment.Length; count++)
            {

                infotainment[count].SetActive(false);
                count++;
            }
            //Landscape No Prize Card Activated Here
            Debug.Log("Rickhouse Thank you " + GetTexts[10].text);
            if (GetTexts[10].text == "0")
            {
                infotainment[24].SetActive(true);
                infotainment[18].SetActive(false);
            }
            else
            {
                infotainment[18].SetActive(true);
                infotainment[24].SetActive(false);
            }

            // Larceny map, Menu, Loading Screen, Helpscreen Disabled Here
            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
            scenes[3].SetActive(false);
            scenes[4].SetActive(false);
            scenes[5].SetActive(false);

        }
    }

    public void checkrickhouse2()
    {
        Debug.Log("FormSubmit New Value= " + GetTexts[10].text);


        //Matching Form is Filled in this Statement with Database parameter Here. if "1" it will be active
        if (GetTexts[10].text == "1")
        {
            //All infotainment cards disabled here
            infotainment[0].SetActive(false);
            infotainment[1].SetActive(false);
            infotainment[2].SetActive(false);
            infotainment[3].SetActive(false);
            infotainment[4].SetActive(false);
            infotainment[5].SetActive(false);
            infotainment[6].SetActive(false);
            infotainment[7].SetActive(false);
            infotainment[8].SetActive(false);
            infotainment[9].SetActive(false);
            infotainment[10].SetActive(false);
            infotainment[11].SetActive(false);
            infotainment[12].SetActive(false);
            infotainment[13].SetActive(false);
            infotainment[15].SetActive(false);
            infotainment[16].SetActive(false);
            infotainment[17].SetActive(false);
            infotainment[18].SetActive(false);
            infotainment[21].SetActive(false);
            infotainment[22].SetActive(false);
            infotainment[23].SetActive(false);
            infotainment[24].SetActive(false);
            infotainment[25].SetActive(false);
            infotainment[26].SetActive(false);

            // All Scenes disabled here
            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
            scenes[3].SetActive(false);
            scenes[4].SetActive(false);
            scenes[5].SetActive(false);

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


            //Setting Form parameter to "0" here.
            GetTexts[10].text = "0";

            //Tilt Message of form Active here and form control script active here
            infotainment[20].SetActive(true);
            FormControl.SetActive(true);

            //Edit information form button disabled here
            FormUpdateBtn.SetActive(false);

            //music disabled here
            gameMusic.SetActive(false);

        }
        else
        {
            //All infotainment cards disabled here.
            infotainment[0].SetActive(false);
            infotainment[1].SetActive(false);
            infotainment[2].SetActive(false);
            infotainment[3].SetActive(false);
            infotainment[4].SetActive(false);
            infotainment[5].SetActive(false);
            infotainment[6].SetActive(false);
            infotainment[7].SetActive(false);
            infotainment[8].SetActive(false);
            infotainment[9].SetActive(false);
            infotainment[10].SetActive(false);
            infotainment[11].SetActive(false);
            infotainment[12].SetActive(false);
            infotainment[13].SetActive(false);
            infotainment[15].SetActive(false);
            infotainment[16].SetActive(false);
            infotainment[17].SetActive(false);
            infotainment[18].SetActive(false);
            infotainment[21].SetActive(false);
            infotainment[22].SetActive(false);
            infotainment[23].SetActive(false);
            infotainment[26].SetActive(false);

            //All scenes disabled here
            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
            scenes[3].SetActive(false);
            scenes[4].SetActive(false);
            scenes[5].SetActive(false);

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


            //Edit information form button active here
            FormUpdateBtn.SetActive(true);

            //Matching Prize Rickhouse parameter with rickhouse number to show hide thank you cards for winnner 
            //and no prize to user.
            if (GetTexts[0].text == btnName)
            {
                //winner card active here
                infotainment[25].SetActive(true);
                infotainment[24].SetActive(false);

            }
            else
            {
                //np prize card active here 
                infotainment[25].SetActive(false);
                infotainment[24].SetActive(true);

            }

            //Checking Number of active rickhouses if rickhouse "0" left and disabled card will be showed.
            if (scoreScript.scoreValue == 0)
            {
                thanksmsg[0].SetActive(false);
                thanksmsg[1].SetActive(false);
                thanksmsg[2].SetActive(true);
            }

            //game music disabled here.
            gameMusic.SetActive(false);

            //formControl disabled here.
            FormControl.SetActive(false);

        }
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }

    //form close button function.
    public void formClose()
    {
        Debug.Log("close Form " + GetTexts[10].text);
        //Form Control Disabled
        FormControl.SetActive(false);

        //Matching form show parameter with "0" if we getting "0" then form will not be disabled.
        if (GetTexts[10].text == "0")
        {
            infotainment[0].SetActive(false);
            infotainment[1].SetActive(false);
            infotainment[2].SetActive(false);
            infotainment[3].SetActive(false);
            infotainment[4].SetActive(false);
            infotainment[5].SetActive(false);
            infotainment[6].SetActive(false);
            infotainment[7].SetActive(false);
            infotainment[8].SetActive(false);
            infotainment[9].SetActive(false);
            infotainment[10].SetActive(false);
            infotainment[11].SetActive(false);
            infotainment[12].SetActive(false);
            infotainment[13].SetActive(false);
            infotainment[15].SetActive(false);
            infotainment[16].SetActive(false);
            infotainment[17].SetActive(false);
            infotainment[18].SetActive(false);
            infotainment[21].SetActive(false);
            infotainment[22].SetActive(false);
            infotainment[23].SetActive(false);

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

            //passing parameter "1" to stay form active
            GetTexts[10].text = "1";

            //Menu Game Object Active here
            infotainment[26].SetActive(true);

            //Larceny Map Game Object Active Here
            scenes[0].SetActive(true);

            //App Music Active Here.
            gameMusic.SetActive(true);

          
        }
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    //That Function will be called after tap on Start Placement Card.
    public void StartMap()
    {
        //Larceny MAp Active here
        //scenes[0].SetActive(true);
        //Map Placement object active here
        placemap.SetActive(true);
        // disabling start placement card here.
        startplacement.SetActive(false);
    }

    public void cancelPlacement()
    {
        placeconfirm.SetActive(false);
        placemap.SetActive(false);
        startplacement.SetActive(true);
        //Larceny MAp Active here
        scenes[0].SetActive(false);

    }

    public void editsaved2()
    {

        if (GetTexts[10].text == "0")
        {
            updateInfoForm[0].SetActive(true);
            updateInfoForm[10].SetActive(true);
            updateInfoForm[6].SetActive(true);
            updateInfoForm[7].SetActive(true);

        }
        else
        {
            infotainment[20].SetActive(true);
            heading4winner.SetActive(true);
        }


        updateInfoForm[1].SetActive(false);
        updateInfoForm[2].SetActive(false);

        updateInfoForm[3].SetActive(true);

        updateInfoForm[4].SetActive(false);
        updateInfoForm[5].SetActive(false);
        updateInfoForm[8].SetActive(false);

        updateInfoForm[9].SetActive(false);

        infotainment[0].SetActive(false);
        infotainment[1].SetActive(false);
        infotainment[2].SetActive(false);
        infotainment[3].SetActive(false);
        infotainment[4].SetActive(false);
        infotainment[5].SetActive(false);
        infotainment[6].SetActive(false);
        infotainment[7].SetActive(false);
        infotainment[8].SetActive(false);
        infotainment[9].SetActive(false);
        infotainment[10].SetActive(false);
        infotainment[11].SetActive(false);
        infotainment[12].SetActive(false);
        infotainment[13].SetActive(false);
        infotainment[15].SetActive(false);
        infotainment[16].SetActive(false);
        infotainment[17].SetActive(false);
        infotainment[18].SetActive(false);
        infotainment[21].SetActive(false);
        infotainment[22].SetActive(false);
        infotainment[23].SetActive(false);

        //Menu Game Object Active here
        infotainment[26].SetActive(false);

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

        //Non Rickhouse Buildings Enabled
        nonrickenable[0].SetActive(false);
        nonrickenable[1].SetActive(false);
        nonrickenable[2].SetActive(false);
        nonrickenable[3].SetActive(false);
        nonrickenable[4].SetActive(false);
        nonrickenable[5].SetActive(false);

        menucontainer.SetActive(false);
        gameMusic.SetActive(false);

    }
}
