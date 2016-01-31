using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CustomPostProcessingF : PostEffectsBase {

    public override bool CheckResources()
    {
        CheckSupport(true);


        if (!isSupported)
        {
            ReportAutoDisable();
        }
        return isSupported;
    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

    }

}
