Feature: Unicitatea produselor pe pagina Men's wear
  Ca utilizator
  Vreau ca fiecare produs din listă să fie unic

  Scenario: Nu există produse duplicate în listă
    Given sunt pe pagina Men's wear
    When citesc toate numele produselor
    Then lista de produse NU trebuie să conțină duplicate
