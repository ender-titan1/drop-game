using Godot;
using System;

/// <summary>
///   This is a default character movement script provided by godot, it's pretty good so i'll leave it
///   but modify it a little 
///		- Szymon
/// </summary>

///<edit_1>
///    I will try to implement w s d a keys into the program
///    PS. why are we writing this in Html format
///    Daniil
///</edit_1>

public partial class Player : CharacterBody3D
{
	[Export]
	public float Speed = 8.0f;

	[Export]
	public float JumpVelocity = 6f;

	[Export]
	public SpringArm3D CameraSpringArm;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// do we need the vector_2 inputs
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		direction = direction.Rotated(Vector3.Up, CameraSpringArm.Rotation.Y).Normalized();

		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
