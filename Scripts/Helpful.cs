using Godot;

class Helpful
{
    public static Transform setRotation(Vector3 axis, float angle, Vector3 origin)
    {
        Transform rot = new Transform(Basis.Identity.Rotated(Vector3.Up, angle),origin);
        rot.origin = origin;

        //GD.Print(rot.basis.GetEuler());
        
          
        return rot;
        
    }
    public static double AngleDifference( float angle1, float angle2 )
    {
        float diff = ( angle2 - angle1 + 180 ) % 360 - 180;
        return diff < -180 ? diff + 360 : diff;
    }
}