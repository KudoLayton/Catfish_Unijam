using System;

namespace Polyperfect.Common
{
  [Serializable]
  public class MovementState : AIState
  {
    public float maxStateTime = 40f;
    public float moveSpeed = 3f;
    public float turnSpeed = 120f;
  }
}