using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

namespace Services;

public class HistoryWriter
{
    public string Filepath {get; set;}
    public List<Tournament> History {get; set;}

    public HistoryWriter(string filepath){
        this.Filepath = filepath;
        History = new List<Tournament>(
            JsonSerializer.Deserialize<Tournament[]>(File.ReadAllText(this.Filepath))
            );

    }

    public void SaveToHistory(Tournament t){

        int i = this.History.FindIndex(x => x.Equals(t));

        if(i!=-1) {
            History[i] = t;
        }
        else{
            History.Add(t);
        }

        File.WriteAllText(Filepath,JsonSerializer.Serialize(History));

    }


}
