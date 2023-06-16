using System;
using I40Extension.UtilsMqtt;
using I40Extension.Message;
using System.Collections.Generic;
using BaSyx.Models.Core.AssetAdministrationShell;
using BaSyx.Models.Core.AssetAdministrationShell.Implementations;
using System.Text;
using System.Net.Http;
using BaSyx.AAS.Client.Http;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.Common;
using BaSyx.Models.Extensions;

namespace I40test
{
    class Program
    {
        public static List<SubmodelElementCollection> inter = new List<SubmodelElementCollection>();
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // intantiate client

            var mqttClient = new MqttClientWrapper("test.mosquitto.org", 1883);
            string uri = "http://localhost:5180";
            //Create message
           GetInteractionElement ie = new GetInteractionElement();
           SubmodelElementCollection orderdescription= ie.GetSubmodelElements(uri, "MaintenanceSubmodel", "Maintenance_1/MaintenanceOrderDescription");

            
            
       

            if (orderdescription != null)
            {
                foreach (var element in orderdescription.Value)
            {
                    if(element.IdShort == "MaintenanceElement")
                    {
                        IValue valuuu = element.GetValue();
                        Console.WriteLine(valuuu);
                        Console.WriteLine(element.IdShort);
                    }
               
            }
            }

            inter.Add(orderdescription);


            I40Extension.I40Message notify_init = new I40Extension.I40Message();

            notify_init.frame = new I40Extension.Frame()
            {
                conversationId = "adsadasf",
                messageId = "1",
                sender =  {
                    identification =
                          {
                    id = "BASYX_MACHINE_AAS_POC",
                    idType = "Coustom",

                },
                     role =
                            {
                              name = "InformationSender",

                          }
                        },
               
                     receiver =
                      {
                          identification =
                          {
                              id ="MES_AAS",
                              idType = "Custom",
                          },
                          role ={
                                  name = "InformationReceiver"
                          }
              
                      },
                     type = "NOTIFY_INIT",

                inReplyTO = "null", 
                replyBy = "null", 
                semanticProtocol =
                {
                     keys = new List<Key>
                      {
                          new Key
                          {
                              type = "GlobalReference",
                              idType ="CUSTOM",
                              value ="Maintenance"
                          }
                      },
                }     
            

            };

            notify_init.interactionElements = inter;

            Console.WriteLine(notify_init.GetFrame());


            /*
              I40Extension.I40Message mes = new I40Extension.I40Message
              {
                 interactionElements = inter,

                  frame =
                  {
                       type = "NOTIFY_INIT",
                       sender =
                      {
                          identification =
                          {
                              id="BASYX_MACHINE_AAS_POC",
                              idType="Coustom",

                          },
                          role =
                          {
                              name = "InformationSender",

                          }
                      },
                       receiver =
                      {
                          identification =
                          {
                              id ="MES_AAS",
                              idType = "Custom",
                          },
                          role ={
                                  name = "InformationReceiver"
                          }
                      },
                       conversationId ="Maintenance_1::1",
                       messageId ="1",
                      inReplyTO ="null",
                      replyBy = "null",
                      semanticProtocol = new SemanticProtocol
                      {

                      keys = new List<Key>
                      {
                          new Key
                          {
                              type = "GlobalReference",
                              idType ="CUSTOM",
                              value ="Maintenance"
                          }
                      }
                      }
                  }

              };

              var result = mqttClient.PublishAsync("Test", mes);
              mqttClient.Subscribe("Test");

              // mqtt Received message handler 
              mqttClient.MessageReceived += (sender, args) => {

                  var receivedTopics = args.ApplicationMessage.Topic;
                  var receivedPayload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
                  Console.WriteLine(receivedPayload);


              };


  */



        }
    }
}
           
           
        
           
           