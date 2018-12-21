using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour {

	//Settings
	[Header("Settings")] 
	[SerializeField] private float walk_time;
	[SerializeField] private float y_offset;
	[SerializeField] private float size = 1;
	
	[Header("Animations")]
	[SerializeField] private GameObject anim_idle;
	[SerializeField] private GameObject anim_walk_1;
	[SerializeField] private GameObject anim_walk_2;
	[SerializeField] private GameObject anim_leap;
	[SerializeField] private GameObject anim_death;
	
	//Variables
	private float walk_timer;
	private bool walk_switch;
	private GameObject[] animations;

	void Awake()
	{
		walk_switch = false;
		animations = new GameObject[5];
		if (anim_idle != null)
		{
			animations[0] = Instantiate(anim_idle, Vector3.zero, Quaternion.Euler(0, 0, 0));
			animations[0].transform.SetParent(transform);
			animations[0].transform.localScale = new Vector3(1, 1, 1) * size;
			animations[0].transform.localPosition = new Vector3(0, -y_offset, 0);
		}
		if (anim_walk_1 != null)
		{
			animations[1] = Instantiate(anim_walk_1, Vector3.zero, Quaternion.Euler(0, 0, 0));
			animations[1].transform.SetParent(transform);
			animations[1].transform.localScale = new Vector3(1, 1, 1) * size;
			animations[1].transform.localPosition = new Vector3(0, -y_offset, 0);
		}
		if (anim_walk_2 != null)
		{
			animations[2] = Instantiate(anim_walk_2, Vector3.zero, Quaternion.Euler(0, 0, 0));
			animations[2].transform.SetParent(transform);
			animations[2].transform.localScale = new Vector3(1, 1, 1) * size;
			animations[2].transform.localPosition = new Vector3(0, -y_offset, 0);
		}
		if (anim_leap != null)
		{
			animations[3] = Instantiate(anim_leap, Vector3.zero, Quaternion.Euler(0, 0, 0));
			animations[3].transform.SetParent(transform);
			animations[3].transform.localScale = new Vector3(1, 1, 1) * size;
			animations[3].transform.localPosition = new Vector3(0, -y_offset, 0);
		}
		if (anim_death != null)
		{
			animations[4] = Instantiate(anim_death, Vector3.zero, Quaternion.Euler(anim_death.transform.eulerAngles));
			animations[4].transform.SetParent(transform);
			animations[4].transform.localScale = new Vector3(1, 1, 1) * size;
			animations[4].transform.localPosition = new Vector3(0, -1f, 0);
		}
		
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i] != null)
			{
				animations[i].SetActive(false);
			}
		}
		transform.GetChild(0).gameObject.SetActive(false);
	}

	public void Play(string animation_name)
	{
		if (animations[3] != null)
		{
			if (animations[3].activeSelf)
			{
				float dis_to_ground = GetComponent<Collider>().bounds.extents.y;
				bool grounded = Physics.Raycast(transform.position, -Vector3.up, dis_to_ground + 0.1f);
				if (grounded)
				{
					animations[3].SetActive(false);
				}
				else
				{
					return;
				}
			}
		}

		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i] != null)
			{
				animations[i].transform.eulerAngles = new Vector3(animations[i].transform.eulerAngles.x, transform.eulerAngles.y, animations[i].transform.eulerAngles.z);
				animations[i].SetActive(false);
			}
		}
		
		if (animation_name == "walk")
		{
			walk_timer -= Time.deltaTime;
			if (walk_timer <= 0)
			{
				walk_timer = walk_time;
				walk_switch = !walk_switch;
			}
			
			if (walk_switch)
			{
				animations[1].SetActive(true);
			}
			else
			{
				animations[2].SetActive(true);
			}
		}
		else if (animation_name == "leap")
		{
			animations[3].SetActive(true);
		}
		else if (animation_name == "death"){
			animations[4].SetActive(true);
			animations[4].transform.parent = null;
		}
		else
		{
			animations[0].SetActive(true);
		}
	}

}
