Feature: Sortare produse pe pagina Men's wear
  Pentru a găsi produsele ușor
  Ca utilizator
  Vreau să pot sorta lista de produse alfabetic

  Scenario: Sortare după nume A-Z
    Given sunt pe pagina Men's wear
    When sortez produsele după "Name(A - Z)"
    Then lista de produse este sortată alfabetic crescător
