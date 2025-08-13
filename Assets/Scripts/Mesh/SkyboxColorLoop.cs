using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxColorLoop : MonoBehaviour
{
    public Material skyboxMaterial;
    public Color color1 = Color.blue;
    public Color color2 = Color.magenta;
    public float changeSpeed = 1f;

    private Material runtimeSkyboxMaterial;

    private void Start()
    {
        runtimeSkyboxMaterial = new Material(skyboxMaterial);
        RenderSettings.skybox = runtimeSkyboxMaterial;
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * changeSpeed, 1f);
        Color newColor = Color.Lerp(color1, color2, t);
        runtimeSkyboxMaterial.SetColor("_SkyTint", newColor);
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
        DynamicGI.UpdateEnvironment();
    }
}