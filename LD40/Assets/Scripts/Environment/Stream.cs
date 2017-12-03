using System;
using System.Collections;
using System.Collections.Generic;
using BezierSplineTools;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stream : MonoBehaviour
{
    public BezierSpline spline;
    public float Step = .05f;
    public GameObject StreamZone;

    public GameObject[] SideIslands;
    public GameObject[] MidIslands;
    public Vector2 MinMaxRiverWidth = new Vector2(250, 350);
    public Vector2 MinMaxIslandStepPerSpline = new Vector2(0.1f, 0.2f);
    public Vector2 MinMaxIslandScale = new Vector2(0.9f, 1.1f);
    public Vector2 MinMaxIslandRotation = new Vector2(-30f, 30f);


    public Vector2 BankCenterJitter = new Vector2(-5f, 5f);
    public float WaterLevel = 1;

    
    public static float WATER_LEVEL = 1;

    
    private void Awake()
    {
        WATER_LEVEL = WaterLevel;
    }

    [ContextMenu("GenerateBanks")]
    public void GenerateBanks()
    {
        
        GameObject islands = new GameObject("islands");
        islands.transform.parent = transform;

        var bankIndex = 0;
        
        float i = RandomFromV2(MinMaxIslandStepPerSpline);
        while (i <= 1)
        {
            for (int j = -1; j <=1; j+=2)
            {
                var center = spline.GetPoint(i);
                Vector3 direction = spline.GetDirection(i);
                var cross = new Vector3(-direction.z, 0, direction.x).normalized;
                
                Debug.DrawRay(center, cross * 20, Color.green, 20);
                
                var bankPos = center + direction * RandomFromV2(BankCenterJitter)
                                  + cross * RandomFromV2(MinMaxRiverWidth) * j;


                bankPos.y = WATER_LEVEL;
                
                bankIndex =  (bankIndex + 1) % MidIslands.Length;
                var island = GameObject.Instantiate(MidIslands[bankIndex]);
                island.transform.position = bankPos;
                island.transform.localScale *= RandomFromV2(MinMaxIslandScale);
                island.transform.parent = islands.transform;
                var r = island.transform.rotation.eulerAngles;
                island.transform.rotation = Quaternion.Euler(r.x, r.y + RandomFromV2(MinMaxIslandRotation), r.z);
            }
            
            
            i += RandomFromV2(MinMaxIslandStepPerSpline);
        }
    }

    public float RandomFromV2(Vector2 v)
    {
        return Random.Range(v.x, v.y);
    }
    
    
    [ContextMenu("GenerateStreamZones")]
    public void GenerateStreamZones()
    {

        GameObject zones = new GameObject("zones");
        zones.transform.parent = transform;
        
        for (float i = 0.001f; i <= 1; i+= Step)
        {
            var go = GameObject.Instantiate(StreamZone);
            go.transform.position = spline.GetPoint(i);

            //go.transform.position = spline.GetDirection(i);

            Vector3 direction = spline.GetDirection(i);

            direction.y = 0;

            go.transform.forward = spline.GetDirection(i);
            go.transform.parent = zones.transform;

            var stream = go.GetComponent<StreamZone>();
            stream.Direction = new Vector3(direction.x, 0, direction.z);
        }
        //Curve
    }


	// Update is called once per frame
	void Update () {
		
	}
}
