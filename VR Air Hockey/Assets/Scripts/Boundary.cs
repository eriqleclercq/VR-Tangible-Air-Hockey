using UnityEngine;

//Custom struct to make our life a bit easier
struct Boundary
{
    public Vector3 Up, Down, Left, Right;

    public Boundary(Transform up, Transform down, Transform left, Transform right)
    {
        Up = up.position;
        Down = down.position; 
        Left = left.position;
        Right = right.position;
    }
}
