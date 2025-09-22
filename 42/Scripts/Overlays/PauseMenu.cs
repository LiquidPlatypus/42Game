using Godot;
using System;

namespace Empty.Scripts.Overlays
{
	public partial class PauseMenu : CanvasLayer
	{
		public override void _Ready()
		{
			ProcessMode = ProcessModeEnum.Always;
			Hide();
		}

		private void OnResumePressed()
		{
			Hide();
			GetTree().Paused = false;
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}

		private void OnReturnMenuPressed()
		{
			GetTree().ChangeSceneToFile("res://Scenes/Levels/main_menu.tscn");
		}
	}
}