using Godot;
using System;
using System.Collections.Generic;

public class Ragdoll : Skeleton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    Godot.Collections.Array physBones;// = new Godot.Collections.Array();// = new List<string>();
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //GetBone
        physBones = new Godot.Collections.Array();
        GD.Print(physBones);
        string[] a = new string[] {"spine100","hello"};
        PhysicalBonesStartSimulation(new Godot.Collections.Array(a));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
