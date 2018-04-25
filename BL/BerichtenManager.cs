using DAL;
using Domain.Bericht;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public class BerichtenManager
  {
    private readonly BerichtenRepository repository;

    public BerichtenManager()
    {
      repository = new BerichtenRepository();
    }

    public void AddBericht(Bericht bericht)
    {
      repository.CreateBericht(bericht);
    }

    public IEnumerable<Bericht> GetBerichten()
    {
      return repository.ReadBerichten();
    }

    public Bericht GetBericht(int id)
    {
      return repository.ReadBericht(id);
    }

    public void ChangeBericht(Bericht bericht)
    {
      repository.UpdateBericht(bericht);
    }

    public void RemoveBericht(Bericht bericht)
    {
      repository.DeleteBericht(bericht);
    }

    public void AddBerichten()
    {
      string filePath = @"C:\Users\bartw\OneDrive\Documenten\KdG\2eJaar\Integratieproject\Barometer\PB.DAL\Res\textgaindump.json";

      JObject alleBerichten = JObject.Parse(File.ReadAllText(filePath));
      List<Bericht> berichtenList = new List<Bericht>();
      List<Bericht> bestaandeBerichten = repository.ReadBerichten().ToList();
      foreach (var bericht in alleBerichten["records"])
      {
        var dict = bericht.ToObject<Dictionary<string, dynamic>>();
        var tempDict = new Dictionary<string, List<string>>();
        foreach (var item in dict)
        {
          if (!(item.Value is IList))
          {
            tempDict.Add(item.Key, new List<string>() { item.Value.ToString() });
          }
          else if (item.Value is IList)
          {
            tempDict.Add(item.Key, item.Value.ToObject<List<string>>());
          }
        }
        berichtenList.Add(new Bericht { BerichtVelden = tempDict });
      }
      repository.CreateBerichten(berichtenList);
    }
  }
}
