using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PieceSeperation = System.Tuple<bool[,], bool[,]>;

namespace UTTTGameLib
{
	public class UTTTGame
	{
		public PieceState[,] GameState { get; private set; } = new PieceState[8, 8];

		private int totalTurns = 0;
		private int turnIndex = 0;
		public int TurnIndex
		{
			get => turnIndex;
			set => turnIndex = value % 2;
		}

		public Tuple<int, int> GetClickedPoint(Point mouse, int w, int h)
		{
			int rs = GetSquareSize(w, h);
			if (mouse.X <= rs * 8)
			{
				if (mouse.Y <= rs * 8)
				{
					return new Tuple<int, int>(mouse.X / rs, mouse.Y / rs);
				}
			}
			return null;
		}

		//0 = no winner
		//1 = Player1 wins
		//2 = Player2 wins
		public int GetWinner()
		{
			PieceSeperation seperated = SeperatePieces();
			if (Overlaps.DoesOverlap(seperated.Item1))
			{
				return 1;
			}
			if (Overlaps.DoesOverlap(seperated.Item2))
			{
				return 2;
			}
			return 0;
		}

		public void PlayPiece(int x, int y, IntPtr hPanel, out int winner)
		{
			winner = 0;
			if (!CanPlacePiece(x, y))
			{
				winner = -1;
				return;
			}
			GameState[y, x] = TurnIndex == 0 ? PieceState.P1 : PieceState.P2;
			winner = AdvanceTurn(hPanel);
		}

		public void SetPiece(int x, int y, PieceState p) => GameState[y, x] = p;

		public PieceSeperation SeperatePieces()
		{
			bool[,] p1 = new bool[8, 8];
			bool[,] p2 = new bool[8, 8];
			for (int y = 0; y < GameState.GetLength(0); y++)
			{
				for (int x = 0; x < GameState.GetLength(1); x++)
				{
					p1[x, y] = GameState[x, y] == PieceState.P1;
					p2[x, y] = GameState[x, y] == PieceState.P2;
				}
			}
			return new PieceSeperation(p1, p2);
		}

		public int AdvanceTurn(IntPtr hPanel)
		{
			int winner = GetWinner();
			if (winner != 0)
			{
				return winner;
			}
			totalTurns++;
			Console.WriteLine($"Total Turns: {totalTurns}");
			TurnIndex++;
			if (hPanel != IntPtr.Zero)
			{
				Panel p = (Panel)Control.FromHandle(hPanel);
				DrawOnPanel(ref p);
			}
			return 0;
		}

		public bool CanPlacePiece(int x, int y)
		{
			if (x < 0 || x > 7 || y < 0 || y > 7)
			{
				return false;
			}
			if (totalTurns <= 2)
			{
				Console.WriteLine("Check if on edge");
				//on player 1's first and second turn they can only place on the edge
				//if x is 0 or 7 it has to be on the edge
				//if it is not 0 or 7, we check the y
				//if the y 0 or 7 we are on the edge
				if (x == 0 || x == 7)
				{
					return GameState[y, x] == PieceState.NONE;
				}
				if (y == 0 || y == 7)
				{
					return GameState[y, x] == PieceState.NONE;
				}
				return false;
			}
			return GameState[y, x] == PieceState.NONE;
		}

		public void DrawOnPanel(ref Panel p)
		{
			using (Graphics g = p.CreateGraphics())
			{
				int rs = GetSquareSize(p.Width, p.Height);
				//Reset
				g.FillRectangle(new SolidBrush(p.BackColor), new RectangleF(0, 0, p.Width, p.Height));
				//Draw grid
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						int n = (8 * i) + j + (i % 2);
						Rectangle r = new Rectangle(j * rs, i * rs, rs, rs);
						g.FillRectangle(new SolidBrush(n % 2 == 0 ? Color.FromArgb(0x7F, 0x40, 0x00) : Color.Tan), r);
					}
				}
				//Draw pieces
				for (int y = 0; y < GameState.GetLength(0); y++)
				{
					for (int x = 0; x < GameState.GetLength(1); x++)
					{
						int cx = x * rs;
						int cy = y * rs;
						cx += rs / 3;
						cy += rs / 3;
						switch (GameState[y, x])
						{
							case PieceState.P1:
								UTTTUtils.DrawCircle(g, Color.White, cx, cy, rs / 3);
								break;
							case PieceState.P2:
								UTTTUtils.DrawCircle(g, Color.Black, cx, cy, rs / 3);
								break;
						}
					}
				}
				//Draw turn indicator (p1 is bottom p2 is top)
				SolidBrush brush = new SolidBrush(Color.Red);
				if (turnIndex == 0)
				{
					g.FillRectangle(brush, new Rectangle((rs * 8) + 5, (rs * 8) - (rs / 4) - 5, rs / 4, rs / 4));
				}
				else
				{
					g.FillRectangle(brush, new Rectangle((rs * 8) + 5, 5, rs / 4, rs / 4));
				}
			}
		}

		private static int GetSquareSize(int w, int h) => Math.Min(w / 8, h / 8) - 15;
	}
}
