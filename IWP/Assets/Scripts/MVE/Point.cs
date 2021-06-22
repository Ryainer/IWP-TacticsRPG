using System.Collections;
using System;
using UnityEngine;
[System.Serializable]
public struct Point : IEquatable<Point>
{
    public int x;
    public int y;
    //constructor to give x and y values
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    //relavant operator overloads here
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.y - b.y);
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }
    //checks if specified object is equal to the current object
    public override bool Equals(object obj)
    {
        if(obj is Point)
        {
            //if obj is point, store values in a variable
            Point p = (Point)obj;
            //return back whether its equal to current x and y position it's comparing to
            return x == p.x && y == p.y;
        }

        return false;
    }

    public bool Equals(Point p)
    {
        return x == p.x && y == p.y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }
}
