namespace algorithms.graph;

public class Edge
{
    protected Int64 _from;
    protected Int64 _to;
    protected Int64? _weight;

    public Edge(Int64 from, Int64 to, Int64? weight = null)
    {
        _from = from;
        _to = to;
        _weight = weight;
    }
}