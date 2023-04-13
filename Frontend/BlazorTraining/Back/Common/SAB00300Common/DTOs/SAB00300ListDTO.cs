using System.Collections.Generic;
using R_APICommonDTO;

namespace SAB00300Common.DTOs
{
    public class SAB00300ListDTO<T> : R_APIResultBaseDTO 
    {
        public List<T> Data { get; set; }
    }
}