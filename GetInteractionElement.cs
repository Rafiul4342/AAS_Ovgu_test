using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BaSyx.AAS.Client.Http;
using BaSyx.Models.Core.AssetAdministrationShell;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Implementations;
using BaSyx.Models.Core.Common;
using BaSyx.Models.Extensions;
using BaSyx.Utils.ResultHandling;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace I40test
{
  
    public class GetInteractionElement
    {
        public ISubmodelElement MaintenanceOrderDescription { get; private set; }

        public SubmodelElementCollection GetSubmodelElements(string uri, string SubmodelIDShort, string SubmodelElementIDShort)
        {

            try
            {

                AssetAdministrationShellHttpClient client = new AssetAdministrationShellHttpClient(new Uri(uri));

                Console.WriteLine(client);

                var result = client.RetrieveAssetAdministrationShell();
                Console.WriteLine(result.Entity.GetType());
                if (result == null)
                {
                    Console.WriteLine("AAS data not received");
                    Console.WriteLine(result.GetType());
                }
                //var a = result.Entity[]
                else
                {
                    var AASGetData = result.Entity.ToJson();
              //      Console.WriteLine(AASGetData);


                    //   client.RetrieveSubmodelElementValue("Maintenence")
                 //   Console.WriteLine(result);
                   
                }
            var result1 = client.RetrieveSubmodelElement(SubmodelIDShort, SubmodelElementIDShort);
             Console.WriteLine(result1.ToString());
                 var result1Josn = result1.Entity.ToJson();
                Console.WriteLine(result1Josn);
                Console.WriteLine(result1.Entity.GetType());


                //  var interactionElement = result1.Entity.Parent;
                // Console.WriteLine(interactionElement);

                //   var jsonData = (JObject)JsonConvert.DeserializeObject(result1Josn);
                //  var message = jsonData["MaintenanceOrderDescription"];

                //  Console.WriteLine(message);



                /* JsonDocument doc = JsonDocument.Parse(result1Josn);

                  JsonElement idShortElement = doc.RootElement
                  .GetProperty("value")
                  .EnumerateArray()
                  .FirstOrDefault(x => x.GetProperty("idShort").GetString() == "MaintenanceOrderDescription")?
                  .GetProperty("value");
                  */

                // Console.WriteLine(idShortElement.ToString());


                //  JObject obj = JObject.Parse(result1Josn);

                //JToken Orderdescription = obj["value"]
                //.FirstOrDefault(x => x["idShort"].ToString() == "MaintenanceOrderDescription")?["value"];

                //    Console.WriteLine(Orderdescription);

                //var MHsev = se3.value.Find(n => n.idShort == "MaintenanceHistory");
                //var mr1 = MHsev.value.Find(n => n.idShort == "MaintenanceRecord_1");
                //Console.WriteLine(mr1.value is JsonElement);
                //Console.WriteLine(mr1.value.ToString());


                SubmodelElementCollection submodelElementCollection = JsonConvert.DeserializeObject<SubmodelElementCollection>(result1Josn);

                Console.WriteLine(submodelElementCollection);

                foreach (var element in submodelElementCollection.Value)
                {
                   IValue val = element.GetValue();
                    Console.WriteLine(val.Value);
                }


                return submodelElementCollection;
            }
            catch(Exception ex)
            {
               
                    Console.WriteLine(ex);
                return null;
            }



        }
    }

}
