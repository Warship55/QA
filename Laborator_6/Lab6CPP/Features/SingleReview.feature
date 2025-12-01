Feature: Adăugare review la produs
  Ca utilizator
  Vreau să pot lăsa un review la produsul afișat

  Scenario: Adăugare review cu date valide
    Given sunt pe pagina produsului
    When completez formularul de review cu nume "Ion", email "ion@test.com" și mesaj "Produs bun"
    And trimit formularul de review
    Then ar trebui să primesc o confirmare că review-ul a fost trimis
