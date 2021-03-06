﻿using System;
using System.Collections.Generic;
using DataStructures;

namespace CrackingTheCodingInterview
{
	public class TreesAndGraphs
	{
		#region Is Tree Balanced
		//Check if a binary tree is balanced
		//Practice this one again!
		public static void IsTreeBalanced()
		{
			var root = new TreeNode<int>(3);
			root.AddLeft(2);
			root.left.AddRight(4);
			root.left.right.AddLeft(1);

			root.AddRight(6);

			Console.WriteLine(IsBalanced(root).ToString());
		}

		private static bool IsBalanced(TreeNode<int> root)
		{
			if (root == null)
			{
				return true; //base case!!
			}

			int leftHeight = GetHeight(root.left);
			int rightHeight = GetHeight(root.right);

			if (Math.Abs(leftHeight - rightHeight) > 1)
			{
				return false;
			}
			else {
				return IsBalanced(root.right) && IsBalanced(root.left);
			}
		}

		private static int GetHeight(TreeNode<int> root)
		{
			if (root == null) { return 0; }
			else {
				return Math.Max(GetHeight(root.left), GetHeight(root.right)) + 1;
			}
		}
		#endregion

		#region Shortest Path / Dijkstra's
		/// <summary>
		/// Dijkstra's Algorithm
		/// </summary>
		private static void Q4_2()
		{
			HackerRankTraversals.ShortestPath();
		}
		#endregion

		#region Is Binary Search Tree
		//https://www.hackerrank.com/challenges/is-binary-search-tree
		//Check if a binary tree is a binary search tree
		public static void IsBinarySearchTree()
		{
			var root = new TreeNode<int>(5);
			root.AddLeft(2);
			root.left.AddRight(4);
			root.left.AddLeft(1);

			root.AddRight(6);

			Console.WriteLine(checkBST(root).ToString());
		}

		//just do inorder traversal
		private static int last = -1;
		private static bool checkBST(TreeNode<int> root)
		{
			if (root == null) { return true; }

			if (!checkBST(root.left)) { return false; } //move left

			if (root.data <= last) { return false; } //check current node
			else {
				last = root.data;
			}

			if (!checkBST(root.right)) { return false; } //move right

			return true;
		}
		#endregion

		#region Find Max Element In Binary Tree
		//find the max element in a tree
		//not a binary search tree
		//page 129 in algo book
		public static void FindMaxTreeElement()
		{
			Console.WriteLine("Find Max Element in Binary Tree");
			var root = new TreeNode<int>(5);
			root.AddLeft(2);
			root.left.AddRight(6);
			root.left.AddLeft(1);

			root.AddRight(4);

			Console.WriteLine("Max (Recur) is : {0}", FindTreeMax(root));
			Console.WriteLine("Max (Iter) is : {0}", FindTreeMaxIter(root));
		}

		private static int FindTreeMax(TreeNode<int> root)
		{
			int max = int.MinValue;
			if (root != null)
			{
				max = Math.Max(max, root.data);
				max = Math.Max(max, FindTreeMax(root.left));
				max = Math.Max(max, FindTreeMax(root.right));
			}
			return max;
		}

		//do it iteratively without recursion
		private static int FindTreeMaxIter(TreeNode<int> root)
		{
			int max = int.MinValue;
			var q = new Queue<TreeNode<int>>();
			q.Enqueue(root);
			while (q.Count > 0)
			{
				var node = q.Dequeue();
				max = Math.Max(max, node.data);
				if (node.left != null) { q.Enqueue(node.left); }
				if (node.right != null) { q.Enqueue(node.right); }
			}

			return max;
		}
		#endregion

		#region Find Max Element In Binary Search Tree
		//find the max element in a binary search tree
		//page 161 in algo book
		public static void FindMaxBSTElement()
		{
			Console.WriteLine("Find Max Element in Binary Search Tree");
			var root = new TreeNode<int>(5);
			root.AddLeft(2);
			root.left.AddRight(4);
			root.left.AddLeft(1);

			root.AddRight(6);
			root.right.AddRight(8);

			Console.WriteLine("Max is (recur): {0}", FindBSTMax(root));
			Console.WriteLine("Max is (iter): {0}", FindBSTMaxIter(root));
		}

