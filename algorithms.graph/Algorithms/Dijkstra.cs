namespace algorithms.graph.Algorithms;

public static class DijkstraShortestPath
{
    public static IReadOnlyList<long> Dijkstra<T>(this Graph<T> source, Vertice<T> start)
    {
        if (source.GraphType != GraphType.WeighedDirected && source.GraphType != GraphType.WeighedUndirected)
        {
            throw new ArgumentException($"Invalid graph type: {source.GraphType}");
        }

        var vertices = source.Vertices.Keys.ToList();
        var verticesById = vertices.ToDictionary(v => v.Id);

        if (!verticesById.ContainsKey(start.Id))
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        int voiceCount = vertices.Count;
        var queue = new PriorityQueue<Vertice<T>, long>();

        var distances = new Dictionary<long, long>(voiceCount);
        foreach (var vertice in vertices)
        {
            distances[vertice.Id] = long.MaxValue;
        }

        distances[start.Id] = 0;
        queue.Enqueue(start, 0);

        while (queue.TryDequeue(out var element, out var distU))
        {
            if (element is null)
            {
                continue;
            }

            if (distU > distances[element.Id])
                continue;

            foreach (var edge in source.GetEdges(element))
            {
                if (!verticesById.TryGetValue(edge.ToVerticeId, out var v))
                {
                    continue;
                }

                long weight = edge.Weight;

                long newDist = distU + weight;

                if (newDist < distances[v.Id])
                {
                    distances[v.Id] = newDist;
                    queue.Enqueue(v, newDist);
                }
            }
        }
        return vertices
            .OrderBy(v => v.Id)
            .Select(v => distances[v.Id])
            .ToList();
    }
}
