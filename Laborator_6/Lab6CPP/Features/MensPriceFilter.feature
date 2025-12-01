Feature: Filtrarea produselor după preț pe Men's wear
  Ca utilizator
  Vreau să pot filtra produsele după un interval de preț

  Scenario: Produsele sunt filtrate după intervalul selectat
    Given sunt pe pagina Men's wear
    When setez filtrul de preț între 50 și 200
    Then toate produsele afișate au prețul între 50 și 200
