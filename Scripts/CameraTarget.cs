using Godot;
using System;

public class CameraTarget : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Spatial trackingObject;
    [Export]
    public NodePath PlayerPath;
    private Node player;

    [Export]
    public NodePath cameraPath;
    private Node camera;
    private Node cameraOffset;

 
    public override void _Ready()
    {
        player = this.GetNode(PlayerPath);
        camera = this.GetNode(cameraPath);
        cameraOffset = this.GetNode("CameraOffset");
        trackingObject = (Spatial)player;


  
        player.Connect("CombatModeChanged", this, "FixRot");
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
            if(Player.inCombat == true)
            {
                this.Transform = trackingObject.Transform;
            }else{

                Transform newPos = this.Transform;
                newPos.origin = trackingObject.Transform.origin;
                this.Transform = newPos;
            }
    }




    public void FixRot(bool started)
    {
        if(started){
            int dir = Mathf.RoundToInt(Mathf.Abs(trackingObject.RotationDegrees.y) ) == 180? 1:-1; 
           
            ((Camera)camera).ChangeCamAnim(dir);

        }else{

            //float snapAngle = this.RotationDegrees.AngleTo(Vector3.Zero) < this.RotationDegrees.AngleTo(new Vector3(0,180,0))? 0f : 180f;
            this.RotationDegrees = new Vector3(0,0,0);
            //GD.Print("Rotation fixed");
        }
        if(Player.inCombat == true){
            ((Spatial)cameraOffset).Translation = new Vector3(0,2,3);
            ((Spatial)cameraOffset).RotationDegrees = new Vector3(-17,0,0);
            
            
        }else{
            ((Spatial)cameraOffset).Translation = new Vector3(6,2,0);
            ((Spatial)cameraOffset).RotationDegrees = new Vector3(-17,90,0);
            //snap2D();
           
        }

    } 
    
}
