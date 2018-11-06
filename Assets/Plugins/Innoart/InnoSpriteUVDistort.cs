using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class InnoSpriteUVDistort : MonoBehaviour {
 
	public string UV_property="_SpriteUV";
	public string Pivot_property="_SpritePivot";
	SpriteRenderer sr;
	Sprite sprite;
	MaterialPropertyBlock mpb;
   
	void OnValidate()
	{
		update();
	}
   
	void OnWillRenderObject(){
		update();
	}
   
	void Start(){
		update();
	}
   
	void update(){
		if(sr==null)
			sr = GetComponent<SpriteRenderer>();
       
		if(sprite != sr.sprite){
			sprite = sr.sprite;
			applySprite(sr, sprite, ref mpb, UV_property, Pivot_property);
		}
	}
   
	public static void applySprite(Renderer renderer, Sprite toSprite, ref MaterialPropertyBlock mpb,
		string uvProp=null, string pivotProp=null){
       
		if(toSprite==null) return;
       
		var scale = new Vector2(
			toSprite.textureRect.width/ toSprite.texture.width,
			toSprite.textureRect.height/toSprite.texture.height);
           
		var offset = new Vector2(
			toSprite.rect.x/toSprite.texture.width,
			toSprite.rect.y/toSprite.texture.height);
       
		Vector4 uvVector = new Vector4(scale.x,scale.y,offset.x,offset.y);
		Vector4 pivotVector = new Vector4(toSprite.pivot.x/toSprite.rect.width,toSprite.pivot.y/toSprite.rect.height);
       
		if(string.IsNullOrEmpty(uvProp))
			uvProp = "_MainTex_ST";
 
		if(mpb==null)
			mpb = new MaterialPropertyBlock();
 
		renderer.GetPropertyBlock(mpb);
       
		mpb.SetVector(uvProp, uvVector);
		if(!string.IsNullOrEmpty(pivotProp))
			mpb.SetVector(pivotProp, pivotVector);
       
		renderer.SetPropertyBlock(mpb);
	}
}