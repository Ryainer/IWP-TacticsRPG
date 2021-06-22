using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameAbilityRange : AbilityRange
{
    public override List<Tiles> tilesinRange(Board board)
    {
        return board.Search(unitTilePos.tile, expandSearch);
    }

    bool expandSearch(Tiles from, Tiles to)
    {
        if((from.distance + 1) <= xAxis && Mathf.Abs(to.height - unitTilePos.tile.height) <= yAxis)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
