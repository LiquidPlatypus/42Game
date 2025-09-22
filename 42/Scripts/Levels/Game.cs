using Godot;
using System;

namespace Empty.Scripts.Overlays {
	public partial class Game : Node
	{
		private CanvasLayer _pauseMenu;

		public override void _Ready()
		{
			_pauseMenu = GetNode<CanvasLayer>("MenuPause");
		}

		public override void _UnhandledInput(InputEvent @event)
		{
			if (@event.IsActionPressed("ui_cancel") || @event.IsActionPressed("pause"))
				TogglePause();
		}

		private void TogglePause()
		{
			// if (_pauseMenu.Visible)
			// {
			// 	_pauseMenu.Hide();
			// 	GetTree().Paused = false;
			// 	Input.MouseMode = Input.MouseModeEnum.Captured;
			// }
			// else
			// {
			// 	_pauseMenu.Show();
			// 	GetTree().Paused = true;
			// 	Input.MouseMode = Input.MouseModeEnum.Visible;
			// }
			_pauseMenu.Show();
			GetTree().Paused = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

	}
}
