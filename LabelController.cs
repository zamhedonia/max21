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

public partial class LabelController : Node2D
{
	public float textSpeed = 1f;
	public int language = 0;
	
	public Label deutsch;
	public Label english;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		deutsch = GetNode<Label>("../LabelController/LabelDeutsch");
		english = GetNode<Label>("../LabelController/LabelEnglish");
		
		deutsch.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("textUp") || Input.IsActionJustPressed("textUp")){
			this.Position -= new Vector2(0, 10f * textSpeed);
		}
		if (Input.IsActionPressed("textDown") || Input.IsActionJustPressed("textDown")){
			this.Position += new Vector2(0, 10f * textSpeed);
		}
		if (Input.IsActionJustPressed("Info")){
			this.Position = new Vector2(0, 0);
		}
		if (Input.IsActionJustPressed("languageSwitch")){
			language++;
			language = language % 2;
			GD.Print(language);
			switch(language){
				case 1:
					//Deutsch
					deutsch.Visible = true;
					english.Visible = false;
					break;
				case 0:
					//Englisch
					deutsch.Visible = false;
					english.Visible = true;
					break;
				default:
					break;
			}
		}
	}
}
