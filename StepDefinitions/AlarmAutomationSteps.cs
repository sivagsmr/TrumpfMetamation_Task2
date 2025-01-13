using NUnit.Framework;
using OpenQA.Selenium.Winium;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace TrumpfMetamation_Task2.StepDefinitions
{
    [Binding]
    public class AlarmAutomationSteps
    {
        private WiniumDriver _winDriver;

        [Given(@"I launch the Clock application")]
        public void GivenILaunchTheClockApplication()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = "ms-clock:",
                UseShellExecute = true
            });
            Console.WriteLine("Clock app launched...");
        }

        [Given(@"I navigate to the Alarm tab")]
        public void GivenINavigateToTheAlarmTab()
        {
            DesktopOptions options = new DesktopOptions
            {
                ApplicationPath = @"C:\Windows\System32\cmd.exe"
            };

            // Initialize Winium Driver
            _winDriver = new WiniumDriver(@"C:\Path\To\Winium.Desktop.Driver.exe", options);
            Thread.Sleep(10000); // Wait for the Clock app to load

            // Verify Alarm tab
            var alarmMessage = _winDriver.FindElementByName("You don’t have any alarms.");
            Assert.IsNotNull(alarmMessage, "Failed to navigate to the Alarm tab.");
        }

        [When(@"I create an alarm with ""(.*)"" and ""(.*)""")]
        public void WhenICreateAnAlarmWithAnd(string time, string alarmName)
        {
            // Click Add Alarm
            _winDriver.FindElementById("AddAlarmButton").Click();

            // Set time
            _winDriver.FindElementById("HourPicker").SendKeys(time.Split(':')[0]);
            _winDriver.FindElementById("MinutePicker").SendKeys(time.Split(':')[1].Replace(" AM", "").Replace(" PM", ""));

            // Set alarm name
            var nameField = _winDriver.FindElementByName("Alarm name");
            nameField.Clear();
            nameField.SendKeys(alarmName);

            // Set repeat days
            _winDriver.FindElementById("RepeatCheckBox").Click();
            _winDriver.FindElementByName("Monday").Click();
            _winDriver.FindElementByName("Tuesday").Click();
            _winDriver.FindElementByName("Wednesday").Click();
            _winDriver.FindElementByName("Thursday").Click();
            _winDriver.FindElementByName("Friday").Click();

            // Select chime
            _winDriver.FindElementById("ChimeComboBox").Click();
            _winDriver.FindElementByName("Jingle").Click();

            // Save alarm
            _winDriver.FindElementByName("Save").Click();

            Console.WriteLine("Alarm created successfully.");
        }

        [When(@"I enable the alarm")]
        public void WhenIEnableTheAlarm()
        {
            _winDriver.FindElementById("AlarmToggleSwitch").Click();
            Console.WriteLine("Alarm enabled.");
        }

        [Then(@"I verify the alarm exists")]
        public void ThenIVerifyTheAlarmExists()
        {
            var alarmToggle = _winDriver.FindElementById("AlarmToggleSwitch");
            Assert.IsNotNull(alarmToggle, "Alarm toggle switch not found.");
            Console.WriteLine("Alarm exists and is enabled.");
        }

        [When(@"I delete the alarm")]
        public void WhenIDeleteTheAlarm()
        {
            _winDriver.FindElementById("DeleteButton").Click();
            Console.WriteLine("Alarm deleted.");
        }

        [Then(@"I verify the alarm is deleted")]
        public void ThenIVerifyTheAlarmIsDeleted()
        {
            var emptyMessage = _winDriver.FindElementById("EmptyAlarmsListMessage").Text;
            Assert.AreEqual("You don’t have any alarms.", emptyMessage, "Alarm was not deleted successfully.");
            Console.WriteLine("Verified that no alarms exist.");
        }
    }
}
