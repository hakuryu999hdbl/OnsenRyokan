using System;
using UnityEngine;

public sealed class SkyboxManager : MonoBehaviour
{
    [SerializeField] private Material[] skyboxes = Array.Empty<Material>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
            CycleSkybox();
    }

    public void CycleSkybox()
    {
        if (skyboxes.Length == 0)
            return;

        int currentIndex = Array.IndexOf(skyboxes, RenderSettings.skybox);
        int nextIndex = (currentIndex + 1) % skyboxes.Length;
        SetSkybox(nextIndex);
    }

    public void SetSkybox(int index)
    {
        if (index < 0 || index >= skyboxes.Length || skyboxes[index] == null)
            return;

        RenderSettings.skybox = skyboxes[index];
    }
}
