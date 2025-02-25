﻿Shader "VertexColorFarmAnimals/VertexColorUnlit" {
Properties {
	_MainTex ("Texture", 3D) = "white" {}
}
 
Category {
	Tags { "Queue"="Geometry" }
	Lighting Off
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex

	}
 
	SubShader {
		Pass {
			SetTexture [_MainTex] {
				combine primary 
			}
		}
	}
}
}