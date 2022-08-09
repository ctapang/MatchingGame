using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    enum Kind { Common, Rare, Epic };

    class Item
    {
        public int Id { get; private set; }
        public double Weight { get; private set; }
        public Kind Kind { get; private set; }

        public Item(int _id, double _weight, Kind _kind)
        {
            Id = _id;
            Weight = _weight;
            Kind = _kind;
        }

        public Item(Item item)
        {
            Id = item.Id;
            Weight = item.Weight;
            Kind = item.Kind;
        }

        public string Serialize() {
            StringBuilder output = new StringBuilder();
            output.Clear();
            switch (Kind)
            {
                case Kind.Common:
                    output.Append("C");
                    break;
                case Kind.Rare:
                    output.Append("R");
                    break;
                case Kind.Epic:
                    output.Append("E");
                    break;
                default:
                    throw new Exception("Invalid Kind of item found");
            }
            output.Append(Id.ToString("000"));
            output.Append(",");
            return output.ToString();
        }
    }
}
