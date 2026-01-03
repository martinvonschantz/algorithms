namespace algorithms.graph;

public class Edge
{
    private Int64 _to;
    public Int64 To
    {
        get => _to;
        set => _to = value;
    }
    private Int64? _weight;
    public Int64 Weight
    {
        get => _weight ?? 0;
        set => _weight = value;
    }

    public Edge(Int64 to, Int64? weight = null)
    {
        _to = to;
        _weight = weight;
    }
}