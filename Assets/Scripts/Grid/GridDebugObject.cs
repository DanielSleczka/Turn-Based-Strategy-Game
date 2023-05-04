using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro gridText;
    private GridObject gridObject;

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        gridText.text = gridObject.ToString();
    }
}
