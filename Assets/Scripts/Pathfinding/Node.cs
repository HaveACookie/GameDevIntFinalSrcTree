using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> 
{
	
	public bool empty { get; private set; }
	public Vector3 position { get; private set; }
	public int grid_x { get; private set; }
	public int grid_y { get; private set; }

	public int g_cost;
	public int h_cost;
	public Node parent;
	int heap_index;
	
	public Node(bool empty, Vector3 position, int grid_x, int grid_y) {
		this.empty = empty;
		this.position = position;
		this.grid_x = grid_x;
		this.grid_y = grid_y;
	}

	public int fCost {
		get {
			return g_cost + h_cost;
		}
	}

	public int HeapIndex {
		get {
			return heap_index;
		}
		set {
			heap_index = value;
		}
	}

	public int CompareTo(Node nodeToCompare) {
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0) {
			compare = h_cost.CompareTo(nodeToCompare.h_cost);
		}
		return -compare;
	}
}