using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    public GameObject directionalLightObj;
    private Light directionalLight;
    [SerializeField] private float dayLength;
    private bool daytime;
    // Start is called before the first frame update
    void Start()
    {
        directionalLight = directionalLightObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float nextVal = Mathf.Max((Mathf.Abs(Mathf.Sin(dayLength * Time.time))), (float)0.05);
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

    public bool isDay()
    {
        Debug.Log(daytime);
        return daytime;
    }
}
