using System;
using System.Collections.Generic;
using System.Text;

namespace SolarFarm.CORE.Interfaces
{
    public interface IRepository
    {
        public Result<List<Panel>> GetAll();
        public Result<Panel> Add(Panel panel);
        public Result<Panel> Update(string keySection, int keyRow, int keyCol, Panel panel);
        public Result<Panel> Remove(Panel panel);
    }
}
