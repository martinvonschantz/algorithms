namespace algorithms.graph;

public class Vertice : IComparable<Vertice>
{
    private Int64 _id;
    public Int64 Id
    {
        get => _id;
    }
    private string _name;
    public string Name
    {
        get => _name;
    }
    
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