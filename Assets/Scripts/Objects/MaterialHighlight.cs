using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MaterialHighlight : MonoBehaviour
{
    private static readonly int IsHighlight = Shader.PropertyToID("_IsHighlight");
    [SerializeField] private Renderer[] targetRenderers;
    private Material[] _uniqueMaterials;

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

    public async void HighlightThisFrame()
    {
        foreach (Material mat in _uniqueMaterials)
        {
            if (mat != null)
            {
                mat.SetInt(IsHighlight, 1);
            }
        }
        await UniTask.Yield();
        foreach (Material mat in _uniqueMaterials)
        {
            if (mat != null)
            {
                mat.SetInt(IsHighlight, 0);
            }
        }
    }
}