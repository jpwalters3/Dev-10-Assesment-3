using System;
using System.Collections.Generic;
using SolarFarm.CORE;
using SolarFarm.CORE.Interfaces;
using SolarFarm.DAL;

namespace SolarFarm.BLL
{
    public class RecordService
    {
        private IRepository _repo;

        public RecordService()
        {
            _repo = new JSONRepository();
        }
        public Result<Panel> AddPanel(Panel panel)
        {
            Result<Panel> result;
            if (panel.Year < 1970)
            {
                result = new Result<Panel>();
                result.Success = false;
                result.Message = "Invalid year, must be after 1970";
                return result;
            }
            if (panel.Year > DateTime.Today.Year)
            {
                result = new Result<Panel>();
                result.Success = false;
                result.Message = $"Invalid year, cannot be after {DateTime.Today.Year}";
                return result;
            }
            result = _repo.Add(panel);
            return result;
        }

        public Result<List<Panel>> GetSection(string section)
        {
            Result<List<Panel>> result = new Result<List<Panel>>();
            Result<List<Panel>> panels = _repo.GetAll();

            if(!panels.Success)
            {
                result.Success = false;
                result.Message = panels.Message;
                return result;
            }

            result.Data = new List<Panel>();

            foreach(Panel panel in panels.Data)
            {
                if (panel.Section == section)
                {
                    result.Data.Add(panel);
                }
            }

            if (result.Data == null || result.Data.Count == 0)
            {
                result.Success = false;
                result.Message = "section not found";
                return result;
            }

            result.Success = true;
            result.Message = "success";
            return result;
        }

        public Result<Panel> RemovePanel(Panel panel)
        {
            return _repo.Remove(panel);
        }

        public Result<Panel> UpdatePanel(string keySection, int KeyRow, int keyCol, Panel updated)
        {
            Result<Panel> result;
            if (updated.Row == int.MinValue) updated.Row = KeyRow;
            if (updated.Col == int.MinValue) updated.Col = keyCol;
            if (updated.Section == "" || updated.Section == null) updated.Section = keySection;

            if (updated.Year < 1970 && updated.Year != int.MinValue)
            {
                result = new Result<Panel>();
                result.Success = false;
                result.Message = "Invalid year, must be after 1970";
                return result;
            }
            if (updated.Year > DateTime.Today.Year)
            {
                result = new Result<Panel>();
                result.Success = false;
                result.Message = $"Invalid year, cannot be after {DateTime.Today.Year}";
                return result;
            }

            if(updated.Row > 250 || 
              (updated.Row < 0))
            {
                result = new Result<Panel>();
                result.Success = false;
                result.Message = "Row out of range";
                return result;
            }

            if (updated.Col > 250 ||
               (updated.Col < 0))
            {
                result = new Result<Panel>();
                result.Success = false;
                result.Message = "Column out of range";
                return result;
            }

            return _repo.Update(keySection, KeyRow, keyCol, updated);
        }
    }
}
