using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    public GameObject directionalLightObj;
    private Light directionalLight;
    [SerializeField] private float dayLength;
    // Start is called before the first frame update
    void Start()
    {
        directionalLight = directionalLightObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.intensity = Mathf.Max((Mathf.Abs(Mathf.Sin(dayLength * Time.time))), (float)0.05);
    }
}