		private static int FindBSTMax(TreeNode<int> root)
		{
			int max = int.MinValue;
			if (root != null)
			{
				max = root.data;
				max = Math.Max(max, FindBSTMax(root.right)); //always move right
			}
			return max;
		}

		//without recursion
		private static int FindBSTMaxIter(TreeNode<int> root)
		{
			int max = int.MinValue;
			if (root != null)
			{
				var s = new Stack<int>();//use stack as most recent will always be largest!
				s.Push(root.data);
				while (root.right != null)
				{
					s.Push(root.right.data);
					root = root.right; //iterate down, always move right
				}
				max = Math.Max(max, s.Pop());
			}

			return max;
		}
		#endregion

		#region Find Size of tree
		//Problem 6, page 131
		public static void TreeSize()
		{
			Console.WriteLine("Get Tree Size");
			var root = new TreeNode<int>(5);
			root.AddLeft(2);
			root.left.AddRight(4);
			root.left.AddLeft(1);

			root.AddRight(6);
			root.right.AddRight(8);

			Console.WriteLine("Tree Size (recur): {0}", TreeSize(root));
		}

		//count number of nodes recursively
		private static int TreeSize(TreeNode<int> root)
		{
			if (root == null)
			{
				return 0;
			}
			return TreeSize(root.left) + TreeSize(root.right) + 1;
		}
		#endregion

		#region Get Tree Height
		//Problem 10, page 132
		public static void GetTreeHeight()
		{
			Console.WriteLine("Get Tree Height");
			var root = new TreeNode<int>(5);
			root.AddLeft(2);
			root.AddRight(6);
			root.right.AddRight(8);
			root.right.right.AddRight(9); //height is 3
			root.right.right.right.AddRight(10); //height is 4

			Console.WriteLine("Tree Height (recur): {0}", TreeHeight(root));
		}

		private static int TreeHeight(TreeNode<int> root)
		{
			if (root == null)
			{
				return -1;
			}

			int left = TreeHeight(root.left);
			int right = TreeHeight(root.right);

			return Math.Max(left, right) + 1; //return longer subtree
		}
		#endregion

		#region LCA in BST
		//https://www.hackerrank.com/challenges/binary-search-tree-lowest-common-ancestor
		public static void LeastCommonAncestor()
		{
			Console.WriteLine("Least Common Ancestor (in a BST)");
			var root = new TreeNode<int>(5);
			root.AddLeft(2);
			root.left.AddRight(4);
			root.left.AddLeft(1);

			root.AddRight(6);
			root.right.AddRight(8);

			Console.WriteLine("LCA: {0}", LCA(root, 1, 8));
		}

		/// <summary>
		/// LCA of A and B in a BST will be the node with a value between A and B
		/// </summary>
		private static int LCA(TreeNode<int> root, int a, int b)
		{
			if (a <= root.data && root.data <= b)
			{
				return root.data;
			}
			else if (a > root.data)
			{
				return LCA(root.right, a, b);
			}
			else {
				return LCA(root.left, a, b);
			}
		}
		#endregion

		#region Subtree of Large Tree
		private static void Q4_8()
		{
			//T1 is a tree with millions of nodes
			//T2 is a tree with hundreds of nodes
			//decide if T2 is a subtree of T1

			//T2 is a subtree of T1 if there is a node in T1 such that the subtree 
			//of n is identical to T2
			//Naive algorithm is to look at each node of T1 then compare to root of T2
			//and go through each node and see if equal

			//start at root of T1, then search for T2
			//return Search(t1.left, t2) || Search(t1.right, t2) 
			//now compare each node of that subtree to T2
		}
		#endregion

		#region Sum in a Tree
		private static void Q4_9()
		{
			//print all paths in a tree that sum to a given value
			//where a path can start/end anywhere in the tree
		}

