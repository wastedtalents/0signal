Shader "2DVLS/Cutout (2-Sided)" 
{
    Properties
    {
        _MainTex ("Base (RGB) Trans. (Alpha)", 2D) = "white" { }
		_Cutoff ("Alpha cutoff", Range (0,1)) = 1
	}

    Category
    {
        ZWrite On
        Cull Off
        Lighting Off

        SubShader
        {
		    Pass {
				ColorMask 0

				AlphaTest GEqual [_Cutoff]
				SetTexture [_MainTex] { }            
			}

            Pass
            {
           		Blend DstColor DstAlpha

           		AlphaTest GEqual [_Cutoff]
                SetTexture [_MainTex] { }             
            }
        } 
    }
}