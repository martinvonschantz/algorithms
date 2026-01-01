namespace algorithms.graph;

public class Vertice : IComparable<Vertice>
{
    private Int64 _id;
    private string _name;
    
    public Vertice(Int64 id,  string name)
    {
        _id = id;
        _name = name;
    }
    
    public int CompareTo(Vertice vertice) 
    {
        return _id.CompareTo(vertice._id);
    }
}