using algorithms.graph;

Graph graph = new Graph(GraphType.Undirected);

var myGraph = graph.AddVertice(new Vertice(0, "Node1"))
    .AddVertice(new Vertice(1, "Node2"), new List<Edge>() { new Edge(2, 1) })
    .AddVertice(new Vertice(3, "Node3"), new List<Edge>() { new Edge(3, 1) });



Console.WriteLine(myGraph.ToString());
Console.ReadLine();