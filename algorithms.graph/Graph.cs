using System.Runtime.InteropServices;

namespace algorithms.graph;

[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class Graph<T>
{
    private GraphType _graphType;
    private Dictionary<Vertice<T>, IList<Edge>> _vertices = new();
    private object _lock = new();
    public Dictionary<Vertice<T>, IList<Edge>> Vertices { get => _vertices; }
    public Int64 Count  => _vertices.Count;
    public GraphType GraphType { get => _graphType; }
    
    public Graph(GraphType graphType)
    {
        _graphType = graphType;
    }

    public IReadOnlyList<Edge> GetEdges(Vertice<T> vertice)
    {
        lock (_lock)
        {
            if (!_vertices.TryGetValue(vertice, out var edges))
            {
                throw new ArgumentOutOfRangeException(nameof(vertice));
            }
            return edges.ToList();
        }
    }

    public Vertice<T> GetVerticeById(Int64 id)
    {
        lock (_lock)
        {
            return _vertices.Single(x => x.Key.Id == id).Key;
        }
    }

    public Graph<T> AddVertice(Vertice<T> vertice)
    {
        lock(_lock)
        {
            _vertices.TryAdd(vertice, new List<Edge>());
        }
        return this;
    }

    public Graph<T> RemoveVertice(Vertice<T> vertice)
    {
        lock (_lock)
        {
            _vertices.Remove(vertice);
        }
        return this;
    }

    public Graph<T> AddEdge(Vertice<T> verticeFrom, Vertice<T> verticeTo, Int64? weight = null)
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
                    if (!_vertices.ContainsKey(verticeFrom))
                        throw new ArgumentOutOfRangeException(nameof(verticeFrom));
                    if (!_vertices.Any(x => x.Key == verticeTo))
                        throw new ArgumentOutOfRangeException();
                    _vertices.Single(x => x.Key == verticeFrom).Value.Add(new Edge(verticeTo.Id, weight));
                    break;
                case GraphType.Undirected:
                case GraphType.WeighedUndirected:
                    if (!_vertices.ContainsKey(verticeFrom))
                        throw new ArgumentOutOfRangeException(nameof(verticeFrom));
                    if (!_vertices.ContainsKey(verticeTo))
                        throw new ArgumentOutOfRangeException(nameof(verticeTo));
                    _vertices.Single(x => x.Key == verticeFrom).Value.Add(new Edge(verticeTo.Id, weight));
                    _vertices.Single(x => x.Key == verticeTo).Value.Add(new Edge(verticeFrom.Id, weight));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return this;
    }

    public Graph<T> RemoveEdge(Vertice<T> verticeFrom, Vertice<T> verticeTo)
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
                    vertice1.Value.Remove(vertice1.Value.Single(x => x.ToVerticeId == vertice2.Key.Id));
                    vertice2.Value.Remove(vertice2.Value.Single(x => x.ToVerticeId == vertice1.Key.Id));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return this;
    }
}
