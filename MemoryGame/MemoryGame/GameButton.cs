using System.Drawing;
using System.Windows.Forms;

namespace MemoryGame
{
	// Inherits from ButtonBase - demonstrates inheritance
	public class GameButton : ButtonBase
	{
		// Encapsulation: private fields with public properties
		private int number;
		private bool isRevealed;
		private bool isMatched;

		public int Number => number;
		public bool IsMatched => isMatched;

		public GameButton(int number)
		{
			this.number = number;
			this.isRevealed = false;
			this.isMatched = false;
			this.Text = number.ToString();
			this.ForeColor = Color.Black; // Hide the number initially
		}

		// Polymorphism: override the base method
		public override void UpdateAppearance()
		{
			base.UpdateAppearance();

			if (isMatched)
			{
				this.BackColor = Color.Yellow;
				this.ForeColor = Color.Black;
				this.Enabled = false;
			}
			else if (isRevealed)
			{
				this.BackColor = Color.Yellow;
				this.ForeColor = Color.Black;
			}
			else
			{
				this.BackColor = Color.Black;
				this.ForeColor = Color.Black; // Hide the number
			}
		}

		public void Reveal()
		{
			isRevealed = true;
			UpdateAppearance();
		}

		public void Hide()
		{
			isRevealed = false;
			UpdateAppearance();
		}

		public void SetMatched()
		{
			isMatched = true;
			isEnabled = false;
			UpdateAppearance();
		}
	}
}