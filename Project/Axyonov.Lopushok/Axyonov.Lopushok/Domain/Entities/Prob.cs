using System;
using System.Collections.Generic;

namespace Axyonov.Lopushok.Domain.Entities
{
    public partial class Prob
    {
        public string Продукция { get; set; } = null!;
        public string НаимованиеМатериала { get; set; } = null!;
        public string НеобходимоеКоличествоМатериала { get; set; } = null!;
    }
}
