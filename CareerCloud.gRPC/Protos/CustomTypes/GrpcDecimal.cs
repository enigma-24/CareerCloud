using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Protos.CustomTypes
{
    public partial class GrpcDecimal
    {
        private const decimal NanoFactor = 1_000_000_000;

        public long Units { get; }
        public int Nanos { get; }

        public GrpcDecimal(long units, int nanos)
        {
            Units = units;
            Nanos = nanos;
        }

        public static implicit operator decimal(GrpcDecimal grpcDecimal)
        {
            return grpcDecimal.ToDecimal();
        }

        public static implicit operator GrpcDecimal(decimal value)
        {
            return FromDecimal(value);
        }
         
        public decimal ToDecimal()
        {
            return Units + Nanos / NanoFactor;
        }

        public static GrpcDecimal FromDecimal(decimal value)
        {
            var units = decimal.ToInt64(value);
            var nanos = decimal.ToInt32((value - units) * NanoFactor);
            return new GrpcDecimal(units, nanos);
        }
    }
}
