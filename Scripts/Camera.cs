using Godot;
using System;

public class Camera : Godot.Camera
{

    [Export]
    public NodePath cameraTargetPath;
    public Spatial cameraTarget;

    [Export]
    Animation CameraAnim;
    
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    float _t = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cameraTarget = this.GetNode(new NodePath(cameraTargetPath)) as Spatial;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
        
        this.GlobalTransform = GlobalTransform.InterpolateWith(cameraTarget.GlobalTransform, .1f);
     
    }
    public void ChangeCamAnim(int dir)
    {
        int track = CameraAnim.FindTrack("CameraOffset:translation");
        Vector3 trans = (Vector3)CameraAnim.TrackGetKeyValue(track,1);
        trans.x = Math.Abs(trans.x) * -dir;
       
        CameraAnim.TrackSetKeyValue(track,1, trans);
        
        int track1 = CameraAnim.FindTrack("CameraOffset:rotation_degrees");
        Vector3 a1 = (Vector3)CameraAnim.TrackGetKeyValue(track1, 1);
        a1.y = dir == 1? -90:90;
        CameraAnim.TrackSetKeyValue(track1,1, a1);
        
    }
    
}
