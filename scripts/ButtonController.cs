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

public partial class ButtonController : CanvasLayer
{
	TouchScreenButton Start;
	TouchScreenButton Roll;
	TouchScreenButton Reroll;
	TouchScreenButton Peek;
	TouchScreenButton Unveil;
	TouchScreenButton Believe;
	TouchScreenButton Info;
	
	public void hideButton(TouchScreenButton buttonName)
	{
		buttonName.Visible = false;
	}
	
	public void unhideButton(TouchScreenButton buttonName)
	{
		buttonName.Visible = true;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Start = GetNode<TouchScreenButton>("Start");
		Roll = GetNode<TouchScreenButton>("Roll");
		Reroll = GetNode<TouchScreenButton>("Reroll");
		Peek = GetNode<TouchScreenButton>("Peek");
		Unveil = GetNode<TouchScreenButton>("Unveil");
		Info = GetNode<TouchScreenButton>("Info");
		Believe = GetNode<TouchScreenButton>("Believe");
		
		hideButton(Roll);
		hideButton(Believe);
		hideButton(Unveil);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
