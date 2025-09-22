using Godot;
using System;

namespace Empty.Scripts.Levels
{
	public partial class MainMenu : Control
	{
		private void OnPlayButtonPressed()
		{
			GetTree().ChangeSceneToFile("res://Scenes/Levels/game.tscn");
		}
		private void OnQuitButtonPressed()
		{
			GetTree().Quit();
		}
	}
}
