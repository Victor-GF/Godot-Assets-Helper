using Godot;
using System;

public class CharacterCamera : Spatial
{
  [Export(PropertyHint.Range, "0.01, 1.0")]
  public float CameraSensitivity = .1f;
  [Export]
  public Vector2 LimitAxis = new Vector2(-85, 85);
  [Export]
  public bool InvertCamera;

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventMouseMotion mouseMotion && Input.GetMouseMode() == Input.MouseMode.Captured)
    {
      // Mouse Motion
      Vector2 motion = mouseMotion.Relative * CameraSensitivity;
      motion *= InvertCamera ? 1 : -1;

      // Rotate Parent
      GetParent<Spatial>().RotateY(Mathf.Deg2Rad(motion.x));

      // Rotate object
      Vector3 camRot = Rotation; camRot.x += Mathf.Deg2Rad(motion.y);
      camRot.x = Mathf.Clamp(camRot.x, Mathf.Deg2Rad(LimitAxis.x), Mathf.Deg2Rad(LimitAxis.y));
      Rotation = camRot;
    }
  }
}
