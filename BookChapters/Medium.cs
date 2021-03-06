﻿using System;
using System.Text;
using System.Collections.Generic;
using DataStructures;
using TextMethods;

namespace CrackingTheCodingInterview
{
	//Chapter 17 in CTCI
	public class Medium
	{
		#region Swap Numbers
		public static void SwapNumbers()
		{
			Q17_1(20, 9);
		}

		private static void Q17_1(int a, int b)
		{
			a = b - a;
			b = b - a;
			a = a + b;
			Console.WriteLine("a: " + a);
			Console.WriteLine("b: " + b);
		}
		#endregion

		#region Tic Tac Toe
		//see if someone has won tic-tac-toe
		public static void TicTacToe(){
			Console.WriteLine(Q17_2());
		}

		private static bool Q17_2()
		{
			Console.WriteLine("Tic-tac-toe");

			//should have made a new class/struct here
			//constructor for size, x, o, empty
			char[,] board = { {'x', 'o', 'o'},
							  {'o', '_', '_'},
							  {'x', 'o', 'x'}};
			int columns = board.GetLength(1);
			int rows = board.GetLength(0);

			//check columns
			for (int i = 0; i < columns; i++)
			{
				var column = new char[columns];
				for (int j = 0; j < rows; j++)
				{
					column[j] = board[j, i];
				}
				if (IsWinner(column)) { return true; }
			}

			//check rows
			for (int j = 0; j < rows; j++)
			{
				var row = new char[rows];
				for (int i = 0; i < columns; i++)
				{
					row[i] = board[j, i];
				}
				if (IsWinner(row)) { return true; }
			}

			//check diagonals
			var diagForward = new char[columns];
			var diagBack = new char[columns];
			for (int i = 0; i < columns; i++)
			{
				diagForward[i] = board[i, i];
				diagBack[i] = board[i, columns - 1 - i];
			}
			if (IsWinner(diagForward)) { return true; }
			if (IsWinner(diagBack)) { return true; }

			return false;
		}

		private static bool IsWinner(char[] column)
		{
			char target = column[0];
			if (!target.Equals('x') || !target.Equals('o'))
			{
				return false;
			}	
			for (int i = 1; i < column.Length; i++)
			{
				if (!column[i].Equals(target)) { return false;}
			}
			return true;
		}
		#endregion

		#region Verify Factorial
		public static void VerifyFactorial()
		{
			Console.WriteLine("Number of trailing zeros in n!");
			int n = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Expected answer: " + (n / 5));
			Console.WriteLine("Actual answer: " + Q17_3(n));
		}

		//brute force
		private static int Q17_3(int n)
		{
			long factorial = 1;
			for (int i = 1; i <= n; i++)
			{
				factorial *= i;
			}
			int zeros = 0;
			string s = factorial.ToString();
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (s[i].Equals('0')) { zeros++; }
				else { break; }
			}
			return zeros;
		}
		#endregion

		#region Get Max
		public static void GetMax()
		{
			//Don't use if-else statements or comparison operators
			Console.WriteLine("Max of 2 numbers");
			int a = Convert.ToInt32(Console.ReadLine());
			int b = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine(Q17_4(a, b));
		}

		private static int Q17_4(int a, int b)
		{
			int diff = a - b;
			int sign = ((diff >> 31) & 1) ^ 1; //1 for positive, 0 for negative
			int flip = sign ^ 1;

			return a * sign + b * flip;

		}
		#endregion

		#region Sorted Sub Array
		public static void SortedSubArray()
		{
			
			Console.WriteLine("Sorted sub array");
			//int[] arr = { 1, 2, 4, 7, 10, 11, 7, 12, 6, 7, 16, 18, 19 };
			//int[] arr = { 1, 2, 3, 4};
			int[] arr = { 4, 3, 2, 1};
			Q17_6(arr);
		}

		//edit insertion sort
		//save lowest and highest index that an element
		//was inserted into
		static void Q17_6(int[] arr)
		{
			int min = Int32.MaxValue;
			int max = Int32.MinValue;
			for (int i = 0; i < arr.Length - 1; i++)
			{
				int j = i;
				//swap to left while smaller than left
				while (j >= 0 && arr[j] > arr[j + 1])
				{
					//swap left with right
					int temp = arr[j + 1];
					arr[j + 1] = arr[j];
					arr[j] = temp;

					max = i + 1; //save where swapping started
					min = Math.Min(min, j); //save min index
					j--;
				}
			}
			Console.WriteLine("{0} {1}", min, max);
		}
		#endregion

		#region Largest Subarray Sum
		public static void LargestSubarraySum()
		{
			Console.WriteLine("Q17.7: Largest Contiguous Subarray Sum");
			int[] arr = {2, -8, 3, -2, 4, -10};
			//int[] arr = { -3, -1, -4 };
			Console.WriteLine(Kadanes(arr));
		}

