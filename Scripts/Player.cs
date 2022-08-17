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

    Node playerModel;


    [Export]
    public NodePath cameraAnimPlayerPath;
    private Node cameraAnimPlayer;

    [Signal]
    delegate void CombatModeChanged(bool started);
    

    

    //2d movement sysvar:
    private float dir = 0;
    private Vector3 movement = Vector3.Forward;
    
    public static bool inCombat = true; //do not set this value manually
   

    Vector2 playerAngle = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.CanSleep = false;
        Input.MouseMode = Input.MouseModeEnum.Captured;
        playerModel = this.GetNode(new NodePath("./bobby"));
        cameraAnimPlayer = this.GetNode(new NodePath(cameraAnimPlayerPath)) as AnimationPlayer;

        
        

    }
    



    public override void _IntegrateForces (PhysicsDirectBodyState state  )
    {

       
       if(inCombat == true){
            combatMovement(state);
       }else{
            normalMovement(state);
       }

        //this.Rotate(new Vector3(0,1,0), 10);
        //float turn = -mouseSpeed.x * mouseSensitivity;
        
    
        
    }
    public override void _Input(InputEvent inputEvent)
    {
        
        if (inputEvent is InputEventMouseMotion mouseEvent && inCombat)
        {
            playerAngle += Input.GetLastMouseSpeed(); 
        }
        if(inputEvent.IsActionPressed("down"))
        {
            SetInCombat(!inCombat);
        }
    }

    public void combatMovement(PhysicsDirectBodyState state)
    {
        float deltaTime = state.Step;

        Transform rot = state.Transform.Rotated(Vector3.Up,  (-mouseSensitivity/100)*playerAngle.x*deltaTime);
        rot.origin = state.Transform.origin;   // for some reason transform.rotated was affecting my pos so i put this here
        state.Transform =rot;
        
        playerAngle.x = 0;

        if(Input.IsActionPressed("up")){

            state.Transform = state.Transform.Translated(Vector3.Forward * deltaTime * speed);
        }
    }

    public void normalMovement(PhysicsDirectBodyState state)
    {
        float deltaTime = state.Step;
        float oldDir = dir;
        
    
        
        
        bool l,r;
        l = Input.IsActionPressed("left");
        r = Input.IsActionPressed("right");
        
        if(l^r){
            if(l){
                dir = (float)Math.PI; // radian 180
            }else if(r)
            {
                dir =  0;
            }  
          
            state.Transform = state.Transform.Translated(Vector3.Forward * deltaTime * speed);            
        }
        Transform rot = new Transform(Basis.Identity.Rotated(Vector3.Up,dir),Vector3.Zero);
        rot.origin = state.Transform.origin;   // for some reason transform.rotated was affecting my pos so i put this here
        state.Transform =rot;
        
    }

   



    public void SetInCombat(bool value)
    {
        if(inCombat == value)
        {
            GD.Print("already in that combat mode");
            return;
        }
    
        
        inCombat = value;

        // anims, world gen, sfx, movement system
        //anims
        if(inCombat == false){
            float snapAngle = Math.Abs(Helpful.AngleDifference(RotationDegrees.y,0)) > 90? Mathf.Pi:0f;
            GD.Print(Helpful.AngleDifference(Rotation.y,0));
            dir = snapAngle;

        }
        
        ((PlayerAnim)playerModel).SetIdle();
        EmitSignal("CombatModeChanged", value);

    }

    private async void snap2D()
    {
        //RotationDegrees = new Vector3(0,90,0);
        await ToSignal(GetTree().CreateTimer(1.0f),"timeout");
        
    }

    
}
