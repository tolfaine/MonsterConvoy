// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32767,y:32647,varname:node_2865,prsc:2|diff-6250-OUT,spec-1487-OUT,gloss-105-OUT,normal-7983-OUT;n:type:ShaderForge.SFN_Vector1,id:1487,x:32525,y:32694,varname:node_1487,prsc:2,v1:0;n:type:ShaderForge.SFN_VertexColor,id:7105,x:31408,y:33052,varname:node_7105,prsc:2;n:type:ShaderForge.SFN_Lerp,id:3850,x:32093,y:32636,varname:node_3850,prsc:2|A-8372-RGB,B-1358-RGB,T-7105-G;n:type:ShaderForge.SFN_Tex2d,id:8372,x:31832,y:32443,ptovrint:False,ptlb:Diff,ptin:_Diff,varname:node_8372,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1358,x:31842,y:32626,ptovrint:False,ptlb:Diff GREEN,ptin:_DiffGREEN,varname:node_1358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6347,x:32088,y:32473,ptovrint:False,ptlb:Diff BLUE,ptin:_DiffBLUE,varname:node_6347,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:6250,x:32322,y:32640,varname:node_6250,prsc:2|A-3850-OUT,B-6347-RGB,T-7105-B;n:type:ShaderForge.SFN_Tex2d,id:7859,x:31847,y:32833,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_7859,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2921,x:31847,y:33005,ptovrint:False,ptlb:Gloss GREEN,ptin:_GlossGREEN,varname:node_2921,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9732,x:32084,y:32850,ptovrint:False,ptlb:Gloss BLUE,ptin:_GlossBLUE,varname:node_9732,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:2931,x:32090,y:33005,varname:node_2931,prsc:2|A-7859-R,B-2921-R,T-7105-G;n:type:ShaderForge.SFN_Lerp,id:105,x:32342,y:33010,varname:node_105,prsc:2|A-2931-OUT,B-9732-R,T-7105-B;n:type:ShaderForge.SFN_Tex2d,id:1662,x:31853,y:33406,ptovrint:False,ptlb:Normal GREEN,ptin:_NormalGREEN,varname:node_1662,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3914,x:31852,y:33240,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_3914,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:8944,x:32077,y:33219,ptovrint:False,ptlb:Normal BLUE,ptin:_NormalBLUE,varname:node_8944,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Lerp,id:9456,x:32080,y:33387,varname:node_9456,prsc:2|A-3914-RGB,B-1662-RGB,T-7105-G;n:type:ShaderForge.SFN_Lerp,id:7983,x:32284,y:33396,varname:node_7983,prsc:2|A-9456-OUT,B-8944-RGB,T-7105-B;proporder:8372-1358-6347-7859-2921-9732-1662-3914-8944;pass:END;sub:END;*/

