using System;

namespace SolarFarm.CORE
{
    public class Panel
    {
        public string Section { set; get; }
        public int Row { set; get; }
        public int Col { set; get; }
        public int Year { set; get; }
        public Materials Material { set; get; }
        public bool IsTracking { set; get; }
        

        public Panel(string Section, int Row, int Col, int Year, Materials Material, bool IsTracking)
        {
            this.Section = Section;
            this.Row = Row;
            this.Col = Col;
            this.Year = Year;
            this.Material = Material;
            this.IsTracking = IsTracking;
        }

        public Panel()
        {

        }
        public override string ToString()
        {
            string tracking = IsTracking ? "yes" : "no";
            return $"Section: {Section}\n" +
                $"Row: {Row}\n" +
                $"Column: {Col}\n" +
                $"Year: {Year}\n" +
                $"Material: {Material}\n" +
                $"Tracking: {tracking}";
        }

        public override bool Equals(object obj)
        {
            return obj is Panel panel &&
                   Section == panel.Section &&
                   Row == panel.Row &&
                   Col == panel.Col &&
                   Year == panel.Year &&
                   Material == panel.Material &&
                   IsTracking == panel.IsTracking;
        }
    }
}
