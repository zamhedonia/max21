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

public partial class Zahlenwert : Label
{
	bool ShowZahlenwert = true;
	
	Wuerfel w1;
	Wuerfel w2;
	int faceW1;
	int faceW2;
	
	public void ZahlenwertUpdate(){
		faceW1 = w1.face;
		faceW2 = w2.face;
		if (faceW2 > faceW1){
			this.Text = faceW2 + "" + faceW1;
		} else {
			this.Text = faceW1 + "" + faceW2;
		}
		GD.Print("Zahlenwert updated:" + this.Text);
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (ShowZahlenwert == false){
			this.Visible = false;
		}
		w1 = GetNode<Wuerfel>("../Wuerfel");
		w2 = GetNode<Wuerfel>("../Wuerfel2");
		faceW1 = w1.face;
		faceW2 = w2.face;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
