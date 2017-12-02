using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningCat : FloatingController {
    public override void Start()
    {
        base.Start();
        Model.SteerPower /= Random.Range(1f, 5f);
    }
}
