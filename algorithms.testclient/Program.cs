using algorithms.graph;
using algorithms.graph.Extensions;

Graph graph = new Graph(GraphType.Undirected)
    .AddVertice(new Vertice(0, "Node1"))
    .AddVertice(new Vertice(1, "Node2"), new List<Edge>() { new Edge(2) })
    .AddVertice(new Vertice(2, "Node3"), new List<Edge>() { new Edge(3), new Edge(0), new Edge(10) });



Console.WriteLine(graph.Print);
Console.ReadLine();