		private static int Kadanes(int[] arr)
		{
			int local = arr[0];
			int global = arr[0];
			for (int i = 1; i < arr.Length; i++)
			{
				if (local < 0)
				{
					local = arr[i];
				}
				else {
					local += arr[i];
				}
				global = Math.Max(global, local);
			}

			return global;
		}
		#endregion

		private static void Q17_9()
		{
			Console.WriteLine("Word Frequencies in a Book");
			//given a word, find the frequency it shows up in a book
			//create hash table, then increment frequencies
		}

		private static void Q17_11()
		{
			Console.WriteLine("TODO: Random Numbers");
		}

		#region Search Pair Sums
		public static void SearchPairSums()
		{
			Console.WriteLine("Search for pairs that sum to a given number");
			//find all pairs in an array that sum to a given number
			int[] arr = { 2, 5, 3, 4, 10, -3, -13, 20 };
			HashPairSums(arr, 7);
			Q17_12(arr, 7);
		}

		private static void Q17_12(int[] arr, int n)
		{
			Array.Sort(arr); //O(nlogn)
			foreach (var i in arr) //O(nlogn)
			{
				int compliment = n - i;
				if (BinarySearch(arr, compliment, 0,  arr.Length - 1)){
					Console.WriteLine("{0} {1}", i, compliment);
				}
			}
		}

		private static bool BinarySearch(int[] arr, int n, int low, int high)
		{
			if (low > high)
			{
				return false;
			}

			int mid = (low + high) / 2;
			if (arr[mid] == n)
			{
				return true;
			}
			else if (arr[mid] > n)
			{
				return BinarySearch(arr, n, low, mid - 1);
			}
			else {
				return BinarySearch(arr, n, mid + 1, high);
			}
		}

		//simpler solution, but O(n) is slower than O(nlogn)
		private static void HashPairSums(int[] arr, int n)
		{
			var pairs = new Dictionary<int, int>();
			foreach (var a in arr) //O(n)
			{
				int compliment = n - a;
				if (!pairs.ContainsKey(compliment))
				{
					pairs.Add(a, compliment);
				}
				else {
					Console.WriteLine("{0} {1}", a, compliment);
				}
			}
		}
		#endregion

		#region Convert BST to a doubly linked list
		//keep items in order, and do in place
		private static Queue<BiNode> q = new Queue<BiNode>();
		public static void BSTtoDLL()
		{
			Console.WriteLine("Convert BST to a doubly linked list.");
			var root = new BiNode(4);
			root.B1 = new BiNode(2);
			root.B1.B1 = new BiNode(1);
			root.B1.B2 = new BiNode(3);

			root.B2 = new BiNode(6);
			root.B2.B1 = new BiNode(5);

			ToQueue(root);
			BiNode head = ToList(q);

			BiNode iter = head;
			BiNode tail = head;
			Console.Write("Forwards: ");
			while (iter != null)
			{
				Console.Write(iter.Data + " ");
				tail = iter;
				iter = iter.B2;
			}

			Console.WriteLine();
			Console.Write("Reverse: ");
			iter = tail;
			while (iter != null)
			{
				Console.Write(iter.Data + " ");
				iter = iter.B1;
			}
		}

		private static void ToQueue(BiNode root)
		{
			if (root == null)
			{
				return;
			}
				
			ToQueue(root.B1);
			q.Enqueue(root);
			ToQueue(root.B2);
		}

		private static BiNode ToList(Queue<BiNode> list)
		{
			BiNode head = q.Dequeue();

			BiNode last = null;
			BiNode iter = head;
			while (q.Count > 0)
			{
				iter.B1 = last;
				iter.B2 = q.Dequeue();

				last = iter;
				iter = iter.B2;
			}
			return head;
		}
		#endregion

		#region Intersecting Lines
		/// <summary>
		/// Question 16.3 in 6th edition
		/// </summary>
		private static void IntersectingLines()
		{
			Console.WriteLine("given 2 line segments, find their intersection, if any");
			int startx1 = TextGui.IntegerPrompt("Enter line 1, start x");
			int starty1 = TextGui.IntegerPrompt("Enter line 1, start y");
			int endx1 = TextGui.IntegerPrompt("Enter line 1, end x");
			int endy1 = TextGui.IntegerPrompt("Enter line 1, end y");

			int startx2 = TextGui.IntegerPrompt("Enter line 2, start x");
			int starty2 = TextGui.IntegerPrompt("Enter line 2, start y");
			int endx2 = TextGui.IntegerPrompt("Enter line 2, end x");
			int endy2 = TextGui.IntegerPrompt("Enter line 2, end y");

			var line1 = new Line(new Point(startx1, starty1), new Point(endx1, endy1));
			var line2 = new Line(new Point(startx2, starty2), new Point(endx2, endy2));

			var intersection = Line.Intersection(line1, line2);
			Console.WriteLine("{0}, {1}", intersection.x, intersection.y);
		}


