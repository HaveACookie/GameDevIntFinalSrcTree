using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
                        .-`                                                                            
                        /./                                                                             
                        +.:-                                                                            
                        `:/+/:..--....-..-.`   ``..``                                                   
                            `.-+:.``       `.--`.-.....`                                                  
                            -/`       `    `..//```   `                                                  
                            -+`        ```  `:/-s-.-.                                                     
                        `` `o:.        ..`  -oydso `..                                                    
                        -//-.`       :/:- `.dyshs   `                                                    
                        `+/.`.``  .:osys..`+s:os                                                        
                            .o--.```+dyddo:-.`.:--+/                                                       
                            //--::.`-h:oy- ````...:+                                                       
                            s--//::``-.:/-`.`````.+-                                           
                    `-..:/+:::++/-.......```.--++                      ~ Written by @mermaid_games ~                                  
                    `/+/o+::+++++oo:-----//++oo/                             aka Inno <3       
                        `-::-/+++/+ss/:-.:oysys++/`                                                       
            `      `-::..-:++++:odhyss/-ysoyyso+-                                                       
            `----.://----:/++/+/:dddyssyhyysyssyy+--                                                     
                `//o++oo/-:+oo:-+:-dhhyhyhdysosyhhhhhy                                                     
                    :o:/+:+/-/+:.ssydddhdhdyydhhhhd`                                                     
                    `+//o`+/-:+o+..yhdhhhmhdyhhhys+d`                                                     
                    .y+s-++:/++oo-`/yysyddhhhhhyso+h`                                                     
                    .y+y+o++oo+shho.:oshmh+oso+osoo-                                                      
                    .y+so++oso+ymmyyo+oymdoo++//o+                                                        
                    :s+++osyssmdmhssshdds///o+od                                                         
                    `+o+oyyyyhmmddssyddy/:/+/shd                                                         
                    `+osyyyhhydmdysshyhs/:::yhhd        ....`                                            
            ..```-:ooyhyyhhhhhddysyyshs/+oohhhs   `./ooo++++o-                                          
                .ooss+/ohhhhysshddhyyysyhy++s:yhd/:o+ssoooo++///s:                                         
                `.. `odhysssyddmhhhyshhsyo:syosysooooooo+oyso+os:                                        
                    :shyssssyyddmhdhsyhhds-:+oys++oooooo+++ooo++os:                                       
                -shhysssssyhddmdhyshhhy/++hhs++ooooooosyy+////+os`                                      
                .yhhhyssssyhhsmdhhsshdds-.odyssooooooosydd+::////oo                                      
                `dhhhysssssyyo+mdhysshdds/:ydhhysooosshd/`so//////+s:                                     
                +dhhyssssss//omhyo++syd+.+dhhhhhyyyhddm  .y+//////+o.                                    
                -o:ooyyss+///odo+---/oyoods+osyyhhddddo   /y+//////+s.                                   
                -+--`-os+///+hs++::::+ohds////++shhddm-   `sso++////oy`                                  
                ./--`  ./+oosdo++/:::++hh/////////+syh:.    /sooo+///oo`                                 
                /o:--.`---.oyo///--:oosd///////o++//+++/.`  .oyso+///o+`                                
                `-+++o//:-.so:-::-..://d+//////++++++ssso+//-.-oyo+/osd:                                
                    `...-::osyo:----/yodhyysssoooossssssoo+++++/+ddddddd:`                              
                            ``s/--.:.-hyyyyyyyyssysso++++///////++oyhhhdddo`                             
                            :s//./o+-o         `-/+so+++////////////+oyhdmo.                            
                            :so+:/-`+:o.            .:+oooo+++//////////+smdho/-.`                       
                            `.::-    ..`                 ..-///+syso+++++yhyyhhhys+/:..`                 
                                                                -dmh/:ooydhhyoosyyyyssssoo:-/oos/`       
                                                                hm+   `+ddh+oo+oyyso++++oyhhyhdm.       
                                                                oy:     ydhyyysoshyysssosyhhhhy+`       
                                                                        `odhhhhhhhhyhhhhhhhhh/`         
                                                                            .ydddddy:` .::::::.            
                                                                            `/ohdddo.                     
                                                                            -:ydh                     
                                                                                ```       
    
                  ~ Queer Witches send fascists to the hospital in stitches ~
*/

public class GridBehaviour : MonoBehaviour
{
	//Components
	public static GridBehaviour instance { get; private set; }
	
	//Settings
	[Header("Grid Settings")]
	[SerializeField] private bool show_debug = true;
	[SerializeField] private LayerMask solids_mask;
	[SerializeField] private Vector2 grid_size = new Vector2(20, 20);
	[SerializeField] private float node_radius = 0.25f;
	[Range(0, 3)] 
	[SerializeField] private int node_outline;
	
