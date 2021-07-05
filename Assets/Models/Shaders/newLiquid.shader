Shader "Unlit/newLiquid"
{
	Properties
	{
		_LiquidColor("LiquidColor", Color) = (0.2,0.1,0.1,0.9)
		_WaterLevel("WaterLevel", range(-0.3, 1)) = 0
		_Wobble("Wobble",range(0, 0.5)) = 0
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
		LOD 100

		Pass//第一个pass先渲染瓶中的液体
		{
			Blend SrcAlpha OneMinusSrcAlpha
			//这里要双面渲染液体
		Cull off
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag			
		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			//顶点输出中增加世界空间坐标
			float4 worldPos : TEXCOORD0;
		};
		float4 _LiquidColor;
		float _WaterLevel;
		float _Wobble;
		//uniform float4 _Volum;
		fixed2 randVec(fixed2 value)
		{
			fixed2 vec = fixed2(dot(value, fixed2(127.1, 337.1)), dot(value, fixed2(269.5, 183.3)));
			vec = -1 + 2 * frac(sin(vec) * 43758.5453123);
			return vec;
		}

		float perlinNoise(float2 uv)
		{
			float a, b, c, d;
			float x0 = floor(uv.x);
			float x1 = ceil(uv.x);
			float y0 = floor(uv.y);
			float y1 = ceil(uv.y);
			fixed2 pos = frac(uv);
			a = dot(randVec(fixed2(x0, y0)), pos - fixed2(0, 0));
			b = dot(randVec(fixed2(x0, y1)), pos - fixed2(0, 1));
			c = dot(randVec(fixed2(x1, y1)), pos - fixed2(1, 1));
			d = dot(randVec(fixed2(x1, y0)), pos - fixed2(1, 0));
			float2 st = 6 * pow(pos, 5) - 15 * pow(pos, 4) + 10 * pow(pos, 3);
			a = lerp(a, d, st.x);
			b = lerp(b, c, st.x);
			a = lerp(a, b, st.y);
			return a;
		}
		v2f vert(appdata v)
		{
			v2f o;
			//这里就是对瓶子进行缩放，比例决定瓶子的厚度，后面加了一个向上的小偏移，因为瓶底儿应该更厚点儿。
			float4 localPos = float4(0.9, 0.9, 0.98, 1) * v.vertex + float4(0, 0, 0.01, 0);
			//o.worldPos = mul(unity_ObjectToWorld, localPos);
			o.worldPos = mul(unity_ObjectToWorld, localPos) - mul(unity_ObjectToWorld, float4(0, 0, 0, 1));
			o.vertex = UnityObjectToClipPos(localPos);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			float noiseValue = 0.5 * abs(frac(i.worldPos.xz + i.worldPos.zx + float2(_Time.y, 1.5 * _Time.y)) - 0.5);
			//将之前计算的液面高度加上这次得到的随机值
		    _WaterLevel -= 0.3 * _Wobble * perlinNoise(noiseValue);
			//取到最高，最低点
			//float heightMax = _Volum.x;
			//float heightMin = _Volum.y;
			//直接根据设置的液面高度，对最低和最高顶点进行插值一下，求出液面实际高度
			//float waterLevel = heightMin + _WaterLevel * (heightMax - heightMin);
			clip(_WaterLevel - i.worldPos.y);
			return _LiquidColor;
		}

		ENDCG
		}


	}
}