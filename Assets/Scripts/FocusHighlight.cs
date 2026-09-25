using System;
using UnityEngine;

public class FocusHighlight : MonoBehaviour
{
    private static readonly int IsHighlight = Shader.PropertyToID("_IsHighlight");
    [SerializeField] private Renderer[] targetRenderers;
    private Material[] _uniqueMaterials;
    public bool isHighlight = false;

    private void Start()
    {
        _uniqueMaterials = new Material[targetRenderers.Length];
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            Renderer renderer = targetRenderers[i];
            _uniqueMaterials[i] = new Material(renderer.sharedMaterial);
            renderer.material = _uniqueMaterials[i];
        }
    }
    
    private void LateUpdate()
    {
        foreach (Material material in _uniqueMaterials)
        {
            int value = isHighlight ? 1 : 0;
            material.SetInt(IsHighlight, value);
        }
        isHighlight = false;
    }
}