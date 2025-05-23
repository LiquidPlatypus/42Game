using Godot;
using System;

namespace Empty.Scripts.Player
{
	public partial class Player : CharacterBody3D
	{
		[Export] public int Speed { get; set; } = 14;
		[Export] public int FallAcceleration {get; set;} = 75;
		[Export] public int JumpImpulse {get; set;} = 20;

		private Vector3 _targetVelocity = Vector3.Zero;

		public override void _PhysicsProcess(double delta)
		{
			var direction = Vector3.Zero;

			if (Input.IsActionPressed("move_forward"))
				direction.X += 1.0f;
			if (Input.IsActionPressed("move_back"))
				direction.X -= 1.0f;
			if (Input.IsActionPressed("move_left"))
				direction.Z -= 1.0f;
			if (Input.IsActionPressed("move_right"))
				direction.Z += 1.0f;
			if (Input.IsActionPressed("jump"))
				if (direction != Vector3.Zero)
				{
					direction = direction.Normalized();
					GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
				}

			_targetVelocity.X = direction.X * Speed;
			_targetVelocity.Z = direction.Z * Speed;

			if (!IsOnFloor())
				_targetVelocity.Y -= FallAcceleration * (float)delta;

			if (IsOnFloor() && Input.IsActionPressed("jump"))
				_targetVelocity.Y = JumpImpulse;

			Velocity = _targetVelocity;
			MoveAndSlide();
		}
	}

}
