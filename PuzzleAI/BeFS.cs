using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleAI
{
    internal class BeFS
    {
		State state = new State();

		State StartState;
		State GoalState;

		public int count = 0;

		public BeFS(State StartState, State GoalState)
		{
			this.StartState = StartState;
			this.GoalState = GoalState;
		}

		public List<State> Solve_BestFirstSearch()
		{
			count = 0;
			List<State> ListStateResult = new List<State>();
			List<State> Open = new List<State>();
			List<State> Closed = new List<State>();

			Open.Add(StartState);

			while (Open.Count > 0)
			{
				State state_putout = Open[0];
				 
				count++;
				Closed.Add(state_putout);

				Open.RemoveAt(0); 

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
						Console.WriteLine("Thuat toan: Best Firt Search");
						Console.WriteLine("Tong dinh duyet: " + count);
						Console.WriteLine("Duong di: ");
						state.TruyVet(ListStateResult, item);
						state.InDuongDi(ListStateResult);
						return ListStateResult;
					}

					if (!state.CheckStateSame_InList(Closed, item) && !state.CheckStateSame_InList(Open, item))
					{
						int h = item.hCost(GoalState);
						item.h_Cost = h;

						Open.Add(item);

						state.SortHCost_InCre(Open);
					}
				}
			}
			return ListStateResult;
		}
	}
}
