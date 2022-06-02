using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleAI
{
    internal class BFS
    {
		State state = new State();

		State StartState;
		State GoalState;
		
		public int count = 0;

		public BFS(State StartState, State GoalState)
		{
			this.StartState = StartState;
			this.GoalState = GoalState;
		}

		public List<State> Solve_BFS()
		{
			count = 0;
			List<State> ListStateResult = new List<State>();
			Queue<State> Open = new Queue<State>();
			List<State> Closed = new List<State>();

			Open.Enqueue(StartState);

			while (Open.Count > 0)
			{
				State state_putout = Open.Dequeue();
				Closed.Add(state_putout);
				count++;

				if (state_putout.CheckGoal(GoalState))
				{
					Console.WriteLine("Chien Thang!");
					break;
				}

				List<State> ListStatePhatSinh;

				if (Puzzle.puzzle.checkShowKhung)
				{
					ListStatePhatSinh = state_putout.PhanTich_State();
				}
				else
				{
					ListStatePhatSinh = state_putout.PhanTich_State_15Puzzle();
				}

				foreach (var item in ListStatePhatSinh)
				{
					if (item.CheckGoal(GoalState))
					{
						Console.WriteLine("\n");
						Console.WriteLine("Thuat toan: BFS");
						Console.WriteLine("Tong dinh duyet: " + count);
						Console.WriteLine("Duong di: ");
						state.TruyVet(ListStateResult, item);
						state.InDuongDi(ListStateResult);
						return ListStateResult;
					}

					if (!state.CheckStateSame_InList(Closed, item))
					{
						Open.Enqueue(item);
					}
				}
			}
			return ListStateResult;
		}
	}
}
