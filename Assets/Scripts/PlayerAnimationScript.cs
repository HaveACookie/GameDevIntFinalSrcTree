using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour {

	//Components
	private PlayerBehaviour player;
	
	//Settings
	[Header("Settings")] 
	[SerializeField] private float walk_time;
	[SerializeField] private float run_time;
	[SerializeField] private float knife_aim_time;
	[SerializeField] private float knife_attack_time;
	[SerializeField] private float gun_aim_time;
	[SerializeField] private float gun_attack_time;
	[SerializeField] private float shotgun_aim_time;
	[SerializeField] private float shotgun_attack_time;
	
	[Header("Animations")]
	[SerializeField] private GameObject anim_idle;
	[SerializeField] private GameObject anim_walk_1;
	[SerializeField] private GameObject anim_walk_2;
	[SerializeField] private GameObject anim_run_1; //3
	[SerializeField] private GameObject anim_run_2;
	[SerializeField] private GameObject anim_knife_aim_1; //5
	[SerializeField] private GameObject anim_knife_aim_2;
	[SerializeField] private GameObject anim_knife_aim_3;
	[SerializeField] private GameObject anim_knife_attack_1; //8
	[SerializeField] private GameObject anim_knife_attack_2;
	[SerializeField] private GameObject anim_gun_aim_1; //10
	[SerializeField] private GameObject anim_gun_aim_2;
	[SerializeField] private GameObject anim_gun_aim_3;
	[SerializeField] private GameObject anim_gun_shoot_1; //13
	[SerializeField] private GameObject anim_gun_shoot_2;
	[SerializeField] private GameObject anim_shotgun_aim_1; //15
	[SerializeField] private GameObject anim_shotgun_aim_2;
	[SerializeField] private GameObject anim_shotgun_aim_3;
	[SerializeField] private GameObject anim_shotgun_shoot_1; //18
	[SerializeField] private GameObject anim_shotgun_shoot_2;
	[SerializeField] private GameObject anim_hurt; //20
	[SerializeField] private GameObject anim_dead; //21
	
	//Variables
	private string animation_state;
	private float animation_timer;
	private int animation_index;
	private GameObject[] animations;

	private float invincibility_timer;
	
	//Init
	void Awake()
	{
		player = GetComponent<PlayerBehaviour>();
		animation_state = "idle";
		animation_timer = 0;
		animation_index = 0;
		animations = new GameObject[22];

		invincibility_timer = 0;
	}

	//Public Method
	public void Play(string animation_name)
	{
		//Turn off all active animations
		for (int i = 0; i < animations.Length; i++)
		{
			animations[i].SetActive(false);
		}
		
		//Reset Animation based on States
		if (animation_state != animation_name)
		{
			animation_timer = 0;
			animation_index = 0;
		}
		animation_state = animation_name;
		
		//Update Timer
		animation_timer -= Time.deltaTime;

		//Play Animation
		if (animation_name == "walk")
		{
			if (animation_timer < 0)
			{
				animation_timer = walk_time;
				if (animation_index == 0)
				{
					animation_index = 1;
				}
				else
				{
					animation_index = 0;
				}
			}
			
			animations[1 + animation_index].SetActive(true);
		}
		else if (animation_name == "run")
		{
			if (animation_timer < 0)
			{
				animation_timer = run_time;
				if (animation_index == 0)
				{
					animation_index = 1;
				}
				else
				{
					animation_index = 0;
				}
			}
			
			animations[3 + animation_index].SetActive(true);
		}
		else if (animation_name == "knife aim")
		{
			if (animation_timer < 0)
			{
				animation_timer = knife_aim_time;
				if (animation_index < 3)
				{
					animation_index++;
				}
			}
			animations[5 + Mathf.Clamp(animation_index - 1, 0, 2)].SetActive(true);
		}
		else if (animation_name == "knife attack")
		{
			if (animation_timer < 0)
			{
				animation_timer = knife_attack_time;
				if (animation_index < 2)
				{
					animation_index++;
				}
			}
			animations[8 + Mathf.Clamp(animation_index - 1, 0, 1)].SetActive(true);
		}
		else if (animation_name == "gun aim")
		{
			if (animation_timer < 0)
			{
				animation_timer = gun_aim_time;
				if (animation_index < 3)
				{
					animation_index++;
				}
			}
			animations[10 + Mathf.Clamp(animation_index - 1, 0, 2)].SetActive(true);
		}
		else if (animation_name == "gun attack")
		{
			if (animation_timer < 0)
			{
				animation_timer = gun_attack_time;
				if (animation_index < 2)
				{
					animation_index++;
				}
			}
			animations[13 + Mathf.Clamp(animation_index - 1, 0, 1)].SetActive(true);
		}
		else if (animation_name == "shotgun aim")
		{
			if (animation_timer < 0)
			{
				animation_timer = shotgun_aim_time;
				if (animation_index < 3)
				{
					animation_index++;
				}
			}
			animations[15 + Mathf.Clamp(animation_index - 1, 0, 2)].SetActive(true);
		}
		else if (animation_name == "shotgun attack")
		{
			if (animation_timer < 0)
			{
				animation_timer = shotgun_attack_time;
				if (animation_index < 2)
				{
					animation_index++;
				}
			}
			animations[18 + Mathf.Clamp(animation_index - 1, 0, 1)].SetActive(true);
		}
		else if (animation_name == "hurt")
		{
			animation_timer = 0;
			animation_index = 0;
			
			animations[20].SetActive(true);
		}
		else if (animation_name == "death")
		{
			animation_timer = 0;
			animation_index = 0;
			
			animations[21].SetActive(true);
		}
		else
		{
			animation_state = "idle";
			animation_timer = 0;
			animation_index = 0;
			
			animations[0].SetActive(true);
		}

		if (!player.canattack)
		{
			invincibility_timer -= Time.deltaTime;
			if (invincibility_timer < -2)
			{
				invincibility_timer = 2;
			}

			if (invincibility_timer < 0)
			{
				for (int i = 0; i < animations.Length; i++)
				{
					animations[i].SetActive(false);
				}
			}
		}
	}
}
