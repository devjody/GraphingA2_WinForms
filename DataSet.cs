using System.Drawing;

namespace A2_Graphs_RAD
{
    public class DataSet
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public Color Color { get; set; }

        public DataSet(string name, double value, Color color)
        {
            Name = name;
            Value = value;
            Color = color;
        }
    }
}
