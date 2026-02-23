using System;

namespace RefugeAnimauxWPF.coucheAccesDB
{
    public class ExceptionAccesBD : Exception
    {
        private string? details;

        public ExceptionAccesBD(string message, string? details) : base(message)
        {
            this.details = details;
        }

        public string? Details { get => details; set => details = value; }
    }
}