		#endregion

		#region Smallest Difference
		public static void SmallestDifference()
		{
			Console.WriteLine("Find the smallest non-neg difference between one value in each array");
			int[] A = { 1, 3, 15, 11, 2 };
			int[] B = { 23, 127, 235, 19, 8 };

			//O(AlogA + BlogB);
			Array.Sort(A);
			Array.Sort(B);
			int a = 0, b = 0;
			int min = int.MaxValue;
			while (a < A.Length && b < B.Length)
			{
				min = Math.Min(min, Math.Abs(A[a] - B[b]));
				if (A[a] < B[b])
				{
					a++;
				}
				else{
					b++;
				}
			}

			Console.WriteLine(min);
		}
		#endregion

		#region Operations
		public static void Operations()
		{
			Console.WriteLine("Implement *, /, - only using addition");
			int a = TextGui.IntegerPrompt("Enter integer 1: ");
			int b = TextGui.IntegerPrompt("Enter integer 2: ");
			Console.WriteLine("{0} x {1} = {2}", a, b, Multiply(a, b));
			Console.WriteLine("{0} / {1} = {2}", a, b, Divide(a, b));
			Console.WriteLine("{0} - {1} = {2}", a, b, Subtract(a, b));
		}

		/// <summary>
		/// Multiply a by b only using +.
		/// </summary>
		private static int Multiply(int a, int b)
		{
			int product = 0;
			if (a == 0 || b == 0)
			{
				return 0;
			}
			int larger = Math.Max(Math.Abs(a), Math.Abs(b)); 
			int smaller = Math.Min(Math.Abs(a), Math.Abs(b));

			for (int i = 1; i <= smaller; i++)
			{
				product += larger;
			}

			if ((a < 0 && b < 0) ||
			   (a > 0 && b > 0))
			{
				return product;
			}
			else {
				return Negate(product);
			}
		}

		/// <summary>
		/// Integer divide a by b only using +
		/// </summary>
		private static int Divide(int a, int b)
		{
			if (b == 0)
			{
				return int.MinValue;
			}
			if (b == 1)
			{
				return a;
			}

			int product = 0;
			int i = 0;

			int absA = Math.Abs(a);
			int absB = Math.Abs(b);
			while (product + absB <= absA)
			{
				product += absB;
				i++;
			}

			if ((a < 0 && b < 0) ||
			   (a > 0 && b > 0))
			{
				return i;
			}
			else {
				return Negate(i);
			}
		}

		/// <summary>
		/// Subtract b from a only using +
		/// </summary>
		private static int Subtract(int a, int b)
		{
			return a + Negate(b);
		}

		private static int Negate(int a)
		{
			int neg = 0;
			int newSign = (a > 0) ? -1 : 1;
			while (a != 0)
			{
				neg += newSign;
				a += newSign;
			}
			return neg;
		}
		#endregion

		#region Diving Board
		static int maxK, shorter, longer;
		static HashSet<int> boards = new HashSet<int>();
		public static void DivingBoard()
		{
			Console.WriteLine("Given short and long planks, you must use k plans.");
			Console.WriteLine("Find all possible lengths for the diving board.");
			maxK = TextGui.IntegerPrompt("Enter k: ");
			shorter = TextGui.IntegerPrompt("Enter shorter: ");
			longer = TextGui.IntegerPrompt("Enter longer: ");
			BoardPerms(0, 0);
			foreach (var b in boards)
			{
				Console.WriteLine(b);
			}
		}

		static void BoardPerms(int length, int k)
		{
			if (k == maxK)
			{
				boards.Add(length);
			}
			else {
				BoardPerms(length + shorter, k + 1);
				BoardPerms(length + longer, k + 1);
			}
		}
		#endregion

		#region Master Mind
		/// <summary>
		/// Exericse 16.15 in 6th edition
		/// </summary>
		public static void MasterMind()
		{
			Console.WriteLine("Given 4 slots and 4 colors, guess the combination");
			Console.WriteLine("Print number of hits and psuedo-hits");

			string solution = "RGBY";
			string guess = "GGRR";
			int[] solutionCounts = new int[4];
			int[] guessCounts = new int[4];

			int hits = 0, pseudoHit = 0;
			for (int i = 0; i < 4; i++)
			{
				if (solution[i] == guess[i])
				{
					hits++;
				}
				else {
					var solutionColor = Mastermind.CharToColor(solution[i]);
					var guessColor = Mastermind.CharToColor(guess[i]);
					solutionCounts[(int)solutionColor]++;
					guessCounts[(int)guessColor]++;
				}
			}

			for (int i = 0; i < 4; i++)
			{
				if (guessCounts[i] >= solutionCounts[i])
				{
					pseudoHit += solutionCounts[i];
				}
			}

			Console.WriteLine("Solution: {0}, Guess: {1}", solution, guess);
			Console.WriteLine("Hits: {0}, Psuedo-Hits: {1}", hits, pseudoHit);
		}
		#endregion

