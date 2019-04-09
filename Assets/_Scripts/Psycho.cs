using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
[Serializable]
[PostProcess(typeof(PsychoRenderer), PostProcessEvent.AfterStack, "Custom/Psycho")]
public sealed class Psycho : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Level of Distortion")]
    public FloatParameter distortion = new FloatParameter { value = 0.5f };

    public float TimeX = 1.0f;
}
 
public sealed class PsychoRenderer : PostProcessEffectRenderer<Psycho>
{
    public override void Render(PostProcessRenderContext context)
    {
        Debug.Log(settings.TimeX);
        var sheet = context.propertySheets.Get(Shader.Find("CameraFilterPack/FX_Psycho"));
        settings.TimeX+=Time.deltaTime;
        sheet.properties.SetFloat("_TimeX", settings.TimeX);
        sheet.properties.SetFloat("_Distortion", settings.distortion);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
