using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
	public partial class GameForm : Form
	{
		// Encapsulation: private fields
		private List<GameButton> buttons;
		private GameButton firstSelectedButton;
		private GameButton secondSelectedButton;
		private int score;
		private Timer matchCheckTimer;
		private Random random;

		public GameForm()
		{
			InitializeComponent();
			random = new Random();
			buttons = new List<GameButton>();
			score = 0;

			// Initialize the timer for checking matches
			matchCheckTimer = new Timer();
			matchCheckTimer.Interval = 500; // 500ms delay
			matchCheckTimer.Tick += MatchCheckTimer_Tick;

			CreateGameBoard();
			CreateRestartButton();
			CreateScoreLabel();
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// GameForm
			// 
			this.ClientSize = new System.Drawing.Size(484, 561);
			this.Name = "GameForm";
			this.Text = "Memory Game";
			this.ResumeLayout(false);
		}

		private void CreateGameBoard()
		{
			// Create a list of pairs of numbers
			List<int> numbers = new List<int>();
			for (int i = 1; i <= 8; i++)
			{
				numbers.Add(i);
				numbers.Add(i);
			}

			// Shuffle the numbers
			numbers = numbers.OrderBy(x => random.Next()).ToList();

			// Create the buttons
			int buttonSize = 80;
			int padding = 10;
			int startX = 50;
			int startY = 50;

			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < 4; col++)
				{
					int index = row * 4 + col;
					int number = numbers[index];

					GameButton button = new GameButton(number);
					button.Location = new Point(startX + col * (buttonSize + padding),
											   startY + row * (buttonSize + padding));
					button.Size = new Size(buttonSize, buttonSize);
					button.BackColor = Color.Black;
					button.ForeColor = Color.Black; // Hide the number initially
					button.Font = new Font("Arial", 20, FontStyle.Bold);
					button.Click += Button_Click;

					buttons.Add(button);
					this.Controls.Add(button);
				}
			}
		}

		private void CreateRestartButton()
		{
			Button restartButton = new Button();
			restartButton.Text = "Restart";
			restartButton.BackColor = Color.Red;
			restartButton.ForeColor = Color.White;
			restartButton.Font = new Font("Arial", 12, FontStyle.Bold);
			restartButton.Size = new Size(100, 40);
			restartButton.Location = new Point(190, 450);
			restartButton.Click += RestartButton_Click;

			this.Controls.Add(restartButton);
		}

		private void CreateScoreLabel()
		{
			Label scoreLabel = new Label();
			scoreLabel.Text = "Score: 0";
			scoreLabel.Font = new Font("Arial", 12, FontStyle.Bold);
			scoreLabel.Size = new Size(150, 30);
			scoreLabel.Location = new Point(190, 500);
			scoreLabel.Name = "scoreLabel";

			this.Controls.Add(scoreLabel);
		}

		private void Button_Click(object sender, EventArgs e)
		{
			GameButton clickedButton = sender as GameButton;

			// Ignore clicks on already matched buttons or when two buttons are already selected
			if (clickedButton.IsMatched || secondSelectedButton != null)
				return;

			// Reveal the number
			clickedButton.Reveal();

			if (firstSelectedButton == null)
			{
				// This is the first button clicked
				firstSelectedButton = clickedButton;
			}
			else if (firstSelectedButton != clickedButton)
			{
				// This is the second button clicked
				secondSelectedButton = clickedButton;

				// Start the timer to check for a match
				matchCheckTimer.Start();
			}
		}

		private void MatchCheckTimer_Tick(object sender, EventArgs e)
		{
			matchCheckTimer.Stop();

			// Check if the two buttons match
			if (firstSelectedButton.Number == secondSelectedButton.Number)
			{
				// Match found
				firstSelectedButton.SetMatched();
				secondSelectedButton.SetMatched();

				// Update score
				score += 2;
				UpdateScoreLabel();

				// Check if all matches are found
				if (buttons.All(b => b.IsMatched))
				{
					MessageBox.Show("Congratulations! You've found all matches!", "Game Over");
				}
			}
			else
			{
				// No match
				MessageBox.Show("WRONG MATCH", "Try Again");

				// Hide the numbers again
				firstSelectedButton.Hide();
				secondSelectedButton.Hide();
			}

			// Reset selected buttons
			firstSelectedButton = null;
			secondSelectedButton = null;
		}

		private void RestartButton_Click(object sender, EventArgs e)
		{
			// Reset the game
			foreach (GameButton button in buttons)
			{
				this.Controls.Remove(button);
			}

			buttons.Clear();
			firstSelectedButton = null;
			secondSelectedButton = null;
			score = 0;
			UpdateScoreLabel();

			CreateGameBoard();
		}

		private void UpdateScoreLabel()
		{
			foreach (Control control in this.Controls)
			{
				if (control.Name == "scoreLabel")
				{
					(control as Label).Text = $"Score: {score}";
					break;
				}
			}
		}
	}

}
