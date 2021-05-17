using System;
using TestCA.Application.Common.Interfaces;

namespace TestCA.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
