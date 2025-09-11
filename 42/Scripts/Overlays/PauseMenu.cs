using Godot;
using System;

namespace Empty.Scripts.PauseMenu
{
	public partial class PauseMenu : Control
	{
		public override void _UnhandledInput(InputEvent @event)
		{
			if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
				if (keyEvent.Keycode == Key.Escape)
					GetTree().Quit();
		}
	}
}