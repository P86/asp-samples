using System;

namespace Paging.Controllers
{
    public class ForecastParams
    {
        const int maxSize = 20;
        private int size;

        public int Page { get; set; } = 1;
        public int Size
        {
            get => size;
            set => size = Math.Min(value, maxSize);
        }
    }
}