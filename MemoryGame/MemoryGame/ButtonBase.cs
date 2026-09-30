using System;
using System.Windows.Forms;

namespace MemoryGame
{
	// Base class for buttons - demonstrates inheritance
	public abstract class ButtonBase : Button
	{
		// Encapsulation: protected property
		protected bool isEnabled;

		public ButtonBase()
		{
			isEnabled = true;
		}

		// Polymorphism: virtual method to be overridden
		public virtual void UpdateAppearance()
		{
			this.Enabled = isEnabled;
		}
	}
}