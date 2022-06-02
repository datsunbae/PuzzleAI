using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PuzzleAI
{
    internal class State
    {
		public List<int> state { get; set; }
		public State parent { get; set; }

		public State cha { get; set; }
		public int h_Cost { get; set; }
		public int g_Cost { get; set; }
		public int f_Cost { get; set; }

		public State() { }
		public State(List<int> state)
		{
			this.state = state;
		}

		public bool CheckStateSame(State a, State b)
		{
			bool checksame = true;
			for (int i = 0; i < b.state.Count; i++)
			{
				if (a.state[i] != b.state[i])
				{
					checksame = false;
				}
			}
			return checksame;
		}

		public bool CheckStateSame_InList(List<State> list, State check)
		{
			bool contains = false;
			foreach (var item in list)
			{
				if (CheckStateSame(item, check))
					contains = true;
			}
			return contains;
		}
		
		public List<List<int>> TaoMang(List<int> arr)
		{
			List<List<int>> arrState = new List<List<int>>();
			int i = arr.IndexOf(9);

			if (i % 3 > 0)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i - 1];
				copy[i - 1] = temp;
				arrState.Add(copy);

			}

			if (i - 3 >= 0)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i - 3];
				copy[i - 3] = temp;
				arrState.Add(copy);
			}

			if (i % 3 < 2)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i + 1];
				copy[i + 1] = temp;
				arrState.Add(copy);
			}

			if (i + 3 < arr.Count)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i + 3];
				copy[i + 3] = temp;
				arrState.Add(copy);
			}
			return arrState;
		}

		public List<List<int>> TaoMang_15Puzzle(List<int> arr)
		{
			List<List<int>> arrState = new List<List<int>>();
			int i = arr.IndexOf(16);

			if (i % 4 > 0)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i - 1];
				copy[i - 1] = temp;
				arrState.Add(copy);

			}

			if (i - 4 >= 0)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i - 4];
				copy[i - 4] = temp;
				arrState.Add(copy);
			}

			if (i % 4 < 3)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i + 1];
				copy[i + 1] = temp;
				arrState.Add(copy);
			}

			if (i + 4 < arr.Count)
			{
				List<int> copy = new List<int>(arr);
				int temp = copy[i];
				copy[i] = copy[i + 4];
				copy[i + 4] = temp;
				arrState.Add(copy);
			}
			return arrState;
		}

		public List<State> PhanTich_State()
		{
			List<State> arrState = new List<State>();
			List<List<int>> generateArray = TaoMang(this.state);
			for (int i = 0; i < generateArray.Count; i++)
			{
				State state = new State(generateArray[i]);
				arrState.Add(state);
				state.parent = this;
			}
			return arrState;
		}


		public List<State> PhanTich_State_15Puzzle()
		{
			List<State> arrState = new List<State>();
			List<List<int>> generateArray = TaoMang_15Puzzle(this.state);
			for (int i = 0; i < generateArray.Count; i++)
			{
				State state = new State(generateArray[i]);
				arrState.Add(state);
				state.parent = this;
			}
			return arrState;
		}

		public bool CheckGoal(State GoalState)
		{
			bool Goal = true;

			for (int i = 0; i < GoalState.state.Count; i++)
			{
				if (this.state[i] != GoalState.state[i])
				{
					Goal = false;
					break;
				}
			}
			return Goal;
		}
		
		public void PrintState()
		{
			int i = 0;
			foreach (var item in this.state)
			{
				Console.Write(item + "\t");
				i += 1;
				if (i % ((Puzzle.puzzle.checkShowKhung) ? 3 : 4) == 0)
					Console.Write("\n");
			}
			Console.Write("\n\n");
		}

		public void TruyVet(List<State> ListDuongDi, State s)
		{
			State state = s;
			ListDuongDi.Add(state);

			while (state.parent != null)
			{
				state = state.parent;
				ListDuongDi.Add(state);
			}
		}

		public void InDuongDi(List<State> temp)
		{
			foreach (var item in temp)
			{
				item.PrintState();
			}
		}

		public int hCost(State GoalState)
		{
			int h = 0;
			for (int i = 0; i < GoalState.state.Count; i++)
			{
				if (this.state[i] != GoalState.state[i])
				{
					h++;
				}
			}
			this.h_Cost = h;
			return h - 1;
		}


		public int fCost(int h, int g)
		{
			this.f_Cost = h + g;
			return h + g;
		}

		public void SortFcost(List<State> temp)
		{
			for (int i = 0; i < temp.Count - 1; i++)
			{
				for (int j = i + 1; j < temp.Count; j++)
				{
					if (temp[i].f_Cost > temp[j].f_Cost)
					{
						State t = temp[i];
						temp[i] = temp[j];
						temp[j] = t;
					}
				}
			}
		}

		public void SortHCost(List<State> temp)
		{
			for (int i = 0; i < temp.Count - 1; i++)
			{
				for (int j = i + 1; j < temp.Count; j++)
				{
					if (temp[i].h_Cost < temp[j].h_Cost)
					{
						State t = temp[i];
						temp[i] = temp[j];
						temp[j] = t;
					}
				}
			}
		}

		public void SortHCost_InCre(List<State> temp)
		{
			for (int i = 0; i < temp.Count - 1; i++)
			{
				for (int j = i + 1; j < temp.Count; j++)
				{
					if (temp[i].h_Cost > temp[j].h_Cost)
					{
						State t = temp[i];
						temp[i] = temp[j];
						temp[j] = t;
					}
				}
			}
		}
	}
}
