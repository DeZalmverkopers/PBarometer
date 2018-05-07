using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Bericht
{
  public class Bericht
  {
    public Bericht()
    {

    }

    [NotMapped]
    public Dictionary<string, List<string>> BerichtVelden
    {
      get
      {
        return BerichtVeldenJSON == null ? null :
        JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(BerichtVeldenJSON);
      }
      set
      {
        BerichtVeldenJSON = JsonConvert.SerializeObject(value);
      }
    }
    public string BerichtVeldenJSON { get; set; }
    public int BerichtId { get; set; }

  }
}

