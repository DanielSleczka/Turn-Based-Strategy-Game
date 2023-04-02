using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem gridSystem;

    void Start()
    {
        gridSystem = new GridSystem(10, 10, 2f);

        Debug.Log(new GridPosition(4, 6));
    }

    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }

}
