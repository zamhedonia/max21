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

public partial class game : Node2D
{
	// Initialize Variables
	bool firstRound = true;
	bool playingStatus = false;
	bool peeking = false;
	bool currentlyRollingAllowedToRollToggling = false;
	public bool currentlyRolling = false;
	bool currentlyRollingAllowedToRoll = false;
	bool doneRolling = false;
	bool infoShowing = false;
	bool canChoose = false;
	bool believing = false;
	Sprite2D arrow;
	
	float rotationAngle = 0f;
	Wuerfel w1;
	Wuerfel w2;
	Zahlenwert zahlenwert;
	ButtonController buttons;
	CanvasLayer InfoLayer;
	CanvasLayer MobileControls;
	
	TouchScreenButton Start;
	TouchScreenButton Roll;
	TouchScreenButton Reroll;
	TouchScreenButton Peek;
	TouchScreenButton Unveil;
	TouchScreenButton Info;
	TouchScreenButton Believe;
	
	public float becherSpeed = 4f;
	public float becherRollingIntensity = .1f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		w1 = GetNode<Wuerfel>("../Wuerfel");
		w2 = GetNode<Wuerfel>("../Wuerfel2");
		zahlenwert = GetNode<Zahlenwert>("../Zahlenwert");
		buttons = GetNode<ButtonController>("../MobileControls");
		InfoLayer = GetNode<CanvasLayer>("../Info");
		MobileControls = GetNode<CanvasLayer>("../MobileControls");
		
		arrow = GetNode<Sprite2D>("../MobileControls/Arrow");
		Start = GetNode<TouchScreenButton>("../MobileControls/Start");
		Roll = GetNode<TouchScreenButton>("../MobileControls/Roll");
		Reroll = GetNode<TouchScreenButton>("../MobileControls/Reroll");
		Peek = GetNode<TouchScreenButton>("../MobileControls/Peek");
		Unveil = GetNode<TouchScreenButton>("../MobileControls/Unveil");
		Info = GetNode<TouchScreenButton>("../MobileControls/Info");
		Believe = GetNode<TouchScreenButton>("../MobileControls/Believe");
	
		arrow.Visible = false;
		InfoLayer.Visible = false;
		Peek.Visible = false;
	
		GD.Print("Press 'R' to Start");
		GD.Print("Press 'I' to Display Game Rules");
		GD.Print("Press 'X' to Restart");
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Move Up Becher -> Use on NewRound and after opening
		if (currentlyRolling == false && playingStatus == false && peeking == false && Position.Y > 240)
		{
			this.Position -= new Vector2(0, (Position.Y - 300f) * .01f * becherSpeed);
		}

		//Info
		if (Input.IsActionJustPressed("Info")){
			infoShowing = !(infoShowing);
			if (infoShowing){
				GD.Print("Showing Info");
				MobileControls.Visible = false;
				InfoLayer.Visible = true;
			} else {
				GD.Print("Hiding Info");
				MobileControls.Visible = true;
				InfoLayer.Visible = false;
			}
		}
		
		//Restart
		if (Input.IsActionJustPressed("Reset"))
		{
			// Restart the scene
			GetTree().ReloadCurrentScene();
		}
		
		
		//Start
		if (!(playingStatus) && Input.IsActionPressed("Start")){
			GD.Print("Starting Round");
			w1.SetVisibility(true);
			w2.SetVisibility(true);
			playingStatus = true;
			currentlyRollingAllowedToRollToggling = true;
			Start.Visible = false;
			Roll.Visible = true;
		}
		//Roll
		if (Input.IsActionPressed("Roll") && currentlyRollingAllowedToRoll){
			currentlyRollingAllowedToRoll = false;
			currentlyRolling = true;
			GD.Print("Rolling");
		}
		if (currentlyRolling){
			w1.SetVisibility(false);
			w2.SetVisibility(false);
			this.Rotation = Mathf.Sin(rotationAngle);
			rotationAngle += becherRollingIntensity;
		}
		//Animate Stop
		if (!(currentlyRolling) && rotationAngle != 0){
			rotationAngle = this.Rotation;
			if (rotationAngle > 0){
				rotationAngle -= .7f * becherRollingIntensity;
			} else {
				rotationAngle += .7f * becherRollingIntensity;
				}
			if (rotationAngle < .0009f){
				rotationAngle = 0f;
				GD.Print("Animation Stop");
			}
			this.Rotation = rotationAngle;
			GD.Print(rotationAngle);
		}
		//Stopped Rolling
		if (currentlyRolling && !(Input.IsActionPressed("Roll"))){
			w1.ChangeFace();
			w2.ChangeFace();
			zahlenwert.ZahlenwertUpdate();
			currentlyRolling = false;
			currentlyRollingAllowedToRollToggling = false;
			doneRolling = true;
			Roll.Visible = false;
			Peek.Visible = true;
			GD.Print("No Longer Rolling");
			Believe.Visible = true;
			Unveil.Visible = true;
			canChoose = true;
			if (firstRound){
				arrow.Visible = true;
			}
			w1.RedropDice();
			w2.RedropDice();
		}
		
		
		//Peek
		if (Input.IsActionPressed("Peek") && doneRolling){
			if ((!peeking)){
				w1.SetVisibility(true);
				w2.SetVisibility(true);
				peeking = true;
				GD.Print("Peek");
			}
			arrow.Visible = false;
			this.Position -= new Vector2(0, (Position.Y - 600f) * .01f * becherSpeed);
			
		}
		
		if (peeking && !(Input.IsActionPressed("Peek"))){
			peeking = false;
			GD.Print("Stopped Peeking");
		}
		
		
		//Doubt
		if (Input.IsActionPressed("Doubt") && doneRolling && canChoose){
			GD.Print("Doubting");
			w1.SetVisibility(true);
			w2.SetVisibility(true);
			arrow.Visible = false;
			Believe.Visible = false;
			Unveil.Visible = false;
			canChoose = false;
			playingStatus = false;
			doneRolling = false;
			Peek.Visible = false;
			Start.Visible = true;
			Start.TextureNormal = (Texture2D)GD.Load("res://images/Restart_Button.png");
			firstRound = false;
		}
		
		
		//Believe
		if (Input.IsActionPressed("Believe") && doneRolling && canChoose){
			GD.Print("Believing");
			Believe.Visible = false;
			arrow.Visible = false;
			Unveil.Visible = false;
			canChoose = false;
			Peek.Visible = false;
			currentlyRollingAllowedToRoll = true;
			Roll.Visible = true;
			firstRound = false;
		}
		
		
		//Move Becher Down
			if (Position.Y < 960 && !(peeking) && playingStatus)
			{
				this.Position += new Vector2(0, 10f * becherSpeed);
				if (Position.Y >= 960)
				{
					GD.Print("Becher.bodenBerühren.Sound");
					if (!(doneRolling)){
						GD.Print("Allowed To Roll");
						w1.SetVisibility(false);
						w2.SetVisibility(false);
						currentlyRollingAllowedToRollToggling = false;
						currentlyRollingAllowedToRoll = true;
					}
				}
			}
	}
}
