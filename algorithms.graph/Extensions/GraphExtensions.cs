using System.Text;

namespace algorithms.graph.Extensions;

public static class GraphExtensions
{
    extension(Graph source)
    {
        public string ToString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var keyValuePair in source.Vertices)
                {
                    sb.Append("Key: " + keyValuePair.Key);
                    sb.Append(" Value: " + keyValuePair.Value);
                    sb.Append("\n");
                }

                return sb.ToString();
            }
        }
    }
}