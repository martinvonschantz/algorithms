// using algorithms.graph;
// using algorithms.graph.Extensions;
//
// Graph graph = new Graph(GraphType.Undirected)
//     .AddVertice(new Vertice(1, "node1"))
//     .AddVertice(new Vertice(2, "node2"))
//     .AddVertice(new Vertice(3, "node3"))
//     .AddVertice(new Vertice(4, "node4"));
//
// graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(2));
// graph.AddEdge(graph.GetVerticeById(1), graph.GetVerticeById(4));
// graph.AddEdge(graph.GetVerticeById(2), graph.GetVerticeById(3));
//
//
//
// Console.WriteLine(graph.Print);
// Console.ReadLine();

using algorithms.graph;
using NUnit.Framework;

[NUnit.Framework.TestFixture]
public class UndirectedGraphTests
{
    [TestCase]
    public static void CreateUndirectedGraph()
    {
        Graph<int> graph = new Graph<int>(GraphType.Undirected)
            .AddVertice(new Vertice(1, () => { return 1; }))
            .AddVertice(new Vertice(2, () => { return 1; }))
            .AddVertice(new Vertice(3, () => { return 1; }))
            .AddVertice(new Vertice(4, () => { return 1; }));
    }
}

