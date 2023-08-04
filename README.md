# FhirStarter
This is a sample FHIR RESTful API service using the FhirEngine libraries.

## Prerequisites
This project requires .NET 6 SDK to be installed and credentials to the Synapxe HIP Nexus.

The command below registers the Synapxe HIP Nexus as a nuget source. Replace the `<username>` and `<password>` arguments with a valid set of credentials.

```cmd
dotnet nuget add source https://nexus.ihis-hip.sg/repository/ihis-nuget/ -n nexus -u <username> -p <password>
```

## FhirEngine template installation

Install the template using the command below. This will install the latest available version of the template.

```cmd
dotnet new install Ihis.FhirEngine.WebApiTemplate.CSharp
```

Confirm that the template is installed using the command below:
```cmd
dotnet new --list
These templates matched your input:

Template Name                                 Short Name           Language    Tags
--------------------------------------------  -------------------  ----------  ----------------
FhirEngine Web Api                            fhirengine-webapi    [C#]        Web/WebAPI/FHIR/
```

