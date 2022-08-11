using Godot;
using System;

public class Player : RigidBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    float speed; 
    [Export]
    float mouseSensitivity;

    Vector2 playerAngle = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.CanSleep = false;
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    



    public override void _IntegrateForces (PhysicsDirectBodyState state  )
    {
        float deltaTime = state.Step;

        Transform rot = state.Transform.Rotated(Vector3.Up,  (-mouseSensitivity/100)*playerAngle.x*deltaTime);
        rot.origin = state.Transform.origin;   // for some reason transform.rotated was affecting my pos so i put this here
        state.Transform =rot;
        
        playerAngle.x = 0;

        if(Input.IsActionPressed("forward")){

            state.Transform = state.Transform.Translated(Vector3.Forward * deltaTime * speed);
        }

        //this.Rotate(new Vector3(0,1,0), 10);
        //float turn = -mouseSpeed.x * mouseSensitivity;
        
    
        
    }


    public override void _Input(InputEvent inputEvent)
    {
        
        if (inputEvent is InputEventMouseMotion mouseEvent)
        {
            playerAngle += Input.GetLastMouseSpeed();
            
        }
    }
}
