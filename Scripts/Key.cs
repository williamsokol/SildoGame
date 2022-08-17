using Godot;
public class Key
{
    public float value{
        get{return anim.BezierTrackGetKeyValue(track_idx, key_idx);}
        set{anim.BezierTrackSetKeyValue(track_idx, key_idx,value);}
    }
    public Vector2 inHandle{
        get{return anim.BezierTrackGetKeyInHandle(track_idx, key_idx);}
        set{anim.BezierTrackSetKeyInHandle(track_idx, key_idx,value);}
    }
    public Vector2 outHandle{
        get{return anim.BezierTrackGetKeyOutHandle(track_idx, key_idx);}
        set{anim.BezierTrackSetKeyOutHandle(track_idx, key_idx,value);}
    }
    public float time{
        get{return anim.TrackGetKeyTime(track_idx, key_idx);}
        set{anim.TrackSetKeyTime(track_idx, key_idx,value);}
    }
    public bool enabled;
    public NodePath path;
    public int track_idx, key_idx;
    public Animation anim;

    
    public Key(Animation _anim, int _track_idx, int _key_idx)
    {
        
    }
}