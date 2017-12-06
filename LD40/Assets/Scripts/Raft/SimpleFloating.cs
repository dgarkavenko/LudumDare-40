using UnityEngine;

public class SimpleFloating : Floating
{
    public float SinPeriod = 2;
    public float SinLn = .4f;
    public bool Drowned;
   
    public override void Update()
    {
        base.Update();
        var s = FloatDirection * StreamPower;
         var targetPosition = rb.transform.position;
        targetPosition.y = waterLevel + Mathf.Sin(Time.time * SinPeriod) * SinLn;
        rb.MovePosition(targetPosition + s * Time.fixedDeltaTime);

        if (Drowned)
        {
            waterLevel -= Time.deltaTime * 1;
            if(waterLevel <= -5)
                GameObject.Destroy(Controller.gameObject);
        }
    }

    public override void Drown()
    {
        GetComponent<Collider>().enabled = false;
        StreamPower = 0;
        Drowned = true;
        base.Drown();
    }
}
