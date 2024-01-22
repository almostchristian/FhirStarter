@Environment:Integration
Feature: Pokemon

Background:
    Given a random tag
    And a new HttpClient as httpClient
        | HeaderName               | Value   |
        | X-Ihis-SourceApplication | testapp |

Scenario: Reading a newly created Pokemon returns exactly the same Pokemon
    Given a Resource is created from Samples/Pokemon.json as createdPkmn
    When getting Pokemon/{createdPkmn.Id} as readPkmn
    Then createdPkmn is a Fhir Pokemon with data
        | Path        | Value | FhirType |
        | statusCode  | 201   |          |
        | primaryType | water | code     |
    And createdPkmn and readPkmn are exactly the same

Scenario: Creating a Pokemon with an invalid type returns 422 status code
    When creating from Samples/Pokemon.json with data as createdPkmn
        | Path        | Value  | FhirType |
        | primaryType | waterx | code     |
    Then createdPkmn is a Fhir OperationOutcome with data
        | Path        | Value  | FhirType |
        | statusCode  | 422    |          |

Scenario: Creating a Pokemon with an invalid species name returns 422 status code
    When creating from Samples/Pokemon.json with data as createdPkmn
        | Path | Value  | FhirType |
        | name | Agamon | string     |
    Then createdPkmn is a Fhir OperationOutcome with data
        | Path        | Value  | FhirType |
        | statusCode  | 422    |          |

Scenario: Searching for fire type Pokemon returns all fire type Pokemon
    Given a Resource is created from Samples/Pokemon.json with data as charmander
        | Path          | Value      | FhirType |
        | name          | Charmander | string   |
        | nationalDexNo | 4          | int      |
        | primaryType   | fire       | code     |
    And a Resource is created from Samples/Pokemon.json with data as pikachu
        | Path          | Value    | FhirType |
        | name          | Pikachu  | string   |
        | nationalDexNo | 25       | int      |
        | primaryType   | electric | code     |
    And a Resource is created from Samples/Pokemon.json with data as houndoom
        | Path          | Value    | FhirType |
        | name          | Houndoom | string   |
        | nationalDexNo | 229      | int      |
        | primaryType   | dark     | code     |
        | secondaryType | fire     | code     |
    When getting Pokemon?type=fire as searchBundle
    Then searchBundle is a Fhir Bundle which contains charmander,houndoom