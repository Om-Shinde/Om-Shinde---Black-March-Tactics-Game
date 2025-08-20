public class PathNode
{
    public int x;
    public int y;
    public bool walkable;
    public PathNode parent;


    public PathNode(int x, int y, bool walkable)
    {
        this.x = x;
        this.y = y;
        this.walkable = walkable;
    }
}