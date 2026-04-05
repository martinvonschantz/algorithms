namespace algorithms.graph;

public class Vertice<T> : IComparable<Vertice<T>>
{
    private Int64 _id;
    private Func<T> _node;
    
    public Int64 Id
    {
        get => _id;
    }
    
    public Func<T> Node
    {
        get => _node;
    }
    
    public Vertice(Int64 id, Func<T> node)
    {
        _id = id;
        _node = node;
    }
    
    public int CompareTo(Vertice<T>? vertice) 
    {
        if (vertice is null)
            throw new  ArgumentNullException(nameof(vertice));
        return _id.CompareTo(vertice._id);
    }

    public bool Equals(Vertice<T>? vertice)
    {
        if (vertice is null)
            throw new ArgumentNullException(nameof(vertice));
        return _id == vertice.Id;
    } 
    
    public override bool Equals(object obj) => Equals(obj as Vertice<T>); 
    public override int GetHashCode() => Id.GetHashCode();
}