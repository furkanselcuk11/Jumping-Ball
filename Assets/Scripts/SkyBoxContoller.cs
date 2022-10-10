using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxContoller : MonoBehaviour
{
    [SerializeField] private Material[] skyBoxMaterials;
    public int skyBoxMaterialNumber=0;

    public static SkyBoxContoller skyBoxContollerInstance;
    private void Awake()
    {
        if (skyBoxContollerInstance == null)
        {
            skyBoxContollerInstance = this;
        }
    }
    void Start()
    {
        RenderSettings.skybox = skyBoxMaterials[skyBoxMaterialNumber];
    }    
    void Update()
    {
        RenderSettings.skybox = skyBoxMaterials[skyBoxMaterialNumber];
    }
    public int SkyBoxChange(int x)
    {
        skyBoxMaterialNumber = x;
        return skyBoxMaterialNumber;
    }
}
