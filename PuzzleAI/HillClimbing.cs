using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleAI
{
	internal class HillClimbing
	{
		State state = new State();

		State StartState;
		State GoalState;

		public int count = 0;
		public HillClimbing(State StartState, State GoalState)
		{
			this.StartState = StartState;
			this.GoalState = GoalState;
		}

		public List<State> Solve_HillClimbing()
		{
			count = 0;
			List<State> ListStateResult = new List<State>();
			Stack<State> Open = new Stack<State>();
			List<State> Closed = new List<State>();

			int hStartState = StartState.hCost(GoalState);
			StartState.h_Cost = hStartState;
			State state_putout = StartState;

			Open.Push(StartState);

			while (Open.Count > 0)
			{
				State state_peek = Open.Peek();

                while (!state.CheckStateSame(state_peek, StartState) && state_peek.h_Cost > state_peek.parent.h_Cost)
                {
                    state_putout = Open.Pop();
                    state_peek = Open.Peek();
                    Closed.Add(state_putout);
					//count++;
                }

				state_putout = Open.Pop();
				Closed.Add(state_putout);
				count++;

				if (state_putout.CheckGoal(GoalState))
				{
					Console.WriteLine("\n");
					Console.WriteLine("Thuat toan: Hill Climbing");
					Console.WriteLine("Duong di: ");
					state.TruyVet(ListStateResult, state_putout);
					state.InDuongDi(ListStateResult);
					return ListStateResult;
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

				List<State> ListState = new List<State>();
				foreach (var item in ListStatePhatSinh)
				{
					if (!state.CheckStateSame_InList(Closed, item))
					{
						int h = item.hCost(GoalState);
						item.h_Cost = h;
						ListState.Add(item);
					}
				}

				state.SortHCost(ListState);
					
				foreach (var item in ListState)
				{
					Open.Push(item);
				}
			}
			return ListStateResult;
		}
	}
}
