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

public partial class RollSound : AudioStreamPlayer
{
	
	game mainGame;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mainGame = GetNode<game>("../Becher");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check if the audio is not playing
		if (!Playing && mainGame.currentlyRolling)
		{
			// Restart the audio playback
			Play();
		} else if (!(mainGame.currentlyRolling)){
			Stop();
		}
	}
}
