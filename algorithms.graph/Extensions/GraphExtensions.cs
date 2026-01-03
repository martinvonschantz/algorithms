using System.Text;

namespace algorithms.graph.Extensions;

public static class GraphExtensions
{
    extension(Graph source)
    {
        public string Print
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var keyValuePair in source.Vertices)
                {
                    sb.Append($"Vertice: {keyValuePair.Key.Id} Name: {keyValuePair.Key.Name}");
                    sb.Append(" Edges: ");
                    foreach (var edge in keyValuePair.Value)
                    {
                        sb.Append($" {edge.ToVerticeId}");
                        if (edge.Weight != 0)
                        {
                            sb.Append($" Weight: {edge.Weight}");
                        }
                            
                    }
                    sb.Append("\n");
                }
                return sb.ToString();
            }
        }
    }
}