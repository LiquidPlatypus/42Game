using Godot;
using System;

namespace Empty.Scripts.Player
{
	public partial class Player : CharacterBody3D
	{
		[Export] public int Speed { get; set; } = 14;
		[Export] public int FallAcceleration {get; set;} = 75;
		[Export] public int JumpImpulse {get; set;} = 20;
		[Export] public float MouseSensitivity = 0.1f;
		[Export] public float PitchLimit = 60.0f;

		private Vector3 _targetVelocity = Vector3.Zero;
		private Vector2 _mouseDelta = new Vector2();
		private float _pitch = 0f;
		private Node3D _head;
		private Camera3D _cameraFP;
		private Camera3D _cameraTP;
		private bool _isThirdPerson = false;

		public override void _Ready()
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
			_head = GetNode<Node3D>("Head");
			_cameraFP = GetNode<Camera3D>("Head/CameraFP");
			_cameraTP = GetNode<Camera3D>("Head/CameraTP");

			_cameraFP.Current = true;
			_cameraTP.Current = false;
		}

		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventMouseMotion mouseMotion)
				_mouseDelta = mouseMotion.Relative;
		}
		public override void _Process(double delta)
		{
			if (_head == null)
				return;
			RotationDegrees -= new Vector3(0, _mouseDelta.X, 0) * MouseSensitivity;

			_pitch = Mathf.Clamp(_pitch - _mouseDelta.Y * MouseSensitivity, -PitchLimit, PitchLimit);
			_head.RotationDegrees = new Vector3(_pitch, 0, 0);

			_mouseDelta = Vector2.Zero;
		}

		public override void _UnhandledInput(InputEvent @event)
		{
			if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
			{
				if (keyEvent.Keycode == Key.F5)
					ToggleCamera();
			}
		}
		private void ToggleCamera()
		{
			_isThirdPerson = !_isThirdPerson;
			_cameraFP.Current = !_isThirdPerson;
			_cameraTP.Current = _isThirdPerson;
		}

		public override void _PhysicsProcess(double delta)
		{
			var direction = Vector3.Zero;

			if (Input.IsActionPressed("move_forward"))
				direction.Z -= 1.0f;
			if (Input.IsActionPressed("move_back"))
				direction.Z += 1.0f;
			if (Input.IsActionPressed("move_left"))
				direction.X -= 1.0f;
			if (Input.IsActionPressed("move_right"))
				direction.X += 1.0f;
			if (Input.IsActionPressed("jump"))
				if (direction != Vector3.Zero)
				{
					direction = direction.Normalized();
					GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
				}

			if (direction != Vector3.Zero)
			{
				// Normaliser l’input
				direction = direction.Normalized();

				// Transformer par l’orientation du Head (ou caméra pivot)
				var basis = GetNode<Node3D>("Head").GlobalTransform.Basis;
				direction = (basis * direction).Normalized();

				// Rester au sol (annuler Y)
				direction.Y = 0;
				direction = direction.Normalized();
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
