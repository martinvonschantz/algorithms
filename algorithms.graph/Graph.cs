using System.Runtime.InteropServices;

namespace algorithms.graph;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class Graph
{
    private GraphType _graphType;
    private Dictionary<Vertice, IList<Edge>> _vertices = new();
    public Dictionary<Vertice, IList<Edge>> Vertices
    {
        get => _vertices;
    }
    
    public Graph(GraphType graphType)
    {
        _graphType = graphType;
    }

    public Graph AddVertice(Vertice vertice, IList<Edge>? edges = null)
    {
        if (edges == null)
            _vertices.TryAdd(vertice, new List<Edge>());
        else
            _vertices.TryAdd(vertice, edges);
        
        return this;
    }

    public Graph RemoveVertice(Vertice vertice)
    {
        _vertices.Remove(vertice);
        return this;
    }

    public Graph AddEdge(Vertice vertice, Edge edge)
    {
        _vertices.SingleOrDefault(x => x.Key == vertice).Value.Add(edge);
        return this;
    }

    public Graph RemoveEdge(Vertice vertice, Edge edge)
    {
        _vertices.SingleOrDefault(x => x.Key == vertice).Value.Remove(edge);
        return this;
    }
}