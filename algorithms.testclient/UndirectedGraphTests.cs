using algorithms.graph;
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

