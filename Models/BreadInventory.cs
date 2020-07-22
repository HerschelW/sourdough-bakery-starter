using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_bakery
{
  public enum BreadType
  {
    Sourdough, // 0
    Focaccia,  // 1
    Rye,       // 2
    White,     // 3
  }

  public class BreadInventory
  {
    // primary key, int id
    public int id { get; set; }

    // bread name, like "Daily Sourdough Bread"
    [Required]
    public string name { get; set; }

    [Required]
    public string description { get; set; }

    // check to make sure that inventory defaults to 0 if not defined?
    public int inventory { get; set; }

    // bread type! Sourdough, Focaccia, Rye, White
    // implicitly convert the enum to a string on json serialization (woo!)
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BreadType breadType { get; set; }

    // who baked this bread? bakedBy
    // EF will do some magic here. It will create
    // a new column based on the property name + primary key
    // of the other table.
    // --> bakedByid will be the ACTUAL column name
    public Baker bakedBy { get; set; }

    // expose the existing bakedByid field from the database
    public int bakedByid { get; set; }

    // TODO: expose bakedByid
    // TODO: serialize breadType for JSON purposes

    // some helper methods: NOT database column
    public void bake(int count = 1)
    {
      this.inventory += count;
    }

    public void sell(int count = 1)
    {
      this.inventory -= count;
    }
  }
}