		#region Pond Sizes
		/// <summary>
		/// Exercise 16.19 in 6th Edition
		/// </summary>
		public static void PondSizes()
		{
			Console.WriteLine("In a matrix where 0 is water, find the sizes of all ponds");
			int[,] land = { { 0, 2, 1, 0 },
							{ 0, 1, 0, 1 },
							{ 1, 2, 0, 1 },
							{ 0, 2, 0, 1 } };

			List<int> ponds = new List<int>();
			for (int row = 0; row < land.GetLength(0); row++)
			{
				for (int col = 0; col < land.GetLength(1); col++)
				{
					//only start DFS if water
					if (land[row, col] == 0)
					{
						int pondSize = DFS_Pond(land, row, col);
						ponds.Add(pondSize);
					}
				}
			}

			Console.WriteLine("Pond sizes: {0}", string.Join(" ", ponds.ToArray()));
		}

		static int DFS_Pond(int[,] land, int row, int col)
		{
			int size = 0;

			//check bounds and if land
			if (row < 0 || row >= land.GetLength(0) ||
			   col < 0 || col >= land.GetLength(1) ||
			   land[row, col] != 0)
			{
				return size;
			}

			size++; //increment count
			land[row, col] = -1; //mark as visited

			//size += DFS_Pond(land, row + 1, col); //south
			//size += DFS_Pond(land, row - 1, col); //north
			//size += DFS_Pond(land, row, col + 1); //east
			//size += DFS_Pond(land, row, col - 1); //west

			//size += DFS_Pond(land, row + 1, col - 1); //southwest
			//size += DFS_Pond(land, row + 1, col + 1); //southeast
			//size += DFS_Pond(land, row - 1, col - 1); //northwest
			//size += DFS_Pond(land, row - 1, col + 1); //northeast

			//goes through al 8 combinations.  
			//will visit self again, but will ok as it will quit early anyway
			for (int dr = -1; dr <= 1; dr++)
			{
				for (int dc = -1; dc <= 1; dc++){
					size += DFS_Pond(land, row + dr, col + dc);
				}
			}

			return size;
		}
		#endregion

		#region T9 Phone
		public static void T9Phone()
		{
			Console.WriteLine("given a string of digits, return all possible words");
			Console.WriteLine("possible from a telephone");
			Console.Write("Enter a number: ");
			string number = Console.ReadLine(); 
			//keep as string so that you can iterate through each char

			T9Words(new StringBuilder(), number);
			foreach (var word in words)
			{
				Console.WriteLine(word);
			}
		}
		static HashSet<string> words = new HashSet<string>();
		static Keypad keypad = new Keypad();

		static void T9Words(StringBuilder perm, string number)
		{
			//done recursion
			if (perm.Length == number.Length)
			{
				words.Add(perm.ToString());
				return;
			}

			int level = perm.Length; //recursion level
			foreach (var letter in keypad.GetLetters(number[level]))
			{
				var nextPerm = new StringBuilder(perm.ToString());
				nextPerm.Append(letter);
				T9Words(nextPerm, number);
			}
		}
		#endregion

		#region Sum Swap 
		public static void SumSwap()
		{
			Console.WriteLine("given 2 arrays, find a number in each, that when swapped");
			Console.WriteLine("both arrays sum to the same number");

			int[] A = { 4, 1, 2, 1, 1, 2 };
			int[] B = { 3, 6, 3, 3 };

			int sumA = 0;
			HashSet<int> numsA = SumAndSet(A, ref sumA);
			int sumB = 0;
			HashSet<int> numsB = SumAndSet(B, ref sumB);

			if (sumA == sumB)
			{
				Console.WriteLine("Nothing to swap");
			}
			else {
				int target = (sumA + sumB) / 2;
				int offsetA = target - sumA;
				//int offsetB = target - sumB;
				foreach (var a in numsA)
				{
					int BtoA = a + offsetA;
					//int AtoB = BtoA + offsetB;
					if (numsB.Contains(BtoA))// && AtoB == a)
					{
						Console.WriteLine("Swap {0} and {1}", BtoA, a);
					}
				}
			}
		}

		//returns both the sum of all elements and a hash set of all unique elements
		static HashSet<int> SumAndSet(int[] arr, ref int sum)
		{
			var nums = new HashSet<int>();
			foreach (int a in arr)
			{
				sum += a;
				nums.Add(a);
			}
			return nums;
		}
		#endregion
	}
}
