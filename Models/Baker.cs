using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// MVC: This is one of our models in our Model-View-Controller pattern
/*
    You can have two kinds of 'variables' inside a c# class:
        public int id; // this is a field
        public int id { get; set; } // this is a property
    The big reason for this: .NET Core WebAPI controllers only look for properties
    (fields are ignored)
*/
namespace dotnet_bakery
{
  public class Baker
  {

    // lets our id: a primary key
    // 'id' is a special name. any class/model with a property called 'id' will
    // automagically get a primary key field in the database of the given type
    public int id { get; set; }

    // this is a required field
    [Required]
    public string name { get; set; }

    [Required]
    [EmailAddress]
    public string emailAddress { get; set; }

    // breadCount field that tracks how many rows are associated with ME in the 
    // breadInventory table
    // because this is a LIST of BreadInventory items, EF/.NET is smart enough to
    // go look at the BreadInventory class to see if THAT links to THIS class via
    // a foreign key linkage in the database. 
    // because it DOES (via bakedByid), EF actually queries for us and dumps the
    // result into this property right here. It's magic.
    // there's no column associated
    [JsonIgnore] // hey please dont include this in the json api
    public ICollection<BreadInventory> myBreads { get; set; }

    [NotMapped] // tell EF to chill. this is NOT a db property
    public int breadCount
    {
      get
      {
        return (myBreads != null
            ? myBreads.Count
            : 0
        );
      }
    }

    // how many total breads have i baked?
    [NotMapped]
    public int totalBreadCount
    {
      get
      {
        if (myBreads == null) return 0;

        // breads.map(b => b.inventory).reduce((a,b) => a+b);
        int total = myBreads.Select(b => b.inventory).Sum();
        return total;
      }
    }
  }
}