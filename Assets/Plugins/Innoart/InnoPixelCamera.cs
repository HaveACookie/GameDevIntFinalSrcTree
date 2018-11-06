using UnityEngine;
using System.Collections;

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
                    `/+/o+::+++++oo:-----//++oo/                                                        
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
    
                  ~ Written by your local Socialist Code Witch Inno UwU ~
*/

[ExecuteInEditMode]
[RequireComponent(typeof(UnityEngine.Camera))]
[AddComponentMenu("Inno/PixelCamera")]
public class InnoPixelCamera : MonoBehaviour {
	public int referenceHeight = 270;
	public int pixelsPerUnit = 32;

	private int renderWidth;
	private int renderHeight;
	private int actualWidth;
	private int actualHeight;
	
	private Camera cam;

	void Awake () {
		cam = GetComponent<Camera>();
	}

	void Update() {
		/*
			Orthographic size is half of reference resolution since it is measured
			from center to the top of the screen.
		*/
		
		renderHeight = referenceHeight;
		cam.orthographicSize = (renderHeight / 2) / (float)pixelsPerUnit;
		
		int scale = Screen.height / renderHeight;
		
		// Height is snapped to the closest whole multiple of reference height.
		actualHeight = (int)(renderHeight * scale);
		
		/*
			Width isn't snapped like height is and will fill the entire width of 
			the monitor using the scale determined by the height.
		*/
		renderWidth = (int)(Screen.width / scale);			
		actualWidth = (int)(renderWidth * scale);

		Rect rect = cam.rect;
		
		rect.width = (float)actualWidth / Screen.width;
		rect.height = (float)actualHeight / Screen.height;
		
		rect.x = (1 - rect.width) / 2;
		rect.y = (1 - rect.height) / 2;

		cam.rect = rect;
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		RenderTexture buffer = RenderTexture.GetTemporary(renderWidth, renderHeight, -1);
		
		buffer.filterMode = FilterMode.Point;
		source.filterMode = FilterMode.Point;
		
		Graphics.Blit(source, buffer);
		Graphics.Blit(buffer, destination);
		
		RenderTexture.ReleaseTemporary(buffer);
	}
}