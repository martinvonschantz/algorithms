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

    public (Vertice, IList<Edge>) GetVerticeById(Int64 id)
    {
        lock (_lock)
        {
            var vertice = _vertices.Single(x => x.Key.Id == id);
            return new(vertice.Key, vertice.Value);
        }
    }

    public Graph AddVertice(Vertice vertice)
    {
        lock(_lock)
        {
            _vertices.TryAdd(vertice, new List<Edge>());
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

    public Graph AddEdge(Vertice verticeFrom, Vertice verticeTo, Int64? weight = null)
    {
        lock (_lock)
        {
            if (_graphType == GraphType.Directed || _graphType == GraphType.Undirected)
            {
                weight = 0;
            }

            switch (_graphType)
            {
                case GraphType.Directed:
                case GraphType.WeighedDirected:
                    if (!_vertices.Any(x => x.Key == verticeTo))
                        throw new ArgumentOutOfRangeException();
                    _vertices.Single(x => x.Key == verticeFrom).Value.Add(new Edge(verticeTo.Id, weight));
                    break;
                case GraphType.Undirected:
                case GraphType.WeighedUndirected:
                    _vertices.Single(x => x.Key == verticeFrom).Value.Add(new Edge(verticeTo.Id, weight));
                    _vertices.Single(x => x.Key == verticeTo).Value.Add(new Edge(verticeFrom.Id, weight));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return this;
    }

    public Graph RemoveEdge(Vertice vertice, Edge edge)
    {
        lock (_lock)
        {
            
        }
        return this;
    }
}