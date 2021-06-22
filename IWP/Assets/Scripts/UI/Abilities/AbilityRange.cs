using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int xAxis = 1;
    public int yAxis = int.MaxValue;
    protected UnitTilePos unitTilePos { get { return GetComponent<UnitTilePos>(); } }
    public abstract List<Tiles> tilesinRange(Board board);
}
