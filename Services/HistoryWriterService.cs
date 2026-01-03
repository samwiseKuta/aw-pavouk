using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Interfaces;
using Models;

namespace Services;

public class HistoryWriterService : IHistoryService
{
    public string Filepath {get; set;}
    public List<Tournament> History {get; set;}

    public HistoryWriterService(string filepath){
        this.Filepath = filepath;
        History = new List<Tournament>(
            JsonSerializer.Deserialize<Tournament[]>(File.ReadAllText(this.Filepath))
            );

    }

    bool IHistoryService.SaveToHistory(Tournament t)
    {

        int i = this.History.FindIndex(x => x.Equals(t));

        if(i!=-1) {
            History[i] = t;
        }
        else{
            History.Add(t);
        }

        File.WriteAllText(Filepath,JsonSerializer.Serialize(History));
        return true;
    }

    bool IHistoryService.RemoveFromHistory(Tournament t)
    {
        int i = History.FindIndex(x => x.Equals(t));

        if(i==-1) {
            return true;
        }
        else{
            History.RemoveAt(i);
        }

        File.WriteAllText(Filepath,JsonSerializer.Serialize(History));
        return true;
    }

    public Task<bool> SaveToHistoryAsync(Tournament t)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveFromHistoryAsync(Tournament t)
    {
        throw new NotImplementedException();
    }

    public string GetFilepath()
    {
        return this.Filepath;
    }

    public List<Tournament> GetHistory()
    {
        return this.History;
    }
}
