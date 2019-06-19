using System;
using System.Threading;

namespace MyCoolSimpleGame
{
	class Program
	{
		static bool _AI = false;
		static int _AIspeed = 1;
		static void Main(string[] args)
		{
			PrepareWorld();
			while (true)
				Move();
		}
		
		static int x, y, Px, Py, Fx, Fy;
		static void PrepareWorld()
		{
			w(true, "Enter world size X(8~20):");
			x = int.Parse(Console.ReadLine());
			w(true, "Enter world size Y(10~30):");
			y = int.Parse(Console.ReadLine());
			
			Px = x / 2;
			Py = y / 2;
			
			var r = new Random();
			Fx = r.Next(2, y);
			Fy = r.Next(2, x);
			CreateMatrix(x, y, Px, Py, Fx, Fy);
		}
		
		static void CreateMatrix(int x, int y, int Px, int Py, int Fx, int Fy)
		{
			Console.Clear();
			for (int i = 1; i <= x; i++) {
				for (int o = 1; o <= y; o++) {
					if (Fx == o && Fy == i) {
						w(false, "8", ConsoleColor.Cyan);
					} else if (Px == o && Py == i) {
						w(false, "O", ConsoleColor.Green);
					} else {
						w(false, "*", ConsoleColor.Yellow);
					}
				}
				w();
			}
		}
		
		static int score = 0;
		static bool DEBUG;
		static void Move()
		{
			#region AI
			if (_AI) {
				Thread.Sleep(200 / _AIspeed);
				
				if (Px < Fx)
					Px++;
				else if (Px > Fx)
					Px--;
				
				if (Py < Fy)
					Py++;
				else if (Py > Fy)
					Py--;
			}
			#endregion
			
			//Player Input
			if (!_AI) {
				Console.WriteLine("Input W/A/S/D to move! \r\nto enter Debug mode press 'space' \r\nto enter AI mode press BackSpace");
				ConsoleKeyInfo ConsoleK = Console.ReadKey();
				switch (ConsoleK.Key) {
					case ConsoleKey.W:
						Py--;
						break;
					case ConsoleKey.S:
						Py++;
						break;
					case ConsoleKey.A:
						Px--;
						break;
					case ConsoleKey.D:
						Px++;
						break;
					case ConsoleKey.Spacebar:
						DEBUG = !DEBUG; // Debug mode
						break;
				}
				switch (ConsoleK.Key) {
					case ConsoleKey.UpArrow:
						Py--;
						break;
					case ConsoleKey.DownArrow:
						Py++;
						break;
					case ConsoleKey.LeftArrow:
						Px--;
						break;
					case ConsoleKey.RightArrow:
						Px++;
						break;
					case ConsoleKey.Backspace: // AI mode
						Console.Clear();
						w(true, "Set the AI Speed(1~3)", ConsoleColor.Yellow);
						int i = int.Parse(Console.ReadLine());
						switch (i) {
							case 1:
								_AIspeed = 1;
								break;
							case 2:
								_AIspeed = 2;
								break;
							case 3:
								_AIspeed = 3;
								break;
						}
						_AI = !_AI;
						break;
				}
			}
			
			
			//Update & Refresh
			CreateMatrix(x, y, Px, Py, Fx, Fy);
			if (Px == Fx && Py == Fy) {
				var r = new Random();
				Fx = r.Next(2, y);
				Fy = r.Next(2, x);
				CreateMatrix(x, y, Px, Py, Fx, Fy);
				score++;
			}
			w(true, "Score:" + score, ConsoleColor.Yellow);
			
			if (_AI)
				w(ConsoleColor.Cyan, "AI mode activated");
			
			if (DEBUG)
				w(ConsoleColor.Red, "(DEBUG)" + "\r\npx:{0} py:{1}\r\nfx:{2} fy:{3}", Px, Py, Fx, Fy);
		}
		
		#region output tool
		static void w()
		{
			Console.WriteLine();
		}
		static void w(ConsoleColor c, string text, params object[] txt)
		{
			Console.ForegroundColor = c;
			Console.WriteLine(text, txt);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		static void w(bool line, string txt)
		{
			if (line) {
				Console.WriteLine(txt);
			} else {
				Console.Write(txt);
			}
		}
		static void w(bool line, string txt, ConsoleColor c)
		{
			if (line) {
				Console.ForegroundColor = c;
				Console.WriteLine(txt);
				Console.ForegroundColor = ConsoleColor.Gray;
			} else {
				Console.ForegroundColor = c;
				Console.Write(txt);
				Console.ForegroundColor = ConsoleColor.Gray;
			}
		}
		#endregion
	}
}