		private static List<int> TreeSum(List<int> list, TreeNode<int> node, int runSum, int sum){
			if (node == null)
			{
				return list;
			}
			if (runSum + node.data == sum)
			{
				list.Add(node.data);
			}
			else {
				runSum += node.data;
				TreeSum(list, node.left, runSum, sum);
				TreeSum(list, node.right, runSum, sum);
			}
			return list;
		}
		#endregion

		#region Build Order
		/// <summary>
		/// Exercise 4.7 in 6th Edition
		/// A topological graph sort
		/// </summary>
		public static void BuildOrder()
		{
			Console.WriteLine("Given a list of projects and dependencies find a build order");
			string[] projects = { "a", "b", "c", "d", "e", "f" };
			string[] dependencies = { "a d", "f b", "b d", "f a", "d c" };

			Graph<string> graph = CreateGraph(projects);
			int[,] adj = CreateAdjMatrix(dependencies, projects);
			Queue<int> roots = FindRoots(graph, adj);
			Console.WriteLine(ProjectOrder(roots, adj, graph));
		}

		private static Graph<string> CreateGraph(string[] projects)
		{
			var graph = new Graph<string>();
			for (int i = 0; i < projects.Length; i++)
			{
				graph.AddNode(i, projects[i]);
			}
			return graph;
		}

		private static int[,] CreateAdjMatrix(string[] dependencies, string[] projects)
		{
			var adj = new int[projects.Length, projects.Length];
			var lookup = new Dictionary<string, int>(); //to get an id given the name
			for (int i = 0; i < projects.Length; i++)
			{
				lookup.Add(projects[i], i);
			}

			foreach (var d in dependencies)
			{
				string[] line = d.Split(' ');
				int start = lookup[line[0]];
				int end = lookup[line[1]];
				adj[start, end] = 1;
			}

			return adj;
		}

		private static Queue<int> FindRoots(Graph<string> graph, int[,] adj)
		{
			var q = new Queue<int>();
			for (int i = 0; i < adj.GetLength(1); i++)
			{
				bool incomingEdges = true;
				for (int j = 0; j < adj.GetLength(0); j++)
				{
					if (adj[j, i] == 1)
					{
						incomingEdges = false;
						break; // go to next column
					}
				}
				if (incomingEdges)
				{
					q.Enqueue(i); //node has no incoming edges, add it as a starting point
					graph.nodes[i].visited = true;
				}
			}
			return q;
		}

		private static string ProjectOrder(Queue<int> q, int[,] adj, Graph<string> graph)
		{
			if (q.Count == 0)
			{
				return "No projects without dependencies, no possible order";
			}

			var order = new List<string>();
			while (q.Count > 0)
			{
				int project = q.Dequeue();
				order.Add(graph.nodes[project].data);
				for (int i = 0; i < adj.GetLength(0); i++)
				{
					if (adj[project, i] == 1) //edge exists
					{
						if (!graph.nodes[i].visited) //node unvisited
						{
							graph.nodes[i].visited = true;
							q.Enqueue(i);
						}
					}
				}
			}

			if (order.Count != graph.nodes.Count)
			{
				return "No possible order";
			}

			return string.Join(" ", order.ToArray());
		}
		#endregion

		#region Random Node
		/// <summary>
		/// Exercise 4.11 in 6th edition
		/// </summary>
		public static void RandomNode()
		{
			Console.WriteLine("Create a binary tree with a method getRandomNode()");
			var tree = new RandomTree<int>(0);

			for (int i = 1; i < 10; i++)
			{
				tree.Insert(i);
			}

			for (int i = 0; i < 5; i++)
			{
				Console.WriteLine("Random node: " + tree.RandomNode());
			}
		}
		#endregion

		#region List of Depths

		/// <summary>
		/// TODO: Exercise 4.3 in 6th edition
		/// </summary>
		public static void DepthLists()
		{
		}
		#endregion
	}
}
