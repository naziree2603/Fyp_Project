using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DynamicRes : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 1.0f;
    public float targetFPS = 60f;

    UniversalRenderPipelineAsset urpAsset;

    float fpsTimer;

    void Start()
    {
        urpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
    }

    void Update()
    {
        fpsTimer = 1f / Time.deltaTime;
        if (urpAsset != null)
        {
            if(fpsTimer < targetFPS - 5f)
            {
                urpAsset.renderScale = Mathf.Max(minScale, urpAsset.renderScale - 0.05f);
            }
            else if (fpsTimer > targetFPS + 5f)
            {
                urpAsset.renderScale = Mathf.Min(maxScale, urpAsset.renderScale + 0.05f);
            }

        }
    }
}
