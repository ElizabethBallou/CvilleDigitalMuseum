using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

#if UNITY_EDITOR
using UnityEditor.Rendering.PostProcessing;
#endif

[Serializable]
[PostProcess(typeof(GlitchImageEffectRenderer), PostProcessEvent.BeforeStack, "Custom/GlitchImageEffect")]
public class GlitchImageEffect : PostProcessEffectSettings
{   
    public GlitchTypeParameter glitchType = new GlitchTypeParameter() { value = GlitchType.Type1 };

    [Range(0, 1)]
    public FloatParameter blend = new FloatParameter() { value = 1 };

    [Header("Parameters of Type1")]
    [Range(0, 10)]
    public FloatParameter frequency = new FloatParameter() { value = 1 };

    [Range(0, 500)]
	public FloatParameter interference = new FloatParameter() { value = 130 };

    [Range(0, 5)]
	public FloatParameter noise = new FloatParameter() { value = 0.15f } ;

    [Range(0, 20)]
	public FloatParameter scanLine = new FloatParameter() { value = 1 };

    [Range(0, 1)]
	public FloatParameter colored = new FloatParameter() { value = 0.25f };

    [Header("Parameters of Type3")]
    [Range(0, 30)]
    public FloatParameter intensityType3 = new FloatParameter() { value = 10 };

    [Header("Parameters of Type4")]
    [Range(100, 500)]
    public FloatParameter lines = new FloatParameter() { value = 240 };

    [Range(1, 6)]
    public FloatParameter scanSpeed = new FloatParameter() { value = 2 };

    [Range(0.1f, 0.9f)]
    public FloatParameter linesThreshold = new FloatParameter() { value = 0.7f };

    [Range(0, 0.8f)]
    public FloatParameter exposure = new FloatParameter() { value = 0.3f };
}

public enum GlitchType
{
    Type1 = 0, 
    Type2 = 1,
    Type3 = 2,
    Type4 = 3
}

[Serializable]
public class GlitchTypeParameter : ParameterOverride<GlitchType> {}

public sealed class GlitchImageEffectRenderer : PostProcessEffectRenderer<GlitchImageEffect>
{
    private Shader shader = null;

    private Texture2D noiseTex = null;

    public override void Render(PostProcessRenderContext iContext)
    {
        if (shader == null)
        {
            shader = Shader.Find("Hidden/GlitchImageEffect");
        }
        if(noiseTex == null)
        {
            noiseTex = Resources.Load<Texture2D>("Texture/GlitchNoiseTex");
        }
        if(shader != null && noiseTex != null)
        {
            var sheet = iContext.propertySheets.Get(shader);

            sheet.properties.SetFloat("_Blend", settings.blend);
            sheet.properties.SetFloat("_Frequency", settings.frequency);
            sheet.properties.SetFloat("_Interference", settings.interference);
            sheet.properties.SetFloat("_Noise", settings.noise);
            sheet.properties.SetFloat("_ScanLine", settings.scanLine);
            sheet.properties.SetFloat("_Colored", settings.colored);
            sheet.properties.SetTexture("_NoiseTex", noiseTex);
            sheet.properties.SetFloat("_IntensityType3", settings.intensityType3);
            sheet.properties.SetFloat("_Lines", settings.lines);
            sheet.properties.SetFloat("_ScanSpeed", settings.scanSpeed);
            sheet.properties.SetFloat("_LinesThreshold", settings.linesThreshold);
            sheet.properties.SetFloat("_Exposure", settings.exposure);

            iContext.command.BlitFullscreenTriangle(iContext.source, iContext.destination, sheet, (int)settings.glitchType.value);
        }
    }

    public override void Release()
    {
        base.Release();

        shader = null;
        if(noiseTex != null)
        {
            Resources.UnloadAsset(noiseTex);
        }
    }
}

#if UNITY_EDITOR
[PostProcessEditor(typeof(GlitchImageEffect))]
public class GlitchImageEffectEditor : PostProcessEffectEditor<GlitchImageEffect>
{
    SerializedParameterOverride glitchType;
    SerializedParameterOverride blend;
    SerializedParameterOverride frequency;
    SerializedParameterOverride interference;
    SerializedParameterOverride noise;
    SerializedParameterOverride scanLine;
    SerializedParameterOverride colored;
    SerializedParameterOverride intensityType3;
    SerializedParameterOverride lines;
    SerializedParameterOverride scanSpeed;
    SerializedParameterOverride linesThreshold;
    SerializedParameterOverride exposure;

    public override void OnEnable()
    {
        base.OnEnable();

        glitchType = FindParameterOverride(x => x.glitchType);
        blend = FindParameterOverride(x => x.blend);
        frequency = FindParameterOverride(x => x.frequency);
        interference = FindParameterOverride(x => x.interference);
        noise = FindParameterOverride(x => x.noise);
        scanLine = FindParameterOverride(x => x.scanLine);
        colored = FindParameterOverride(x => x.colored);
        intensityType3 = FindParameterOverride(x => x.intensityType3);
        lines = FindParameterOverride(x => x.lines);
        scanSpeed = FindParameterOverride(x => x.scanSpeed);
        linesThreshold = FindParameterOverride(x => x.linesThreshold);
        exposure = FindParameterOverride(x => x.exposure);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PropertyField(glitchType);
        PropertyField(blend);
        PropertyField(frequency);
        PropertyField(interference);
        PropertyField(noise);
        PropertyField(scanLine);
        PropertyField(colored);
        PropertyField(intensityType3);
        PropertyField(lines);
        PropertyField(scanSpeed);
        PropertyField(linesThreshold);
        PropertyField(exposure);
    }
}
#endif