Shader "Shader Forge/blend" {
    Properties {
        _Diff ("Diff", 2D) = "white" {}
        _DiffGREEN ("Diff GREEN", 2D) = "white" {}
        _DiffBLUE ("Diff BLUE", 2D) = "white" {}
        _Gloss ("Gloss", 2D) = "white" {}
        _GlossGREEN ("Gloss GREEN", 2D) = "white" {}
        _GlossBLUE ("Gloss BLUE", 2D) = "white" {}
        _NormalGREEN ("Normal GREEN", 2D) = "bump" {}
        _Normal ("Normal", 2D) = "bump" {}
        _NormalBLUE ("Normal BLUE", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Diff; uniform float4 _Diff_ST;
            uniform sampler2D _DiffGREEN; uniform float4 _DiffGREEN_ST;
            uniform sampler2D _DiffBLUE; uniform float4 _DiffBLUE_ST;
            uniform sampler2D _Gloss; uniform float4 _Gloss_ST;
            uniform sampler2D _GlossGREEN; uniform float4 _GlossGREEN_ST;
            uniform sampler2D _GlossBLUE; uniform float4 _GlossBLUE_ST;
            uniform sampler2D _NormalGREEN; uniform float4 _NormalGREEN_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _NormalBLUE; uniform float4 _NormalBLUE_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 _NormalGREEN_var = UnpackNormal(tex2D(_NormalGREEN,TRANSFORM_TEX(i.uv0, _NormalGREEN)));
                float3 _NormalBLUE_var = UnpackNormal(tex2D(_NormalBLUE,TRANSFORM_TEX(i.uv0, _NormalBLUE)));
                float3 normalLocal = lerp(lerp(_Normal_var.rgb,_NormalGREEN_var.rgb,i.vertexColor.g),_NormalBLUE_var.rgb,i.vertexColor.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Gloss_var = tex2D(_Gloss,TRANSFORM_TEX(i.uv0, _Gloss));
                float4 _GlossGREEN_var = tex2D(_GlossGREEN,TRANSFORM_TEX(i.uv0, _GlossGREEN));
                float4 _GlossBLUE_var = tex2D(_GlossBLUE,TRANSFORM_TEX(i.uv0, _GlossBLUE));
                float gloss = lerp(lerp(_Gloss_var.r,_GlossGREEN_var.r,i.vertexColor.g),_GlossBLUE_var.r,i.vertexColor.b);
                float perceptualRoughness = 1.0 - lerp(lerp(_Gloss_var.r,_GlossGREEN_var.r,i.vertexColor.g),_GlossBLUE_var.r,i.vertexColor.b);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float4 _Diff_var = tex2D(_Diff,TRANSFORM_TEX(i.uv0, _Diff));
                float4 _DiffGREEN_var = tex2D(_DiffGREEN,TRANSFORM_TEX(i.uv0, _DiffGREEN));
                float4 _DiffBLUE_var = tex2D(_DiffBLUE,TRANSFORM_TEX(i.uv0, _DiffBLUE));
                float3 diffuseColor = lerp(lerp(_Diff_var.rgb,_DiffGREEN_var.rgb,i.vertexColor.g),_DiffBLUE_var.rgb,i.vertexColor.b); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Diff; uniform float4 _Diff_ST;
            uniform sampler2D _DiffGREEN; uniform float4 _DiffGREEN_ST;
            uniform sampler2D _DiffBLUE; uniform float4 _DiffBLUE_ST;
            uniform sampler2D _Gloss; uniform float4 _Gloss_ST;
            uniform sampler2D _GlossGREEN; uniform float4 _GlossGREEN_ST;
            uniform sampler2D _GlossBLUE; uniform float4 _GlossBLUE_ST;
            uniform sampler2D _NormalGREEN; uniform float4 _NormalGREEN_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _NormalBLUE; uniform float4 _NormalBLUE_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 _NormalGREEN_var = UnpackNormal(tex2D(_NormalGREEN,TRANSFORM_TEX(i.uv0, _NormalGREEN)));
                float3 _NormalBLUE_var = UnpackNormal(tex2D(_NormalBLUE,TRANSFORM_TEX(i.uv0, _NormalBLUE)));
                float3 normalLocal = lerp(lerp(_Normal_var.rgb,_NormalGREEN_var.rgb,i.vertexColor.g),_NormalBLUE_var.rgb,i.vertexColor.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Gloss_var = tex2D(_Gloss,TRANSFORM_TEX(i.uv0, _Gloss));
                float4 _GlossGREEN_var = tex2D(_GlossGREEN,TRANSFORM_TEX(i.uv0, _GlossGREEN));
                float4 _GlossBLUE_var = tex2D(_GlossBLUE,TRANSFORM_TEX(i.uv0, _GlossBLUE));
                float gloss = lerp(lerp(_Gloss_var.r,_GlossGREEN_var.r,i.vertexColor.g),_GlossBLUE_var.r,i.vertexColor.b);
                float perceptualRoughness = 1.0 - lerp(lerp(_Gloss_var.r,_GlossGREEN_var.r,i.vertexColor.g),_GlossBLUE_var.r,i.vertexColor.b);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float4 _Diff_var = tex2D(_Diff,TRANSFORM_TEX(i.uv0, _Diff));
                float4 _DiffGREEN_var = tex2D(_DiffGREEN,TRANSFORM_TEX(i.uv0, _DiffGREEN));
                float4 _DiffBLUE_var = tex2D(_DiffBLUE,TRANSFORM_TEX(i.uv0, _DiffBLUE));
                float3 diffuseColor = lerp(lerp(_Diff_var.rgb,_DiffGREEN_var.rgb,i.vertexColor.g),_DiffBLUE_var.rgb,i.vertexColor.b); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _Diff; uniform float4 _Diff_ST;
            uniform sampler2D _DiffGREEN; uniform float4 _DiffGREEN_ST;
            uniform sampler2D _DiffBLUE; uniform float4 _DiffBLUE_ST;
            uniform sampler2D _Gloss; uniform float4 _Gloss_ST;
            uniform sampler2D _GlossGREEN; uniform float4 _GlossGREEN_ST;
            uniform sampler2D _GlossBLUE; uniform float4 _GlossBLUE_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _Diff_var = tex2D(_Diff,TRANSFORM_TEX(i.uv0, _Diff));
                float4 _DiffGREEN_var = tex2D(_DiffGREEN,TRANSFORM_TEX(i.uv0, _DiffGREEN));
                float4 _DiffBLUE_var = tex2D(_DiffBLUE,TRANSFORM_TEX(i.uv0, _DiffBLUE));
                float3 diffColor = lerp(lerp(_Diff_var.rgb,_DiffGREEN_var.rgb,i.vertexColor.g),_DiffBLUE_var.rgb,i.vertexColor.b);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0.0, specColor, specularMonochrome );
                float4 _Gloss_var = tex2D(_Gloss,TRANSFORM_TEX(i.uv0, _Gloss));
                float4 _GlossGREEN_var = tex2D(_GlossGREEN,TRANSFORM_TEX(i.uv0, _GlossGREEN));
                float4 _GlossBLUE_var = tex2D(_GlossBLUE,TRANSFORM_TEX(i.uv0, _GlossBLUE));
                float roughness = 1.0 - lerp(lerp(_Gloss_var.r,_GlossGREEN_var.r,i.vertexColor.g),_GlossBLUE_var.r,i.vertexColor.b);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
