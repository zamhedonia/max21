/*
 * Max 21 - Maexchen - Multiplayer Dice Game Simulation
 *
 * Copyright (c) 2023 zamhedonia
 * see github.com/zamhedonia
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */


using Godot;
using System;

public partial class Wuerfel : RigidBody2D
{
	// Initialize Variables
	public int face = 0;
	Random random = new Random();
	Texture FaceTexture;
	Sprite2D sprite;

	public void SetVisibility(bool visible)
	{
		sprite.Visible = visible; 
	}

	public void ChangeFace()
	{
		face = random.Next(1, 7);
		GD.Print("Face = " + face);

		// Adjust the file path to include the "images" folder
		string imagePath = $"res://images/wuerfel{face}.png";

		// Load the texture using ResourceLoader
		FaceTexture = ResourceLoader.Load<Texture>(imagePath) as Texture;

		// Set the loaded texture to the Sprite2D
		if (FaceTexture != null)
		{
			// Explicitly cast to Texture2D
			sprite.Texture = (Texture2D)FaceTexture;
		}
		else
		{
			GD.Print("Failed to load texture for face value: " + face);
		}
	}
	
	public void RedropDice()
	{
		GD.Print("Redropping Dice");
		this.ApplyCentralImpulse(new Vector2(5, -3) * 500);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Assuming "Sprite" is the name of your Sprite2D child node
		sprite = GetNode<Sprite2D>("Sprite");

		if (sprite != null)
		{
			ChangeFace();
		}
		else
		{
			GD.Print("Child node 'Sprite' not found.");
		}
		
		// Set a random rotation for the RigidBody2D
		float randomRotation = (float)random.NextDouble() * Mathf.Pi * 2; // Random rotation in radians
		Rotation = randomRotation;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// You can add any additional logic here if needed
	}
}
