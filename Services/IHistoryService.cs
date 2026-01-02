using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services;

public interface IHistoryService
{
    public string GetFilepath();
    public List<Tournament> GetHistory();
    public bool SaveToHistory(Tournament t);
    public bool RemoveFromHistory(Tournament t);

    public Task<bool> SaveToHistoryAsync(Tournament t);
    public Task<bool> RemoveFromHistoryAsync(Tournament t);

}
