namespace algorithms.graph.Algorithms;

public static class DijkstraShortestPath
{
    extension(Graph source)
    {
        public IList<Vertice> Dijkstra(Vertice start)
        {
            if (source.GraphType != GraphType.WeighedDirected)
            {
                throw new ArgumentException($"Invalid graph type: {source.GraphType}");
            }

            var V = source.Vertices.Count();
            var pq = new PriorityQueue<Vertice, Int64>();
            Int64[] distance = new Int64[V];
            for (Int64 i = 0; i < V; i++)
                distance[i] = Int64.MaxValue;   
            
            distance[start.Id] = 0;
            pq.Enqueue(start, 0);

            while (pq.Count > 0)
            {
                 pq.TryDequeue(out Vertice vertice, out Int64 length);
                 if (length > distance[vertice.Id])
                 {
                     
                 }
            }
            
            return new List<Vertice>();
        }
    }

    // static List<int> dijkstra(List<List<int[]>> adj, int src)
    // {
    //     // Process the queue until all reachable vertices are finalized
    //     while (pq.Count > 0)
    //     {
    //         pq.TryDequeue(out int u, out int d);
    //
    //         // If this distance is not the latest shortest one, skip it
    //         if (d > dist[u])
    //             continue;
    //
    //         // Explore all adjacent vertices
    //         foreach (var p in adj[u])
    //         {
    //             int v = p[0];
    //             int w = p[1];
    //
    //             // If we found a shorter path to v through u, update it
    //             if (dist[u] + w < dist[v])
    //             {
    //                 dist[v] = dist[u] + w;
    //                 pq.Enqueue(v, dist[v]);
    //             }
    //         }
    //     }
    //
    //     // Convert result to List for output
    //     List<int> result = new List<int>();
    //     foreach (int d in dist)
    //         result.Add(d);
    //
    //     // Return the final shortest distances from the source
    //     return result;
    // }
}