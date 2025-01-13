Feature: Alarm Automation in Clock App

  Scenario: Create, enable, and delete an alarm
    Given I launch the Clock application
    And I navigate to the Alarm tab
    When I create an alarm with "9:00 AM" and "Trumpf Metamation - Login Time"
    And I enable the alarm
    Then I verify the alarm exists
    When I delete the alarm
    Then I verify the alarm is deleted
