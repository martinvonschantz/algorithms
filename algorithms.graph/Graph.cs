using System.Runtime.InteropServices;

namespace algorithms.graph;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class Graph
{
    private GraphType _graphType;
    private Dictionary<Vertice, IList<Edge>> _vertices = new();
    private object _lock = new();
    public Dictionary<Vertice, IList<Edge>> Vertices
    {
        get => _vertices;
    }
    public Int64 Count  => _vertices.Count;
    
    public Graph(GraphType graphType)
    {
        _graphType = graphType;
    }

    public Graph AddVertice(Vertice vertice, IList<Edge>? edges = null)
    {
        lock(_lock)
        { 
            _vertices.TryAdd(vertice, edges ?? new List<Edge>());
        }
        return this;
    }

    public Graph RemoveVertice(Vertice vertice)
    {
        lock (_lock)
        {
            _vertices.Remove(vertice);
        }
        return this;
    }

    public Graph AddEdge(Vertice vertice, Edge edge)
    {
        lock (_lock)
        {
            if (_vertices.ContainsKey(vertice))
                if (!_vertices.SingleOrDefault(x => x.Key == vertice).Value.Contains(edge))
                    _vertices.SingleOrDefault(x => x.Key == vertice).Value.Add(edge);
        }
        return this;
    }

    public Graph RemoveEdge(Vertice vertice, Edge edge)
    {
        lock (_lock)
        {
            if (_vertices.ContainsKey(vertice))
                _vertices.SingleOrDefault(x => x.Key == vertice).Value.Remove(edge);
        }
        return this;
    }
}