using Godot;
using System;

public class PlayerAnim : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    AnimationPlayer animator;

    private string dir;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animator = this.GetNode(new NodePath("./AnimationPlayer")) as AnimationPlayer;
    }




    public override void _Input(InputEvent inputEvent)
    {
        if(Player.inCombat == true){
                CombatAnim(inputEvent);
        }else{
                NormalAnim(inputEvent);
        }

        
        
    }
    public void SetIdle()
    {
        animator.Play("BobbyIdle");
    }

    private void CombatAnim(InputEvent inputEvent)
    {

        if (inputEvent.IsActionPressed("up"))
        {
         
            animator.Play("BobbyWalk");
        }else if(inputEvent.IsActionReleased("up")){
            animator.Play("BobbyIdle");
        }
    }
    private void NormalAnim(InputEvent inputEvent)
    {
        bool l,r;
        l = Input.IsActionPressed("left");
        r = Input.IsActionPressed("right");
       
        if(!(l ^ r)){
            animator.Play("BobbyIdle");
        }else{
            animator.Play("BobbyWalk");
        }
    }
}
