using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour {

	public static string itemName(int index)
	{
		switch (index)
		{
			case 0:
				return "";
			case 1:
				return "Combat Knife";
			case 2:
				return "Beretta M92FS";
			case 3:
				return "Beretta M92FS Custom";
			case 4:
				return "Remington M870";
			case 5:
				return "Bazooka";
			case 6:
				return "Colt Python";
			case 7:
				return "Flamethrower";
			case 8:
				return "Rocket Launcher";
			case 9:
				return "Clip";
			case 10:
				return "Shells";
			case 11:
				return "Explosive Rounds";
			case 12:
				return "Acid Rounds";
			case 13:
				return "Flame Rounds";
			case 14:
				return "Magnum Rounds";
			case 15:
				return "Ink Ribbon";
			case 16:
				return "Desk Key";
			case 17:
				return "Sword Key";
			case 18:
				return "Herbicide";
			case 19:
				return "Armor Key";
			case 20:
				return "Broken Shotgun";
			case 21:
				return "Blue Jewel";
			case 22:
				return "Lighter";
			case 23:
				return "Music Notes";
			case 24:
				return "Emblem";
			case 25:
				return "Gold Emblem";
			case 26:
				return "Shield Key";
			case 27:
				return "Wind Crest";
			case 28:
				return "Star Crest";
			case 29:
				return "Sun Crest";
			case 30:
				return "Moon Crest";
			case 31:
				return "Moon Crest (left piece)";
			case 32:
				return "Moon Crest (right piece)";
			case 33:
				return "Square Crank";
			case 34:
				return "Blank Book";
			case 35:
				return "002 Key";
			case 36:
				return "Control Room Key";
			case 37:
				return "003 Key";
			case 38:
				return "Empty Bottle";
			case 39:
				return "Water";
			case 40:
				return "UMB No.2";
			case 41:
				return "UMB No.4";
			case 42:
				return "NP-003";
			case 43:
				return "Yellow-6";
			case 44:
				return "UMB No.7";
			case 45:
				return "UMB No.13";
			case 46:
				return "V-JOLT";
			case 47:
				return "Helmet Key";
			case 48:
				return "Red Jewel";
			case 49:
				return "Battery";
			case 50:
				return "Doom Book 1";
			case 51:
				return "Eagle Medal";
			case 52:
				return "Hex Crank";
			case 53:
				return "Doom Book 2";
			case 54:
				return "Wolf Medal";
			case 55:
				return "MO Disk";
			case 56:
				return "Slides";
			case 57:
				return "Power Room Key";
			case 58:
				return "Master Key";
			case 59:
				return "Flare";
			case 60:
				return "Closet Key";
			case 61:
				return "Green Herb";
			case 62:
				return "Red Herb";
			case 63:
				return "Blue Herb";
			case 64:
				return "First Aid Spray";
			case 65:
				return "Serum";
			case 66:
				return "Mixed Herbs (G+G)";
			case 67:
				return "Mixed Herbs (G+R)";
			case 68:
				return "Mixed Herbs (G+B)";
			case 69:
				return "Mixed Herbs (G+G+B)";
			case 70:
				return "Mixed Herbs (G+G+G)";
			case 71:
				return "Mixed Herbs (G+R+B)";
			case 72:
				return "Lock Pick";
			default:
				return "";
		}
	}
	
}
