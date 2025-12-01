Feature: Căutări Google - Varianta 3
  Testarea funcționalității Google Search în Chrome:
  - căutare în limbi diferite
  - comportament la majuscule/minuscule
  - servicii speciale: calculator, convertor

  @lang
  Scenario Outline: Căutare în limbi diferite
    Given că sunt pe pagina principală Google
    When caut pe Google "<text>"
    Then rezultatele Google trebuie să conțină "<text>"

    Examples:
      | text       |
      | calculator |
      | meteo      |
      | weather    |

  @case
  Scenario: Căutare case-insensitive
    Given că sunt pe pagina principală Google
    When caut pe Google "Google"
    And memorez primul rezultat Google
    And caut pe Google "google"
    Then primul rezultat Google trebuie să fie similar pentru case-insensitive

  @calc
  Scenario: Serviciul Calculator
    Given că sunt pe pagina principală Google
    When caut pe Google "calculator"
    Then serviciul de calculator Google trebuie să fie afișat

  @convert
  Scenario: Serviciul de conversie
    Given că sunt pe pagina principală Google
    When caut pe Google "Google converter services"
    Then serviciul de conversie Google trebuie să fie afișat în partea de sus
