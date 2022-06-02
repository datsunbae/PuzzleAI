using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleAI
{
    internal class AStar
    {
		State state = new State();

		State StartState;
		State GoalState;

		public int count = 0;

		public AStar(State StartState, State GoalState)
		{
			this.StartState = StartState;
			this.GoalState = GoalState;
			
		}
		public List<State> Solve_AStar()
		{
			count = 0;
			List<State> ListStateResult = new List<State>();
			List<State> Open = new List<State>();
			List<State> Close = new List<State>();

			Open.Add(StartState);

			int g;

			while (Open.Count > 0)
			{
				State state_putout = Open[0];
				g = state_putout.g_Cost;
				g++;
				count++;
				Close.Add(state_putout);

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
						Console.WriteLine("Thuat t oan: A*");
						Console.WriteLine("Tong dinh duyet: " + count);
						Console.WriteLine("Duong di: ");
						state.TruyVet(ListStateResult, item);
						state.InDuongDi(ListStateResult);
						return ListStateResult;
					}

					item.g_Cost = g;
					int h = item.hCost(GoalState);
					int f = item.fCost(h, g);
					item.f_Cost = f;

					if (state.CheckStateSame_InList(Open, item))
                    {
						for (int i = 0; i < Open.Count - 1; i++)
						{							
							if (state.CheckStateSame(item, Open[i]) && item.f_Cost < Open[i].f_Cost)
							{
								Open[i] = item;
								Open[i].g_Cost = item.g_Cost;
								Open[i].f_Cost = item.f_Cost;
								state.SortFcost(Open);		
							}
						}
					}
					else if(state.CheckStateSame_InList(Close, item))
                    {
						for (int i = 0; i < Close.Count - 1; i++)
                        {
							if (state.CheckStateSame(item, Close[i]) && item.f_Cost < Close[i].f_Cost)
							{
								Open.Add(Close[i]);
								state.SortFcost(Open);
								Close.RemoveAt(i);
							}
						}
					}
                    else
                    {							
						Open.Add(item);
						state.SortFcost(Open);
					}
				}
			}
			return ListStateResult;
		}
	}
}
