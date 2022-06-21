using Godot;

public abstract class KinematicCharacter : KinematicBody
{
  [Export(PropertyHint.Range, "1,100,1")]
  public int GroundFriction = 50;
  [Export]
  public Vector3 Gravity = Vector3.Down * 9.8f;
  [Export]
  public Vector2 InputVector;
  [Export]
  public bool WalkOnAir;
  public Vector3 Velocity;

  protected virtual void CharacterProcess(float delta) { }

  public override void _PhysicsProcess(float delta)
  {
    CharacterProcess(delta);

    if (IsOnFloor())
    {
      Velocity.x = Mathf.Lerp(Velocity.x, InputVector.x, (float)GroundFriction / 100);
      Velocity.z = Mathf.Lerp(Velocity.z, InputVector.y, (float)GroundFriction / 100);
    }
    else if (WalkOnAir)
    {
      Velocity.x = Mathf.Lerp(Velocity.x, InputVector.x, (float)GroundFriction / 200);
      Velocity.z = Mathf.Lerp(Velocity.z, InputVector.y, (float)GroundFriction / 200);
    }

    Velocity += Gravity * delta;

    Velocity = MoveAndSlide(Velocity, Vector3.Up);
  }
}
