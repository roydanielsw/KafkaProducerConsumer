using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaProducer
{
    public class Vehicle
    {
        public virtual string Type { get; set; }

        public virtual string Model { get; set; } = string.Empty;

        public virtual DateTime ManufacterDate { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{Type}:{Model} :{ManufacterDate}";
        }


    }

    public class BMW : Vehicle
    {
        public override string Type { get { return "SUV"; } }
    }

    public class HarlyDavidson : Vehicle
    {
        public override string Type { get { return "Bike"; }}
    }
}
