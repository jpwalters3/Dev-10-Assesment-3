using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using SolarFarm.CORE;
using SolarFarm.CORE.Interfaces;

namespace SolarFarm.DAL
{
    //TODO save and load implementation
    public class JSONRepository : IRepository
    {
        private string _directory;
        public JSONRepository()
        {
            _directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\saves\\test.json";
        }

        public Result<Panel> Add(Panel panel)
        {
            Result<Panel> result = new Result<Panel>();
            result.Data = panel;

            Result<List<Panel>> panels = GetAll();

            if (panels.Message == "Empty repository") panels.Data = new List<Panel>();

            foreach(Panel each in panels.Data)
            {
                if(each.Row == panel.Row && each.Col == panel.Col && each.Section == panel.Section)
                {
                    result.Success = false;
                    result.Message = "Panel already exists";
                    return result;
                }
            }

            panels.Data.Add(panel);

            string json = JsonSerializer.Serialize(panels.Data);

            File.WriteAllText(_directory, json);

            result.Success = true;
            result.Message = "Success";
            result.Data = panel;

            return result;
        }

        public Result<List<Panel>> GetAll()
        {
            Result<List<Panel>> result = new Result<List<Panel>>();
            string json = File.ReadAllText(_directory);

            if(json.Length == 0 || json == null)
            {
                result.Success = false;
                result.Message = "Empty repository";
                return result;
            }

            List<Panel>? panels = JsonSerializer.Deserialize<List<Panel>>(json);
            result.Data = panels;
            result.Success = true;
            result.Message = "Data retrieved";
            return result;
        }

        public Result<Panel> Remove(Panel panel)
        {
            Result<Panel> result = new Result<Panel>();
            result.Data = panel;

            Result<List<Panel>> panels = GetAll();

            if (!panels.Success)
            {
                result.Success = false;
                result.Message = panels.Message;
                return result;
            }

            int index;
            for(index = 0; index < panels.Data.Count; index++)
            {
                if(panels.Data[index].Section == panel.Section &&
                    panels.Data[index].Row == panel.Row &&
                    panels.Data[index].Col == panel.Col)
                {

                    panels.Data.RemoveAt(index);

                    string json = JsonSerializer.Serialize(panels.Data);

                    File.WriteAllText(_directory, json);

                    result.Success = true;
                    result.Message = "Panel removed";
                    result.Data = panel;

                    return result;
                }
            }

            result.Success = false;
            result.Message = "Panel not found";
            result.Data = panel;
            return result;
        }

        public Result<Panel> Update(string keySection, int keyRow, int keyCol, Panel updated)
        {
            Result<Panel> result = new Result<Panel>();
            result.Data = updated;

            Result<List<Panel>> panels = GetAll();

            if (!panels.Success)
            {
                result.Success = false;
                result.Message = panels.Message;
                return result;
            }
            
            for (int i=0; i < panels.Data.Count; i++) {
                if (panels.Data[i].Section == keySection &&
                    panels.Data[i].Row == keyRow &&
                    panels.Data[i].Col == keyCol)
                {
                    if (updated.Year == int.MinValue) updated.Year = panels.Data[i].Year;
                    if (panels.Data[i].Equals(updated))
                    {
                        result.Success = false;
                        result.Message = "No Changes Found";
                        return result;
                    }

                    panels.Data[i] = updated;
                    string json = JsonSerializer.Serialize(panels.Data);
                    File.WriteAllText(_directory, json);

                    result.Success = true;
                    result.Message = "updated successfully";
                    result.Data = updated;
                    return result;
                }
             }

            result.Success = false;
            result.Message = "Panel specification not found";
            return result;
        }
    }
}
