using System.Runtime.InteropServices;

namespace algorithms.graph;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class Graph
{
    private GraphType _graphType;
    private Dictionary<Vertice, IList<Edge>> _vertices = new();
    private object _lock = new();
    public Dictionary<Vertice, IList<Edge>> Vertices { get => _vertices; }
    public Int64 Count  => _vertices.Count;
    public GraphType GraphType { get => _graphType; }
    
    public Graph(GraphType graphType)
    {
        _graphType = graphType;
    }

    public Vertice GetVerticeById(Int64 id)
    {
        lock (_lock)
        {
            return _vertices.Single(x => x.Key.Id == id).Key;
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

    public Graph RemoveEdge(Vertice verticeFrom, Vertice verticeTo)
    {
        lock (_lock)
        {
            switch (_graphType)
            {
                case GraphType.Directed:
                case GraphType.WeighedDirected:
                    var vertice = _vertices.Single(x => x.Key == verticeFrom);
                    vertice.Value.Remove(vertice.Value.Single(x => x.ToVerticeId == verticeTo.Id));
                    break;
                case GraphType.Undirected:
                case GraphType.WeighedUndirected:
                    var vertice1 = _vertices.Single(x => x.Key == verticeFrom);
                    var vertice2 =  _vertices.Single(x => x.Key == verticeTo);
                    vertice1.Value.Remove(vertice1.Value.Single(x => x.ToVerticeId == vertice1.Key.Id));
                    vertice2.Value.Remove(vertice2.Value.Single(x => x.ToVerticeId == vertice2.Key.Id));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return this;
    }
}