	//Variables
	private bool draw_debug;
	private Node[,] grid;
	private int grid_width;
	private int grid_hight;
	
	//Initialize Grid
	void Awake()
	{
		//Set Singleton
		instance = this;
		
		//Settings
		draw_debug = true;
		grid_width = Mathf.RoundToInt(grid_size.x / node_dia);
		grid_hight = Mathf.RoundToInt(grid_size.y / node_dia);
		grid = new Node[grid_width, grid_hight];

		createGrid();
		if (node_outline != 0)
		{
			filterGrid();
		}
	}

	//Methods
	private void createGrid()
	{
		Vector3 offset = new Vector3(transform.position.x - (grid_size.x / 2f), transform.position.y, transform.position.z - (grid_size.y / 2f));

		for (int h = 0; h < grid_hight; h++)
		{
			for (int w = 0; w < grid_width; w++)
			{
				Vector3 pos = new Vector3(w * node_dia, 0, h * node_dia) + offset;
				bool empty = !(Physics.CheckBox(pos, Vector3.one * node_radius, new Quaternion(0f, 0f, 0f, 0f), solids_mask));
				grid[w, h] = new Node(empty, pos, w, h);
			}
		}
	}

	private void filterGrid()
	{
		bool[,] empty_grid = new bool[grid_width, grid_hight];
		
		for (int h = 0; h < grid_hight; h++)
		{
			for (int w = 0; w < grid_width; w++)
			{
				if (!grid[w, h].empty)
				{
					for (int x = -node_outline; x <= node_outline; x++)
					{
						for (int y = -node_outline; y <= node_outline; y++)
						{
							int x_index = Mathf.Clamp(w + x, 0, grid_width - 1);
							int y_index = Mathf.Clamp(h + y, 0, grid_hight - 1);
							
							empty_grid[x_index, y_index] = true;
						}
					}
				}
			}
		}
		
		grid = new Node[grid_width, grid_hight];
		Vector3 offset = new Vector3(transform.position.x - (grid_size.x / 2f), transform.position.y, transform.position.z - (grid_size.y / 2f));

		for (int h = 0; h < grid_hight; h++)
		{
			for (int w = 0; w < grid_width; w++)
			{
				Vector3 pos = new Vector3(w * node_dia, 0, h * node_dia) + offset;
				bool empty = !empty_grid[w, h];
				grid[w, h] = new Node(empty, pos, w, h);
			}
		}
	}
	
	public List<Node> getNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int check_x = node.grid_x + x;
				int check_y = node.grid_y + y;

				if (check_x >= 0 && check_x < grid_width && check_y >= 0 && check_y < grid_hight) {
					neighbours.Add(grid[check_x, check_y]);
				}
			}
		}

		return neighbours;
	}
	
	public Node nodeFromPositionClamp(Vector3 position)
	{
		Node temp_node = nodeFromPosition(position);
		float node_distance = Mathf.Infinity;
		
		for (int h = 0; h < grid_hight; h++)
		{
			for (int w = 0; w < grid_width; w++)
			{
				if (grid[w, h].empty)
				{
					float distance = Vector2.Distance(new Vector2(position.x, position.z), new Vector2(grid[w, h].position.x, grid[w, h].position.z));
					if (distance < node_distance)
					{
						temp_node = grid[w, h];
						node_distance = distance;
					}
				}
			}
		}

		return temp_node;
	}
	
	public Node nodeFromPosition(Vector3 position)
	{
		Vector3 world_position = position - transform.position;
		float percent_x = (world_position.x + (grid_size.x / 2)) / grid_size.x;
		float percent_y = (world_position.z + (grid_size.y / 2)) / grid_size.y;
		percent_x = Mathf.Clamp01(percent_x);
		percent_y = Mathf.Clamp01(percent_y);

		int x = Mathf.RoundToInt((grid_width - 1) * percent_x);
		int y = Mathf.RoundToInt((grid_hight - 1) * percent_y);
		return grid[x,y];
	}

	public int grid_maxsize
	{
		get
		{
			return grid_width * grid_hight;
		}
	}

	public float node_dia
	{
		get
		{
			return node_radius * 2;
		}
	}

	public LayerMask solids_layer
	{
		get
		{
			return solids_mask;
		}
	}
	
	//Draw Debug
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(transform.position, new Vector3(grid_size.x, node_dia, grid_size.y));

		if (show_debug && draw_debug)
		{
			for (int w = 0; w < grid_width; w++)
			{
				for (int h = 0; h < grid_hight; h++)
				{
					Node draw_node = grid[w, h];
					
					Gizmos.color = Color.red;
					if (draw_node.empty)
					{
						Gizmos.color = Color.blue;
					}
					
					Gizmos.DrawWireSphere(draw_node.position, node_radius);
				}
			}
		}
	}
}