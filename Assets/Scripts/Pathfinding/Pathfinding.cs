using UnityEngine;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
	//Components
	private GridBehaviour grid;
	
	//Initialize Singleton Grid
	void Awake()
	{
		grid = GridBehaviour.instance;
	}
	
	//Path Finding Methods
	public List<Node> findPath(Vector3 start, Vector3 target)
	{
		Node start_node = grid.nodeFromPositionClamp(start);
		Node target_node = grid.nodeFromPositionClamp(target);
		
		Heap<Node> open_set = new Heap<Node>(grid.grid_maxsize);
		HashSet<Node> closed_set = new HashSet<Node>();
		open_set.Add(start_node);

		if (start_node == target_node)
		{
			List<Node> single_list = new List<Node>();
			single_list.Add(target_node);
			return single_list;
		}
		
		while (open_set.Count > 0) 
		{
			Node current_node = open_set.RemoveFirst();
			closed_set.Add(current_node);

			if (current_node == target_node) 
			{
				return retracePath(start_node, target_node);
			}

			foreach (Node neighbour in grid.getNeighbours(current_node)) {
				if (!neighbour.empty || closed_set.Contains(neighbour)) {
					continue;
				}

				int movement_cost = current_node.g_cost + getDistance(current_node, neighbour);
				if (movement_cost < neighbour.g_cost || !open_set.Contains(neighbour)) {
					neighbour.g_cost = movement_cost;
					neighbour.h_cost = getDistance(neighbour, target_node);
					neighbour.parent = current_node;

					if (!open_set.Contains(neighbour))
						open_set.Add(neighbour);
					else {
						open_set.UpdateItem(neighbour);
					}
				}
			}
		}
		
		return null;
	}
	
	public Vector2[] findPathArray(Vector3 start, Vector3 target)
	{
		List<Node> nodes = findPath(start, target);
		if (nodes == null || nodes.Count == 0)
		{
			return null;
		}
		
		Vector2[] array = new Vector2[nodes.Count];
		for (int i = 0; i < nodes.Count; i++)
		{
			array[i] = new Vector2(nodes[i].position.x, nodes[i].position.z);
		}

		return array;
	}
	
	//Misc Methods
	private List<Node> retracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		
		return cleanPath(path);
	}
	
	private List<Node> cleanPath(List<Node> path)
	{
		List<Node> waypoints = new List<Node>();
		int pointA = 0;
		int pointB = 2;
		waypoints.Add(path[pointA]);
		while (pointB < path.Count){
			if (!checkPoints(path[pointA].position, path[pointB].position, grid.node_dia * 0.5f)){
				pointB++;
			}
			else {
				waypoints.Add(path[pointB - 1]);
				pointA = pointB;
				pointB = pointA + 2;
			}
		}
		waypoints.Add(path[path.Count - 1]);
		return waypoints;
	}
	
	public bool checkPoints(Vector3 point_a, Vector3 point_b)
	{
		if (Physics.Linecast(point_a, point_b, grid.solids_layer))
		{
			return true;
		}
		return false;
	}
	
	public bool checkPoints(Vector3 point_a, Vector3 point_b, float thickness)
	{
		float distance = Vector3.Distance(point_a, point_b);
		int iterations = Mathf.RoundToInt(distance / thickness);
		
		for (int i = 0; i < iterations; i++)
		{
			Vector3 origin = Vector3.Lerp(point_a, point_b, i / (iterations * 1f));
			Collider[] hits = Physics.OverlapSphere(origin, thickness, grid.solids_layer);
			if (hits.Length > 0)
			{
				return true;
			}
			if (!grid.nodeFromPosition(origin).empty)
			{
				return true;
			}
		}
		return false;
	}
	
	private int getDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.grid_x - nodeB.grid_x);
		int dstY = Mathf.Abs(nodeA.grid_y - nodeB.grid_y);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}