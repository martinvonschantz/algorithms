using algorithms.graph;
using algorithms.graph.Algorithms;
using NUnit.Framework;

namespace algorithms.testclient;

[TestFixture]
public class UndirectedGraphTests
{
    [Test]
    public void CreateUndirectedGraph()
    {
        Graph<int> graph = new Graph<int>(GraphType.Undirected)
            .AddVertice(new Vertice<int>(1, () => 1))
            .AddVertice(new Vertice<int>(2, () => 1))
            .AddVertice(new Vertice<int>(3, () => 5))
            .AddVertice(new Vertice<int>(4, () => 3));
        
        graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(2));
        graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(4));
        graph.AddEdge(graph.GetVerticeById(2), graph.GetVerticeById(3));

        Assert.That(graph.Count, Is.EqualTo(4));
    }

    [Test]
    public void CreateUndirectedGraphWithEdge()
    {
        
    }
}

[TestFixture]
public class DijkstraShortestPathTests
{
    [Test]
    public void Dijkstra_WeighedUndirected_ReturnsExpectedDistances()
    {
        Graph<int> graph = new Graph<int>(GraphType.WeighedUndirected)
            .AddVertice(new Vertice<int>(1, () => 1))
            .AddVertice(new Vertice<int>(2, () => 2))
            .AddVertice(new Vertice<int>(3, () => 3))
            .AddVertice(new Vertice<int>(4, () => 4))
            .AddVertice(new Vertice<int>(5, () => 5));

        graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(2), 2);
        graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(3), 5);
        graph.AddEdge(graph.GetVerticeById(2), graph.GetVerticeById(3), 1);
        graph.AddEdge(graph.GetVerticeById(2), graph.GetVerticeById(4), 2);
        graph.AddEdge(graph.GetVerticeById(3), graph.GetVerticeById(5), 3);
        graph.AddEdge(graph.GetVerticeById(4), graph.GetVerticeById(5), 1);

        var result = graph.Dijkstra(graph.GetVerticeById(1));

        Assert.That(result, Is.EqualTo(new long[] { 0, 2, 3, 4, 5 }));
    }

    [Test]
    public void Dijkstra_WeighedDirected_RespectsDirectionAndUnreachableVertices()
    {
        Graph<int> graph = new Graph<int>(GraphType.WeighedDirected)
            .AddVertice(new Vertice<int>(1, () => 1))
            .AddVertice(new Vertice<int>(2, () => 2))
            .AddVertice(new Vertice<int>(3, () => 3))
            .AddVertice(new Vertice<int>(4, () => 4));

        graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(2), 1);
        graph.AddEdge(graph.GetVerticeById(2), graph.GetVerticeById(3), 1);
        graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(3), 5);
        graph.AddEdge(graph.GetVerticeById(3), graph.GetVerticeById(1), 10);

        var result = graph.Dijkstra(graph.GetVerticeById(1));

        Assert.That(result, Is.EqualTo(new long[] { 0, 1, 2, long.MaxValue }));
    }

    [Test]
    public void Dijkstra_InvalidGraphType_Throws()
    {
        Graph<int> graph = new Graph<int>(GraphType.Undirected)
            .AddVertice(new Vertice<int>(1, () => 1));

        var start = graph.GetVerticeById(1);

        Assert.Throws<ArgumentException>(() => graph.Dijkstra(start));
    }

    [Test]
    public void Dijkstra_StartVerticeMissing_Throws()
    {
        Graph<int> graph = new Graph<int>(GraphType.WeighedUndirected)
            .AddVertice(new Vertice<int>(1, () => 1));

        var missing = new Vertice<int>(99, () => 99);

        Assert.Throws<ArgumentOutOfRangeException>(() => graph.Dijkstra(missing));
    }
}
