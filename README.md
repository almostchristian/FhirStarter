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
dotnet new install Ihis.FhirEngine.WebApiTemplate.CSharp::3.0.1
```

Confirm that the template is installed using the command below:
```cmd
dotnet new --list
These templates matched your input:

Template Name                                 Short Name           Language    Tags
--------------------------------------------  -------------------  ----------  ----------------
FhirEngine Web Api                            fhirengine-webapi    [C#]        Web/WebAPI/FHIR/
```

### Steps
### Add source generator nuget package
Modify FhirStarter.csproj to add the source generator nuget package.
```xml
<PackageReference Include="Synapxe.Fhir.CodeGeneration" Version="1.0.0-*" />
```

### Create a partial class for the Pokemon resource

```csharp
[GeneratedFhir("Conformance/Pokemon.StructureDefinition.json",
    SharedTerminologyResources = new string[] { "Conformance/PokemonType.ValueSet.json" })]
public partial class Pokemon
{
}
```

### Register the Pokemon custom resource
The Pokemon custom resource can be registered either by adding the registration method in Program.cs or through configuration.

```csharp
builder.AddFhirEngineServer()
    .AddCustomResource<Pokemon>();
```

```json
{
  "FhirEngine": {
    "SystemPlugins":{
	    "CustomResources": [
	      "FhirStarter.CustomResource.Pokemon"
	    ]
    }
  }
}
```

#### Update the Capability Statement to add the Pokemon resource
Add Pokemon as a resource in the `Conformance/capability-statement.json` file.

```json
"resource":[
    {
        "type": "Pokemon",
        "documentation": "A Pokemon resource.",
        "interaction": [
        {
            "code": "read",
            "documentation": "Returns the Pokemon."
        },
        {
            "code": "create",
            "documentation": "Creates a Pokemon."
        },
        {
            "code": "search-type",
            "documentation": "Searches the Pokemon."
        }
        ],
        "searchParam": [
            {
                "name": "type",
                "type": "string",
                "definition": "SearchParameter/Pokemon-type",
                "documentation": "Type of the Pokemon"
            }
        ],
        "versioning": "versioned"
    }
]
```