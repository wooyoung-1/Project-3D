using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MeshColorSetter : MonoBehaviour
{
    [Header("Color")]
    public Color meshColor = Color.white;

    MaterialPropertyBlock mpb;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    void Start()
    {
        ApplyColor();
    }

    void OnValidate()
    {
        if (Application.isPlaying && rend != null)
        {
            ApplyColor();
        }
    }

    void ApplyColor()
    {
        mpb.Clear();
        mpb.SetColor("_Color", meshColor);
        rend.SetPropertyBlock(mpb);
    }
}

