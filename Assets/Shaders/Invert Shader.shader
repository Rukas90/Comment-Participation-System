Shader "Custom/InvertShader" 
{
	Properties
    {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)

		_StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    SubShader
    {
        Tags 
		{ 
			"Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
		}
 
		Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Pass
        {
           ZWrite On
           ColorMask 0
        }

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		ColorMask [_ColorMask]
        Blend OneMinusDstColor OneMinusSrcAlpha //invert blending, so long as FG color is 1,1,1,1
        BlendOp Add
       
        Pass
        {
			Name "Invert"

			CGPROGRAM

			#pragma vertex    vert
			#pragma fragment  frag
			#pragma target 2.0

			#include "UnityCG.cginc"
            #include "UnityUI.cginc"

			#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
 
			struct vertexInput
			{
				float4 vertex: POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
			};
 
			struct fragmentInput
			{
				float4 pos : SV_POSITION;
				float4 color : COLOR0;
				float2  texcoord : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
			};
 
			uniform float4 _Color;
			sampler2D _MainTex;
			float4 _TextureSampleAdd;
			float4 _ClipRect;
			float4 _MainTex_ST;

			fragmentInput vert(vertexInput i)
			{
				fragmentInput o;
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.worldPosition = i.vertex;
				o.pos = UnityObjectToClipPos(o.worldPosition);

				o.texcoord = TRANSFORM_TEX(i.texcoord, _MainTex);
				o.color = _Color;

				return o;
			}
 
			half4 frag(fragmentInput i) : COLOR
			{
				half4 color = (tex2D(_MainTex, i.texcoord) + _TextureSampleAdd);
				color.rgb *= i.color.rgb;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

				return color;
			}
			ENDCG
		}
	}
}