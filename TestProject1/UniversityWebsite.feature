@regression
Feature: University Website E2E Scenarios
  As a user
  I want to interact with the university website
  So that I can perform various actions like searching, language switching, and contact verification

  Background:
    Given I am on the university website homepage

  @search
  Scenario: Search for study programs
    When I search for "study programs"
    And I submit the search form
    Then I should see search results

  @language
  Scenario: Change language to Lithuanian
    When I change the language to Lithuanian
    Then I should be redirected to the Lithuanian version of the site

  @contact
  Scenario: Verify contact information
    When I navigate to the contact page
    Then I should see the correct email address "E-mail: franciskscarynacr@gmail.com"
    And I should see the correct Lithuanian phone number "Phone (LT): +370 68 771365"
    And I should see the correct Belarus phone number "Phone (BY): +375 29 5781488"
    And I should see the correct social networks information "Join us in the social networks: Facebook Telegram VK"
