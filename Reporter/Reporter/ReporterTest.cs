namespace Reporter
{
    public class ReporterTest
    {
        public static void Main(string[] args)
        {
            HtmlReporter reporter = new HtmlReporter();

            int testCasesCount = 7;
            int testStepsCount = 12;

            string pageTitle = "Product - Test Report ";
            string cssFilePath = "styles/style.css";
            string headerText = "Product Name - Test Automation Results";

            reporter.InitiateHtmlReport(pageTitle, cssFilePath, headerText);

            for (int i = 1; i <= testCasesCount; i++)
            {
                reporter.StartTest("Test case " + i + " description", "testMethod" + i);

                for (int j = 1; j <= testStepsCount; j++)
                {
                    reporter.StartStep("Test step description " + j);

                    if (j % 2 == 0)
                    {
                        reporter.EndStep();
                    }
                    else
                    {
                        reporter.EndStep(false);
                    }
                }

                reporter.EndTest();
            }

            reporter.CompleteHtmlReportAndDispose();

            System.IO.File.WriteAllText("../../test.html", reporter.StringWriter.ToString());
        }
    }
}
