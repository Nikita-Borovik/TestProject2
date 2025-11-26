using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework.Interfaces;

namespace TestProject1
{
    public class TestReportGenerator
    {
        private static readonly List<TestResultInfo> TestResults = new List<TestResultInfo>();
        private static DateTime SuiteStartTime;
        private static DateTime SuiteEndTime;

        public static void RecordTestResult(string testName, TestStatus status, string message, TimeSpan duration)
        {
            var statusString = status switch
            {
                TestStatus.Passed => "Pass",
                TestStatus.Failed => "Fail",
                TestStatus.Skipped => "Skip",
                TestStatus.Inconclusive => "Skip",
                _ => status.ToString()
            };

            TestResults.Add(new TestResultInfo
            {
                TestName = testName,
                Status = statusString,
                Duration = duration.TotalMilliseconds
            });
        }

        public static void SetSuiteStartTime()
        {
            SuiteStartTime = DateTime.Now;
        }

        public static void SetSuiteEndTime()
        {
            SuiteEndTime = DateTime.Now;
        }

        public static void GenerateReport()
        {
            var reportDir = "TestReports";
            Directory.CreateDirectory(reportDir);

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var reportPath = Path.Combine(reportDir, $"TestReport_{timestamp}.json");

            var report = new TestSuiteReport
            {
                SuiteExecutionTimestamp = SuiteStartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                TestResults = TestResults
            };

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            File.WriteAllText(reportPath, JsonSerializer.Serialize(report, jsonOptions));
            Console.WriteLine($"Отчет сгенерирован: {Path.GetFullPath(reportPath)}");
        }
    }

    public class TestResultInfo
    {
        public string TestName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double Duration { get; set; }
    }

    public class TestSuiteReport
    {
        public string SuiteExecutionTimestamp { get; set; } = string.Empty;
        public List<TestResultInfo> TestResults { get; set; } = new List<TestResultInfo>();
    }
}

