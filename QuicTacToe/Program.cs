using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace QicTacToe
{
    public class GameForm : Form
    {
        private Button[,] board;
        private int[,] gameState;
        private int currentPlayer;
        private Queue<Point>[] playerMoves;
        private string[] playerNames;
        private TextBox player1TextBox;
        private TextBox player2TextBox;
        private Button startGameButton;
        private Label timerLabel;
        private System.Timers.Timer turnTimer;
        private int timeLeft;
        private readonly string[] timerGraphics = { " ☠️", "💣*", "💣--*", "💣----*" };

        public GameForm()
        {
            this.Text = "QicTacToe";
            this.ClientSize = new Size(320, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            player1TextBox = new TextBox { Location = new Point(10, 10), Width = 140, PlaceholderText = "Player 1 Name" };
            player2TextBox = new TextBox { Location = new Point(160, 10), Width = 140, PlaceholderText = "Player 2 Name" };
            startGameButton = new Button { Text = "Start Game", Location = new Point(110, 40), Width = 100 };
            timerLabel = new Label { Text = "", Location = new Point(130, 70), Width = 60, Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold) };
            startGameButton.Click += StartGame;

            
            this.Controls.Add(player1TextBox);
            this.Controls.Add(player2TextBox);
            this.Controls.Add(startGameButton);
            this.Controls.Add(timerLabel);

            board = new Button[3, 3];
            gameState = new int[3, 3];
            playerMoves = new Queue<Point>[2] { new Queue<Point>(), new Queue<Point>() };
            playerNames = new string[2] { "Player 1", "Player 2" };
            currentPlayer = 1;
            InitializeBoard();
            ToggleBoard(false);

            turnTimer = new System.Timers.Timer(1000);
            turnTimer.Elapsed += UpdateTimer;
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(player1TextBox.Text))
                playerNames[0] = player1TextBox.Text;
            if (!string.IsNullOrWhiteSpace(player2TextBox.Text))
                playerNames[1] = player2TextBox.Text;
            ToggleBoard(true);
            startGameButton.Enabled = false;
            ResetTimer();
        }

        private void ToggleBoard(bool enabled)
        {
            foreach (var btn in board)
                btn.Enabled = enabled;
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(90, 90),
                        Location = new Point(col * 100 + 10, row * 100 + 100),
                        Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold),
                        Tag = new Point(row, col)
                    };
                    btn.Click += OnCellClick;
                    board[row, col] = btn;
                    this.Controls.Add(btn);
                }
            }
        }

        private void OnCellClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point position = (Point)clickedButton.Tag;
            int row = position.X;
            int col = position.Y;

            if (gameState[row, col] == 0)
            {
                if (playerMoves[currentPlayer - 1].Count == 3)
                {
                    Point oldestMove = playerMoves[currentPlayer - 1].Dequeue();
                    gameState[oldestMove.X, oldestMove.Y] = 0;
                    board[oldestMove.X, oldestMove.Y].Text = "";
                }

                gameState[row, col] = currentPlayer;
                clickedButton.Text = currentPlayer == 1 ? "X" : "O";
                playerMoves[currentPlayer - 1].Enqueue(position);
                CheckWinCondition();
                SwitchPlayer();
            }
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 1) ? 2 : 1;
            ResetTimer();
        }

        private void ResetTimer()
        {
            timeLeft = 4;
            turnTimer.Start();
            UpdateTimer(null, null);
        }

        private void UpdateTimer(object sender, ElapsedEventArgs e)
        {
            if (timeLeft == 0)
            {
                turnTimer.Stop();
                Invoke(new Action(() => EndGame(currentPlayer == 1 ? 2 : 1)));
            }
            else
            {
                timeLeft--;
                Invoke(new Action(() => timerLabel.Text = timerGraphics[timeLeft]));
            }
        }

        private void CheckWinCondition()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameState[i, 0] != 0 && gameState[i, 0] == gameState[i, 1] && gameState[i, 1] == gameState[i, 2])
                    EndGame(gameState[i, 0]);
                if (gameState[0, i] != 0 && gameState[0, i] == gameState[1, i] && gameState[1, i] == gameState[2, i])
                    EndGame(gameState[0, i]);
            }
            if (gameState[0, 0] != 0 && gameState[0, 0] == gameState[1, 1] && gameState[1, 1] == gameState[2, 2])
                EndGame(gameState[0, 0]);
            if (gameState[0, 2] != 0 && gameState[0, 2] == gameState[1, 1] && gameState[1, 1] == gameState[2, 0])
                EndGame(gameState[0, 2]);
        }

        private void EndGame(int winner)
        {
            MessageBox.Show($"{playerNames[winner - 1]} wins!", "Game Over");
            Application.Exit();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm());
        }
    }
}
