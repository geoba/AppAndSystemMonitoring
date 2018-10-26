using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;

namespace WebApplication4.Controllers {
  public class MyDateTimeFormatter : DateTimeConverterBase {
    public override object ReadJson( Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer ) {
      return DateTime.Parse( reader.Value.ToString() );
    }

    public override void WriteJson( Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer ) {
      // writer.WriteValue( ( (DateTime) value ).ToString( "ddd dd MMM HH:mm" ) );
      writer.WriteValue( ( (DateTime) value ).ToUniversalTime() );
    }
  }
}