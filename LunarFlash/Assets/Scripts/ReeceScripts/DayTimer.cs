using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayTimer : MonoBehaviour
{
    public GameObject directionalLightObj;
    private Light directionalLight;
    private float dayLength;
    private bool daytime;
    float nextVal;
    bool dayLengthSetUp;
    bool dayLengthDefaulSetUp;
    bool dayLengthNeedstoUpdated = false;
    GameManager gmForLightControl;
    float timeMultiplier = 1;
    float intensityChange = 0.03f;
    bool isDayMax = false;
    // Start is called before the first frame update

    private void Awake()
    {
        directionalLight = directionalLightObj.GetComponent<Light>();
       directionalLight.intensity = 0.5f;
    }
    void Start()
    {
        isDayMax = false;
        intensityChange = 0.0003f;
        timeMultiplier = 3;
        if (gmForLightControl == null)
        {
            gmForLightControl = FindObjectOfType<GameManager>();
        }
        dayLengthSetUp = false;
        dayLengthDefaulSetUp = true;
        dayLengthNeedstoUpdated = false;
        directionalLight = directionalLightObj.GetComponent<Light>();
        //directionalLight.intensity = 0f;   //////////////////////////////////////////////////////////////////////////OPTION1
        directionalLight.intensity = 0.5f;///////////////////////////////////////////////////////////////////////////OPTION2
        Debug.Log("DirectionalLight intensity is " + directionalLight.intensity);
        Debug.Log("SIN VALUE IS--------------------"+Mathf.Sin(dayLength * Time.timeSinceLevelLoad * timeMultiplier));
        nextVal = 0;
        dayLength = 0.03f;
    }

 
    // Update is called once per frame
    void Update()
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////OPTION 1 LIne 54-67
        /* {
             nextVal = Mathf.Max((Mathf.Abs(Mathf.Sin(dayLength * Time.timeSinceLevelLoad))), (float)0.05);
             directionalLight.intensity = nextVal;
             if (nextVal > 0.5)
             {
                 daytime = true;
                 Time.timeScale = 1.5f;
             }
             else
             {
                 Time.timeScale = 1.0f;
                 daytime = false;
             }
         }*/


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////OPTION 2 Line 71 - 151
        {
            if (Mathf.Sin(dayLength * Time.timeSinceLevelLoad * timeMultiplier) < 0)//(directionalLight.intensity > 0.5)//(nextVal> 0.5)
            {

                //Debug.Log("DAY TIME=====Tan is " + Mathf.Tan(dayLength * Time.timeSinceLevelLoad * timeMultiplier));
                daytime = true;

                if (Mathf.Tan(dayLength * Time.timeSinceLevelLoad * timeMultiplier) > 0)
                {
                    if (directionalLight.intensity < 1)  // keep getting brighter
                    { directionalLight.intensity += intensityChange; }
                    else
                    {
                        directionalLight.intensity = 1;  //dayMax
                    }
                }
                else
                {
                    if (directionalLight.intensity > 0.5) //getting darker
                    { directionalLight.intensity -= intensityChange; }
                    else
                    {
                        directionalLight.intensity = 0.5f; //Day starts
                    }
                }

                if (directionalLight.intensity < 0.52) { dayLengthNeedstoUpdated = true; }

                if (dayLengthNeedstoUpdated == true && dayLengthSetUp == true)
                {
                    dayLengthDefaulSetUp = true;
                    dayLengthNeedstoUpdated = false;
                    dayLengthSetUp = false;

                    SetUpIntensityChange(gmForLightControl.GetWaveNum(), daytime);
                    Debug.Log("================DAY Condition");

                }

            }
            else //when night
            {
                daytime = false;
                // Debug.Log("NIGHT TIME=====Tan is " + Mathf.Tan(dayLength * Time.timeSinceLevelLoad * timeMultiplier));
                if (Mathf.Tan(dayLength * Time.timeSinceLevelLoad * timeMultiplier) > 0)
                {
                    if (directionalLight.intensity > 0) //getting darker
                    { directionalLight.intensity -= intensityChange; }
                    else
                    {
                        directionalLight.intensity = 0; // complete dark
                    }
                }
                else
                {
                    if (directionalLight.intensity < 0.5) //getting brigther
                    { directionalLight.intensity += intensityChange; }
                    else
                    {
                        directionalLight.intensity = 0.5f; //Day starts
                    }
                }


                if (directionalLight.intensity > 0.49) { dayLengthNeedstoUpdated = true; }
                //  else { dayLengthNeedstoUpdated = false; dayLengthSetUp = true; }


                if (dayLengthDefaulSetUp == true && dayLengthNeedstoUpdated == true)//&& dayLengthSetUp == false)
                {
                    timeMultiplier = 2;
                    Time.timeScale = 1.0f;
                    dayLengthDefaulSetUp = false;
                    dayLengthSetUp = true;
                    dayLengthNeedstoUpdated = false;
                    intensityChange = 0.0003f;
                    Debug.Log("================NightCondition");

                }
            }
        }
    }

    public float getDayTime()
    {
       // return Mathf.Max((Mathf.Abs(Mathf.Sin(dayLength * Time.timeSinceLevelLoad))), (float)0.05);/////////////////////////////////////////OPTION1
        return directionalLight.intensity; /////////////////////////////////////////////////////////////////////////////////////////////////////////////OPTION2
    }

    public bool getDayTimeUsingSin()
    {
        if (daytime)
        {
            if (Mathf.Sin(dayLength * Time.timeSinceLevelLoad * timeMultiplier) < 0.05 && Mathf.Sin(dayLength * Time.timeSinceLevelLoad * timeMultiplier) > -0.005)
            {
                isDayMax = true;
                return isDayMax;
            }
          //  else
           // {
          //      isDayMax = false;
          //      return isDayMax;
          //  }
        }

        isDayMax = false;
        return isDayMax;
    }

    public bool isDay()
    {
        //Debug.Log(daytime);
        return daytime;
    }

    public void SetUpDayLength(int wave, bool isDay)
    {
        if (isDay)
        {
            if ((wave) == 1)
            {
                dayLength = 0.00f;
            }
            else if((wave) == 2)
            {
                dayLength = 0.13f;
            }
           /* else if (wave == 3)
            {
                dayLength = 0.08f;
            }*/
        }
        else
        {

        }

    }

    public void SetUpLightMultiplier(int wave, bool isDay)
    {
        if (isDay)
        {
            if ((wave) == 1)
            {
                timeMultiplier = 2.4f;
            }
            else if ((wave) == 2)
            {
                timeMultiplier = 2.8f;
            }
            /* else if (wave == 3)
             {
                 dayLength = 0.08f;
             }*/
        }
        else
        {
            if ((wave) == 2)
            {
                timeMultiplier = 10/14f;
            }
            else if ((wave) == 3)
            {
                timeMultiplier = 10/18f;
            }

        }

    }

    public void SetUpIntensityChange(int wave, bool isDay)
    {
        if (isDay)
        {
            if ((wave) == 1)
            {
                Time.timeScale = 1.3f;
                intensityChange = 0.0009f;
                Debug.Log("Wave 1 =============  Intensity = " + intensityChange);
            }
            else if ((wave) == 2)
            {
                Time.timeScale = 1.5f;
                intensityChange = 0.0015f;
                Debug.Log("Wave 2 =============  Intensity = " + intensityChange);
            }
            /* else if (wave == 3)
             {
                 dayLength = 0.08f;
             }*/
        }
    }
   
}
