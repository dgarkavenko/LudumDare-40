using UnityEngine;

public class SimpleFloating : Floating
{
    public float SinPeriod = 2;
    public float SinLn = .4f;
    
   
    public override void Update()
    {
        base.Update();
        var s = FloatDirection * StreamPower;
     var targetPosition = rb.transform.position;
        targetPosition.y = waterLevel + Mathf.Sin(Time.time * SinPeriod) * SinLn;
        rb.MovePosition(targetPosition + s * Time.fixedDeltaTime);
    }

}
