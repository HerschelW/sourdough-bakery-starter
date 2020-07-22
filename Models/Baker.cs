using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// MVC: This is one of our models in our Model-View-Controller pattern
/*
You can have two kinds of 'variables' inside a c# class
  public int id; // this is a field
  public int id { get; set; } // this is a property

  The big reason for this: .NET Core WebAPI controllers only look for properties ( fields are ignored)
*/

namespace dotnet_bakery
{
    public class Baker {
      // lets our id: a primary key
      // 'id' is a special name, any class or model with a property called 'id'
      //  will automatically get a primary key field in the database of the given type
      public int id { get; set; }

      // this is a required field
      [Required]
      public string name { get; set; }

      [Required]
      [EmailAddress]
      public string emailAddress { get; set; } 
    }
}
