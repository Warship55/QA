Feature: Căutare produs pe pagina principală
  Ca utilizator
  Vreau să caut un produs existent după nume

  Scenario: Căutare după numele unui produs existent
    Given sunt pe pagina principală
    When caut după textul "Formal Blue Shirt"
    Then ar trebui să văd rezultate relevante pentru produs
