using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    public GameObject directionalLightObj;
    private Light directionalLight;
    [SerializeField] private float dayLength;
    private bool daytime;
    float nextVal;
    // Start is called before the first frame update

    private void Awake()
    {
        directionalLight = directionalLightObj.GetComponent<Light>();
       directionalLight.intensity = 0;
    }
    void Start()
    {
        directionalLight = directionalLightObj.GetComponent<Light>();
        directionalLight.intensity = 0;
        Debug.Log("DirectionalLight intensity is " + directionalLight.intensity);
        nextVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        nextVal =Mathf.Max((Mathf.Abs(Mathf.Sin(dayLength * Time.timeSinceLevelLoad))), (float)0.05);
        directionalLight.intensity = nextVal;
        if(nextVal > 0.5)
        {
            daytime = true;
        }
        else
        {
            daytime = false;
        }
    }

    public float getDayTime()
    {
        return Mathf.Max((Mathf.Abs(Mathf.Sin(dayLength * Time.timeSinceLevelLoad))), (float)0.05);
    }

    public bool isDay()
    {
        //Debug.Log(daytime);
        return daytime;
    }
}
