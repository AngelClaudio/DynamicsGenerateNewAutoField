﻿using Microsoft.Xrm.Sdk;
using System;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Metadata;

namespace DynamicsGenerateNewAutoField
{
    internal class Program
    {
        static void Main()
        {
            //Create IOrganization Service Object

            //URL is from Settings > Customizations > Developer Resources > Organization Service
            CrmServiceClient conn =
                new CrmServiceClient("AuthType=AD;Url=https://crmdev.com/enteryourorg/XRMServices/2011/Organization.svc;" +
                "Username=youruserid;Password=yourpassword");

            var _service = (IOrganizationService)conn.OrganizationServiceProxy;

            //Test ID brings back a GUID
            var resp1 = (WhoAmIResponse)_service.Execute(new WhoAmIRequest());
            Console.WriteLine($"My GUID ID is ${resp1.UserId}");

            //CreateAutoNumberField(_service);

            Console.WriteLine("\n We are at the end... press any key to exit.");

            Console.ReadKey();
        }

        static void CreateAutoNumberField(IOrganizationService _serviceProxy)
        {
            try
            {
                CreateAttributeRequest widgetSerialNumberAttributeRequest = new CreateAttributeRequest
                {
                    EntityName = "aiims_incident",
                    Attribute = new StringAttributeMetadata
                    {
                        //Define the format of the attribute
                        AutoNumberFormat = "AIIMS-{DATETIMEUTC:yyyyMMddhhmmss}-{RANDSTRING:4}",
                        LogicalName = "aiims_autogeneratedid",
                        SchemaName = "aiims_autogeneratedid",
                        RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                        MaxLength = 100, // The MaxLength defined for the string attribute must be greater than the length of the AutoNumberFormat value, that is, it should be able to fit in the generated value.
                        DisplayName = new Label("Autogenerated AIIMS ID", 1033),
                        Description = new Label("Autogenerated AIIMS ID.", 1033)
                    }
                };
                _serviceProxy.Execute(widgetSerialNumberAttributeRequest);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error!" + "Message: " + ex.Message + Environment.NewLine +
                    "StackTrace: " + ex.StackTrace + Environment.NewLine +
                    "Inner Exception: " + ex.InnerException);

                Console.WriteLine("\n We are at the end... press any key to exit.");

                Console.ReadKey();
            }
        }
    }


}