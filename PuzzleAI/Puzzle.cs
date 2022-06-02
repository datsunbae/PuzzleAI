using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleAI
{
    public partial class Puzzle : Form
    {
        public static Puzzle puzzle;
        public GroupBox khung_8Puzzle;
        public GroupBox khung_15Puzzle;

        List<Bitmap> ListHinh_8Puzzle  = new List<Bitmap>();
        List<Bitmap> ListHinh_15Puzzle = new List<Bitmap>();

        List<List<int>> ListTestCase_8Puzzle = new List<List<int>>();
        List<List<int>> ListTestCase_15Puzzle = new List<List<int>>();

        List<int> ListRandom_8Puzzle;
        List<int> ListRandom_15Puzzle;

        List<int> ListGoal_8Puzzle = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> ListGoal_15Puzzle = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };


        List<State> ListResult = new List<State>();
        int OTrong_Index = 0;
        int OTrong_Index_15Puzzle = 0;
        int currentState = 0;
        public bool checkShowKhung;

        List<int> testCase1 = new List<int> { 4, 5, 9, 3, 1, 6, 7, 2, 8 };  
        List<int> testCase2 = new List<int> { 9, 1, 2, 3, 6, 5, 4, 8, 7 }; 
        List<int> testCase3 = new List<int> { 4, 8, 9, 6, 1, 5, 7, 3, 2 };     
        List<int> testCase4 = new List<int> { 4, 5, 6, 3, 2, 1, 7, 8, 9 };
        List<int> testCase5 = new List<int> { 4, 1, 5, 2, 6, 9, 3, 7, 8 };
        List<int> testCase6 = new List<int> { 4, 1, 5, 9, 3, 2, 7, 8, 6 };
        List<int> testCase7 = new List<int> { 1, 2, 9, 3, 4, 6, 7, 5, 8 };
        List<int> testCase8 = new List<int> { 4, 9, 5, 3, 1, 6, 7, 2, 8 };
        List<int> testCase9 = new List<int> { 1, 5, 2, 7, 4, 3, 9, 8, 6 };
        List<int> testCase10 = new List<int> { 9, 1, 3, 2, 6, 5, 4, 7, 8 };

        List<int> testCase11 = new List<int> { 1, 16, 3, 4, 5, 2, 7, 8, 9, 6, 10, 11, 13, 14, 15, 12 };
        List<int> testCase12 = new List<int> { 5, 1, 2, 4, 16, 6, 3, 8, 9, 10, 7, 11, 13, 14, 15, 12 };
        List<int> testCase13 = new List<int> { 1, 6, 2, 4, 5, 10, 3, 8, 9, 14, 7, 11, 13, 16, 15, 12 };
        List<int> testCase14 = new List<int> { 1, 6, 2, 4, 16, 5, 10, 8, 9, 14, 3, 11, 13, 15, 7, 12 };


        public Puzzle()
        {
            InitializeComponent();

            puzzle = this;


            ListHinh_8Puzzle.AddRange(new Bitmap[] { Properties.Resources.Hinh1, Properties.Resources.Hinh2, Properties.Resources.Hinh3, Properties.Resources.Hinh4,
            Properties.Resources.Hinh5, Properties.Resources.Hinh6, Properties.Resources.Hinh7, Properties.Resources.Hinh8, Properties.Resources.Hinh9 });


            ListHinh_15Puzzle.AddRange(new Bitmap[] { Properties.Resources.pic1, Properties.Resources.pic2, Properties.Resources.pic3, Properties.Resources.pic4,
            Properties.Resources.pic5, Properties.Resources.pic6, Properties.Resources.pic7, Properties.Resources.pic8, Properties.Resources.pic9, Properties.Resources.pic10,
            Properties.Resources.pic11, Properties.Resources.pic12, Properties.Resources.pic13, Properties.Resources.pic14, Properties.Resources.pic15, Properties.Resources.pic16});

            AddTestCase();

        }

        private void Puzzle_Load(object sender, EventArgs e)
        {
            RandomPicture();

            RandomPicture_15Puzzle();

            btn_8Puzzle.Enabled = false;
            checkShowKhung = true;
            Khung15Puzzle.Hide();
        }

        private void AddTestCase()
        {
            ListTestCase_8Puzzle.Add(testCase1);
            ListTestCase_8Puzzle.Add(testCase2);
            ListTestCase_8Puzzle.Add(testCase3);
            ListTestCase_8Puzzle.Add(testCase4);
            ListTestCase_8Puzzle.Add(testCase5);
            ListTestCase_8Puzzle.Add(testCase6);
            ListTestCase_8Puzzle.Add(testCase7);
            ListTestCase_8Puzzle.Add(testCase8);
            ListTestCase_8Puzzle.Add(testCase9);
            ListTestCase_8Puzzle.Add(testCase10);

            ListTestCase_15Puzzle.Add(testCase11);
            ListTestCase_15Puzzle.Add(testCase12);
            ListTestCase_15Puzzle.Add(testCase13);
            ListTestCase_15Puzzle.Add(testCase14);
        }

        private void RandomPicture()
        {
            Random r = new Random();
            int j = r.Next(0, 10);
            ListRandom_8Puzzle = ListTestCase_8Puzzle[j];

            for (int i = 0; i < 9; i++)
            {
                ((PictureBox)KhungHinh.Controls[i]).Image = ListHinh_8Puzzle[ListRandom_8Puzzle[i] - 1];
                if (ListRandom_8Puzzle[i] == 9)
                    OTrong_Index = i;
            }
        }

        private void RandomPicture_15Puzzle()
        {
            Random r = new Random();
            int j = r.Next(0, 4);
            ListRandom_15Puzzle = ListTestCase_15Puzzle[j];

            for (int i = 0; i < 16; i++)
            {
                ((PictureBox)Khung15Puzzle.Controls[i]).Image = ListHinh_15Puzzle[ListRandom_15Puzzle[i] - 1];
                if (ListRandom_15Puzzle[i] == 16)
                    OTrong_Index_15Puzzle = i;
            }
        }

        private void MovePicture(object sender, EventArgs e)
        {
            int OChon_Index = KhungHinh.Controls.IndexOf(sender as Control);
            int OchonInListRandom = ListRandom_8Puzzle[OChon_Index];
            
            if (OTrong_Index != OChon_Index)
            {
                List<int> listCheckMove = new List<int>(new int[] { ((OChon_Index % 3 == 0) ? -1 : OChon_Index - 1), OChon_Index - 3, (OChon_Index % 3 == 2) ? -1 : OChon_Index + 1, OChon_Index + 3 });
                
                if (listCheckMove.Contains(OTrong_Index))
                {
                    ((PictureBox)KhungHinh.Controls[OTrong_Index]).Image = ((PictureBox)KhungHinh.Controls[OChon_Index]).Image;
                    ((PictureBox)KhungHinh.Controls[OChon_Index]).Image = ListHinh_8Puzzle[8];

                    for(int i = 0; i < 9; i++)
                    {
                        if (ListRandom_8Puzzle[i] == 9)
                        {
                            ListRandom_8Puzzle[i] = OchonInListRandom;
                        }
                        else
                        {
                            if (ListRandom_8Puzzle[i] == OchonInListRandom)
                            {
                                ListRandom_8Puzzle[i] = 9;
                            }
                        }
                    }
                    OTrong_Index = OChon_Index;
                }
            }

        }

        private void MovePicture_15Puzzle(object sender, EventArgs e)
        {
            int OChon_Index = Khung15Puzzle.Controls.IndexOf(sender as Control);
            int OchonInListRandom = ListRandom_15Puzzle[OChon_Index];

            if (OTrong_Index_15Puzzle != OChon_Index)
            {
                List<int> listCheckMove = new List<int>(new int[] { ((OChon_Index % 4 == 0) ? -1 : OChon_Index - 1), OChon_Index - 4, (OChon_Index % 4 == 3) ? -1 : OChon_Index + 1, OChon_Index + 4 });

                if (listCheckMove.Contains(OTrong_Index_15Puzzle))
                {
                    ((PictureBox)Khung15Puzzle.Controls[OTrong_Index_15Puzzle]).Image = ((PictureBox)Khung15Puzzle.Controls[OChon_Index]).Image;
                    ((PictureBox)Khung15Puzzle.Controls[OChon_Index]).Image = ListHinh_15Puzzle[15];

                    for (int i = 0; i < 16; i++)
                    {
                        if (ListRandom_15Puzzle[i] == 16)
                        {
                            ListRandom_15Puzzle[i] = OchonInListRandom;
                        }
                        else
                        {
                            if (ListRandom_15Puzzle[i] == OchonInListRandom)
                            {
                                ListRandom_15Puzzle[i] = 16;
                            }
                        }
                    }
                    OTrong_Index_15Puzzle = OChon_Index;
                }
            }

        }

        private void btn_newGame_Click(object sender, EventArgs e)
        {
            if (checkShowKhung)
            {
                RandomPicture();
            }
            else
            {
                RandomPicture_15Puzzle();
            }
            lbl_ThuatGiai.Text = "Thuật giải: ";
            txt_TongBuocDuyet.Text = "Tổng bước duyệt: ";
            txt_time.Text = "Thời gian giải: ";
            lbl_stepNum.Text = "Số bước đi: ";
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (currentState < ListResult.Count - 1)
            {
                currentState += 1;

                this.lbl_stepNum.Text = "Số Bước Đi: " + (currentState + 1).ToString() + "/" + this.ListResult.Count.ToString();

                State trt = ListResult[currentState];


                if (checkShowKhung)
                {
                    for (int i = 0; i < trt.state.Count; i++)
                    {
                        ((PictureBox)KhungHinh.Controls[i]).Image = ListHinh_8Puzzle[trt.state[i] - 1];
                    }
                }
                else
                {
                    for (int i = 0; i < trt.state.Count; i++)
                    {
                        ((PictureBox)Khung15Puzzle.Controls[i]).Image = ListHinh_15Puzzle[trt.state[i] - 1];
                    }
                } 
            }
        }

        private void btn_SolveBFS_Click(object sender, EventArgs e)
        {
            lbl_ThuatGiai.Text = "Thuật giải: Breadth-first search";

            List<int> KhoiTao;
            State StartState;
            State GoalState;

            if (checkShowKhung)
            {
                KhoiTao = ListRandom_8Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_8Puzzle);
            }
            else
            {
                KhoiTao = ListRandom_15Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_15Puzzle);
            }

            BFS bfs = new BFS(StartState, GoalState);

            Stopwatch time_Solve = new Stopwatch();
            time_Solve.Reset();
            time_Solve.Start();

            this.ListResult = bfs.Solve_BFS();

            txt_time.Text = "Thời gian giải: " + time_Solve.Elapsed.TotalMilliseconds.ToString() + " ms";

            this.ListResult.Reverse();
            this.currentState = 0;

            this.txt_TongBuocDuyet.Text = "Số Bước Duyệt: " + bfs.count.ToString();
            this.lbl_stepNum.Text = "Số Bước Đi: " + (currentState + 1).ToString() + "/" + this.ListResult.Count.ToString();

            State tmp = this.ListResult[this.currentState];
            List<int> arr = tmp.state;

            if (checkShowKhung)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)KhungHinh.Controls[i]).Image = ListHinh_8Puzzle[arr[i] - 1];
                }
            }
            else
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)Khung15Puzzle.Controls[i]).Image = ListHinh_15Puzzle[arr[i] - 1];
                }
            }

        }

        private void btn_SolveBeFS_Click(object sender, EventArgs e)
        {
            lbl_ThuatGiai.Text = "Thuật giải: Best-first search";

            List<int> KhoiTao;
            State StartState;
            State GoalState;

            if (checkShowKhung)
            {
                KhoiTao = ListRandom_8Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_8Puzzle);
            }
            else
            {
                KhoiTao = ListRandom_15Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_15Puzzle);
            }
           
            BeFS befs = new BeFS(StartState, GoalState);

            Stopwatch time_Solve = new Stopwatch();
            time_Solve.Reset();
            time_Solve.Start();

            this.ListResult = befs.Solve_BestFirstSearch();

            txt_time.Text = "Thời gian giải: " + time_Solve.Elapsed.TotalMilliseconds.ToString() + " ms";

            this.ListResult.Reverse();
            this.currentState = 0;

            this.txt_TongBuocDuyet.Text = "Số Bước Duyệt: " + befs.count.ToString();
            this.lbl_stepNum.Text = "Số Bước Đi: " + (currentState + 1).ToString() + "/" + this.ListResult.Count.ToString();
            State tmp = this.ListResult[this.currentState];
            List<int> arr = tmp.state;
            

            if (checkShowKhung)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)KhungHinh.Controls[i]).Image = ListHinh_8Puzzle[arr[i] - 1];
                }
            }
            else
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)Khung15Puzzle.Controls[i]).Image = ListHinh_15Puzzle[arr[i] - 1];
                }
            }

        }    

        private void btn_SolveAStar_Click(object sender, EventArgs e)
        {
            lbl_ThuatGiai.Text = "Thuật giải: A*";

            List<int> KhoiTao;
            State StartState;
            State GoalState;

            if (checkShowKhung)
            {
                KhoiTao = ListRandom_8Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_8Puzzle);
            }
            else
            {
                KhoiTao = ListRandom_15Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_15Puzzle);
            }

            AStar astar = new AStar(StartState, GoalState);

            Stopwatch time_Solve = new Stopwatch();
            time_Solve.Reset();
            time_Solve.Start();

            StartState.g_Cost = 0;
            int h = StartState.hCost(GoalState);
            int f = StartState.fCost(h, 0);
            StartState.f_Cost = f;

            this.ListResult = astar.Solve_AStar();

            txt_time.Text = "Thời gian giải: " + time_Solve.Elapsed.TotalMilliseconds.ToString() + " ms";

            this.ListResult.Reverse();
            this.currentState = 0;

            this.txt_TongBuocDuyet.Text = "Số Bước Duyệt: " + astar.count.ToString();
            this.lbl_stepNum.Text = "Số Bước Đi: " + (currentState + 1).ToString() + "/" + this.ListResult.Count.ToString();

            State tmp = this.ListResult[this.currentState];
            List<int> arr = tmp.state;

            if (checkShowKhung)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)KhungHinh.Controls[i]).Image = ListHinh_8Puzzle[arr[i] - 1];
                }
            }
            else
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)Khung15Puzzle.Controls[i]).Image = ListHinh_15Puzzle[arr[i] - 1];
                }
            }
        }

        private void btn_SolveHillClimbing_Click(object sender, EventArgs e)
        {
            lbl_ThuatGiai.Text = "Thuật giải: Hill Climbing";

            List<int> KhoiTao;
            State StartState;
            State GoalState;

            if (checkShowKhung)
            {
                KhoiTao = ListRandom_8Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_8Puzzle);
            }
            else
            {
                KhoiTao = ListRandom_15Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_15Puzzle);
            }

            HillClimbing hillClimbing = new HillClimbing(StartState, GoalState);

            Stopwatch time_Solve = new Stopwatch();
            time_Solve.Reset();
            time_Solve.Start();

            this.ListResult = hillClimbing.Solve_HillClimbing();

            txt_time.Text = "Thời gian giải: " + time_Solve.Elapsed.TotalMilliseconds.ToString() + " ms";

            this.ListResult.Reverse();
            this.currentState = 0;

            this.txt_TongBuocDuyet.Text = "Số Bước Duyệt: " + hillClimbing.count.ToString();
            this.lbl_stepNum.Text = "Số Bước Đi: " + (currentState + 1).ToString() + "/" + this.ListResult.Count.ToString();

            State tmp = this.ListResult[this.currentState];
            List<int> arr = tmp.state;

            if (checkShowKhung)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)KhungHinh.Controls[i]).Image = ListHinh_8Puzzle[arr[i] - 1];
                }
            }
            else
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)Khung15Puzzle.Controls[i]).Image = ListHinh_15Puzzle[arr[i] - 1];
                }
            }
        }

        private void btn_SolveAKT_Click(object sender, EventArgs e)
        {
            lbl_ThuatGiai.Text = "Thuật giải: AKT";

            List<int> KhoiTao;
            State StartState;
            State GoalState;

            if (checkShowKhung)
            {
                KhoiTao = ListRandom_8Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_8Puzzle);
            }
            else
            {
                KhoiTao = ListRandom_15Puzzle;
                StartState = new State(KhoiTao);
                GoalState = new State(ListGoal_15Puzzle);
            }

            AKT akt = new AKT(StartState, GoalState);

            Stopwatch time_Solve = new Stopwatch();
            time_Solve.Reset();
            time_Solve.Start();

            StartState.g_Cost = 0;
            int h = StartState.hCost(GoalState);
            int f = StartState.fCost(h, 0);
            StartState.f_Cost = f;

            this.ListResult = akt.Solve_AKT();

            txt_time.Text = "Thời gian giải: " + time_Solve.Elapsed.TotalMilliseconds.ToString() + " ms";

            this.ListResult.Reverse();
            this.currentState = 0;

            this.txt_TongBuocDuyet.Text = "Số Bước Duyệt: " + akt.count.ToString();
            this.lbl_stepNum.Text = "Số Bước Đi: " + (currentState + 1).ToString() + "/" + this.ListResult.Count.ToString();

            State tmp = this.ListResult[this.currentState];
            List<int> arr = tmp.state;

            if (checkShowKhung)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)KhungHinh.Controls[i]).Image = ListHinh_8Puzzle[arr[i] - 1];
                }
            }
            else
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    ((PictureBox)Khung15Puzzle.Controls[i]).Image = ListHinh_15Puzzle[arr[i] - 1];
                }
            }
        }

        private void btn_8Puzzle_Click(object sender, EventArgs e)
        {
            KhungHinh.Show();
            Khung15Puzzle.Hide();
            btn_8Puzzle.Enabled = false;
            btn_15Puzzle.Enabled = true;
            checkShowKhung = true;
        }

        private void btn_15Puzzle_Click(object sender, EventArgs e)
        {
            Khung15Puzzle.Show();
            KhungHinh.Hide();
            btn_15Puzzle.Enabled = false;
            btn_8Puzzle.Enabled = true;
            checkShowKhung = false;
        } 
    }
}
